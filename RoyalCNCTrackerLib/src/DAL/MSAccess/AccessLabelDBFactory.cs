using RoyalCNCTrackerLib.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace RoyalCNCTrackerLib.DAL.MSAccess {
	
	[SupportedOSPlatform("windows")]
	public class AccessLabelDBFactory : ILabelDBFactory {

		public string ImagePath { get; set; }
		public string GCodePath { get; set; } //"Y:\\CADCode\\CNC Programs\\Omnitech";

		private readonly ICCRepository _barcodeRepository;

		public AccessLabelDBFactory(ICCRepository barcodeRepository, string imagePath, string gcodePath) {
			_barcodeRepository = barcodeRepository;
			ImagePath = imagePath;
			GCodePath = gcodePath;
		}

		public ILabelDatabase Create(string pathToDb) {
			ILabelDatabase database = new AccessLabelDatabase(pathToDb, GCodePath, ImagePath, _barcodeRepository);
			return database;
		}


	}

	[SupportedOSPlatform("windows")]
	public class AccessLabelDatabase : ILabelDatabase {

		private readonly string _singlePartGCodePath;
		private readonly string _singlePartImagePath;
		private readonly AccessConnection _accessConnection;
		private readonly ICCRepository _barcodeRepository;

		public AccessLabelDatabase(string path, string gcodePath, string imagePath, ICCRepository barcodeRepository) {
			_accessConnection = new AccessConnection($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={path}");
			_barcodeRepository = barcodeRepository;
			_singlePartGCodePath = gcodePath;
			_singlePartImagePath = imagePath;
		}

		public IEnumerable<CADCodeProgram> GetAllPrograms(string jobDbName) {
			throw new NotImplementedException();
		}

		public SinglePart GetSinglePart(string jobDbName, string programName) {
			throw new NotImplementedException();
		}

		public IEnumerable<SinglePart> GetAllSubParts(string jobDbName, string programName) {

			string query = $"SELECT [Filename], [Job Name], [Machining Picture] FROM [{jobDbName}] WHERE [Pattern Barcode] = \"{programName}\"";

			List<SinglePart> singleParts = new();

			_accessConnection.GetReaderFromDb(query, (reader) => {

				if (!reader.HasRows)
					return;

				while (reader.Read()) {

					string filename = "";
					try {
						filename = reader[0].ToString();
					} catch (Exception e) { 
						Debug.WriteLine(e);
					}
					
					string jobname = "";
					try {
						jobname = reader[1].ToString();
					} catch (Exception e) { 
						Debug.WriteLine(e);
					}

					string machiningPicture = "";
					try {
						machiningPicture = reader[2].ToString();
					} catch (Exception e) {
						Debug.WriteLine(e);
					}

					if (string.IsNullOrEmpty(filename) || string.IsNullOrEmpty(jobname) || string.IsNullOrEmpty(machiningPicture))
						continue;

					SinglePart part = new SinglePart {
						GCodeFile = $"{_singlePartGCodePath}\\{jobname}\\{filename}",
						ImageFile = $"{_singlePartImagePath}\\{machiningPicture}.wmf",
						IsComplete = false,
						JobDBName = jobDbName,
						ProgramName = filename,
						ParentId = 0
					};
					singleParts.Add(part);
				}

			});

			return singleParts;

		}

	}

}
