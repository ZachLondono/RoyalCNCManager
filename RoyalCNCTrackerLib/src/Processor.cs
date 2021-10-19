using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

using RoyalCNCTrackerLib.DAL;
using RoyalCNCTrackerLib.Models;

namespace RoyalCNCTrackerLib {
	class Program {
	
		static void Main(string[] args) {


		}

	}

	public class Processor {

		private readonly ICCRepository _ccBarcodeDb; 
		private readonly IJobRepository _jobDb;
		private readonly IProgramRepository _programDb;
		private readonly ILabelDBFactory _labelDbFactory; 
		private readonly string _patternOutputPath; 
		private readonly string _imagePath; 
		
		public SortType _sortType { get; set; }
		public SortOrder _sortOrder { get; set; }		

		public Processor(string patternPath, string imagePath, ICCRepository jobDb, IJobRepository trackerDb, IProgramRepository programDb, ILabelDBFactory labelDbFactory) {
			_ccBarcodeDb = jobDb;
			_jobDb = trackerDb;
			_programDb = programDb;
			_labelDbFactory = labelDbFactory;
			_patternOutputPath = patternPath;
			_imagePath = imagePath;

			_sortType = SortType.Name;
			_sortOrder = SortOrder.Ascending;
		}    

		// <summary
		// Process barcode input and returns a CADCodeProgam with that name. If one does not exists it will return null.
		// </summary>
		// <remark>
		// The input to this method should be a CADCode pattern program name. When called, this method first checks if the pattern has already been loaded.
		// If it has then it will retrieve the program details from the 'trackerDb' and return them. If the program has not already been loaded from the network drive,
		// it will load all of it's information. In order to minimize how many times this needs to be done, when the first pattern is entered, the program information of 
		// all of the patterns, as well as their sub programs will be loaded and stored in the DB.
		// </remark>
		// <exception cref="ArgumentException">Thrown when the argument for the method is not a valid pattern program</exception>
		public CADCodeProgram GetCADCodeProgram(string input) {

			// Check if the program is already in the database
			CADCodeProgram program = _programDb.GetByName(input);		

			if (program is not null) return program;

			// If the pattern is not already in the database we need to retrieve it and add it to the local database 
			Job job = _ccBarcodeDb.GetJobFromPattern(input);

			if (job is null) {
				throw new ArgumentException($"Unable to find program '{input}'");
			}

			// Add the record of the job to the database 
			job = _jobDb.Insert(job);

			ILabelDatabase labelDb = _labelDbFactory.Create(job.LabelDBPath);

			// Copy over files from the job
			IEnumerator<Pattern> patternEnum = job.Patterns.GetEnumerator();
			while (patternEnum.MoveNext()) {
				Pattern p = patternEnum.Current;

				string newFilePath = _patternOutputPath + "\\" + p.Name;			
				string oldFilePath = job.Path + "\\" + p.Name;

				bool copied = CopyFile(oldFilePath, newFilePath);

				if (!copied) Debug.WriteLine($"Failed to copy file '{oldFilePath}' to '{newFilePath}'");
				
				// TODO: create a CADCodeProgam for each pattern in the job, then write it to the tracker db
				IEnumerable<SinglePart> subPrograms = labelDb.GetAllSubParts(job.DBName, p.Name);

				CADCodeProgram newProgram = new PatternProgram(subPrograms);
				newProgram.ProgramName = p.Name;
				newProgram.JobDBName = job.DBName;
				newProgram.ImageFile = GetImagePath(p.Name);
				newProgram.GCodeFile = newFilePath;
				newProgram.IsComplete = false;

				newProgram = _programDb.Insert(newProgram);

				if (input == p.Name) program = newProgram;

			}

			return program;

		}

		// <summary>
		// Updates the tracker database so that it is in sync with the job database
		// </summary>
		// <remark>
		// This method will query the job database which holds the newest job data
		// and store all of the jobs in the tracker database as well as remove all 
		// old jobs from the tracker database which are no longer in the job databse.
		// </remark>
		public void UpdateTrackerDatabase() {
			throw new NotImplementedException();
		}

		// <summary>
		// This method will return the full path to the image for the given pattern
		// </summary>
		// <param name="pattern">The pattern id for which to get the image</param>
		// <remark>
		// For a pattern program, at least one of the other patterns in the job must have already been scanned.
		// For a single program, the pattern to which it belongs must have already been scanned.
		// </remark>
		public string GetImagePath(string program) {

			// if program is a full pattern, then the image of the part will be the name of the program
			// if it is not a full pattern, first check to see if the part is stored in the tracker database,
			// otherwise, if the name of the program is a single part, then the files name is stored in the job's label database  

			string filePath = _imagePath + "\\" + program;

			// First check if the program is a full pattern, simply by checking if the file exists
			if (File.Exists(filePath)) 
				return filePath;

			// Second, check if the single part is already stored in the tracker database
			CADCodeProgram part = _programDb.GetByName(program);
			if (part is not null) return part.ImageFile;
			
			return null;

			/*// If the file does not exists, we need to get the label database 
			Job job = _ccBarcodeDb.GetJobFromPattern(program);
			// if the program is not stored in the job database, there is no way to tell what job it belongs to
			if (job is null) return null;
			// If it is not in the CADCode barcode database, it may be a single part file
			ILabelDatabase labelDb = _labelDbFactory.Create(job.LabelDBPath);
			part = labelDb.GetSinglePart(job.DBName, program);
			return part.ImageFile;	*/

		}
		
		// <summary>
		// Returns the index of the given file name in the pattern directory.
		// </summary>
		// <remark>
		// The index includes both the reference to the current directory (".") and the previous directory ("..")
		// The order that the files are read in is dependent on the values of the _sortType & _sortOrder properties 
		// </remark>
		public int GetFileIndex(string filename) {
			
			try {

				var di = new DirectoryInfo(_patternOutputPath);
				var dirInfo = di.EnumerateFiles("*", SearchOption.TopDirectoryOnly);

				IEnumerable<FileInfo> ordered = null;
				switch (_sortType) {
					case SortType.CreationDate:
						ordered = dirInfo.OrderBy(x => x.CreationTime);
						break;
					case SortType.AccessDate:
						ordered = dirInfo.OrderBy(x => x.LastAccessTime);
						break;
					case SortType.WriteDate:
						ordered = dirInfo.OrderBy(x => x.LastWriteTime);
						break;
					case SortType.Name:
					default:
						ordered = dirInfo.OrderBy(x => x.Name);
						break;
				}

				if (_sortOrder == SortOrder.Descending) ordered = ordered.Reverse();

				IEnumerator<FileInfo> fileEnumerator = ordered.GetEnumerator();

				int i = 3;
				while (fileEnumerator.MoveNext()) {
					var file = fileEnumerator.Current;
					if (file.Name == filename) return i;
					i++;
					Debug.WriteLine($"{file.Name} {file.CreationTime}");
				}

			} catch (Exception e) {
				Console.WriteLine(e.Message);
				return -1;
			}

			return -1;

		}

		private bool CopyFile(string srcPath, string destPath) {
			try {
				File.Copy(srcPath, destPath, true);
				return true;
			} catch (Exception e) { 
				Debug.WriteLine($"Error copying files\n'{e}'");
				return false;
			}
		}

	}

}
