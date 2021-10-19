using System;
using System.Diagnostics;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Runtime.Versioning;

using RoyalCNCTrackerLib.Models;

namespace RoyalCNCTrackerLib.DAL.MSAccess {

	// An implementation of IJobDatabase which uses MS Access as the backend
	[SupportedOSPlatform("windows")]
	public class AccessCCRepository : ICCRepository {

		// TODO load this info from a config file
		private string _tableName = "Barcodes";
		private string _jobNameCol = "DatabaseJobName";
		private string _pathNameCol = "GCodePath";
		private string _labelPathCol = "DatabaseName";
		private string _dbNameCol = "DatabaseJobName";
		private string _patternNameCol = "GCodeFileName";
	
		private AccessConnection _connection { get; set; }

		public AccessCCRepository(string source) {
			string _connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={source}";
			_connection = new AccessConnection(_connectionString);
		}

		public Job GetJobFromName(string jobname) {

			try {
				Job job = null;

				string query = $"SELECT * FROM {_tableName} WHERE {_jobNameCol} = \"{jobname}\";";
				_connection.GetReaderFromDb(query, (reader) => {

					if (!reader.HasRows) return;

					job = new Job();

					List<Pattern> patterns = new List<Pattern>();

					reader.Read();
					job.Name = jobname;
					job.Path = reader[_pathNameCol].ToString();
					job.DBName = jobname;//reader[_dbNameCol].ToString();
					job.LabelDBPath = reader[_labelPathCol].ToString();

					Pattern pattern = new Pattern();
					pattern.Name = reader[_patternNameCol].ToString();
					pattern.IsComplete = false;
					patterns.Add(pattern);

					while (reader.Read()) {
						pattern = new Pattern();
						pattern.Name = reader[_patternNameCol].ToString();
						pattern.IsComplete = false;
						patterns.Add(pattern);
					}

					job.Patterns = patterns;

				});

				_connection.Close();
				return job;

			} catch (Exception ex) {
				Debug.WriteLine("[Exception in AccessJobDatabase]" + ex);
				_connection.Close();
				return null;
			}

		}

		public Job GetJobFromPattern(string pattern) {


			Job job = new Job();
			try {
	
				string query = $"SELECT * FROM {_tableName} WHERE {_patternNameCol} = \"{pattern}\";";

				bool jobFound = false;
				// First query will get the job information			
				_connection.GetReaderFromDb(query, (reader) => {

					if (!reader.HasRows) return;
					else jobFound = true;					

					reader.Read();
					job.Name = reader[_jobNameCol].ToString();
					job.Path = reader[_pathNameCol].ToString();
					job.DBName = reader[_dbNameCol].ToString();
					job.LabelDBPath = reader[_labelPathCol].ToString();

				});

				if (!jobFound) {
					_connection.Close();
					return null;
				}

				query = $"SELECT * FROM {_tableName} WHERE {_jobNameCol} = \"{job.Name}\";";
				// Second query will get all associated patterns
				_connection.GetReaderFromDb(query, (reader) => {

					if (!reader.HasRows) return;	
				
					List<Pattern> patterns = new List<Pattern>();
					while (reader.Read()) {
						Pattern pattern = new Pattern();
						pattern.Name = reader[_patternNameCol].ToString();
						pattern.IsComplete = false;
						patterns.Add(pattern);
					}

					job.Patterns = patterns;

				});


			} catch (Exception ex) {
				Debug.WriteLine("[Exception in AccessJobDatabase]" + ex);
				_connection.Close();
				return null;
			}
			
			_connection.Close();

			return job;
		}

		public IEnumerable<Job> GetAllJobs() {

			// map job name to job for quick access
			Dictionary<string, Job> jobmap = new();

			// map from job name to patterns
			Dictionary<string, List<Pattern>> patternmap = new();

			string query = $"SELECT {_jobNameCol}, {_pathNameCol}, {_labelPathCol}, {_dbNameCol}, {_patternNameCol} FROM {_tableName};";

			_connection.GetReaderFromDb(query, (reader) => {

				while (reader.Read()) {

					string jobName = reader[_jobNameCol].ToString();
					if (!jobmap.ContainsKey(jobName)) {

						Job job = new();
						job.Name = jobName;
						job.Path = reader[_pathNameCol].ToString();
						job.LabelDBPath = reader[_labelPathCol].ToString();
						job.DBName = reader[_dbNameCol].ToString();
						job.Patterns = null;

						jobmap.Add(jobName, job);

					}

					if (!patternmap.ContainsKey(jobName)) {
						patternmap.Add(jobName, new List<Pattern>());
					}

					Pattern pattern = new Pattern();
					pattern.Name = reader[_patternNameCol].ToString();
					pattern.IsComplete = false;

					patternmap[jobName].Add(pattern);

				}

			});

			var jobEnum = patternmap.GetEnumerator();
			while (jobEnum.MoveNext()) {
				KeyValuePair<string, List<Pattern>> pair = jobEnum.Current;
				jobmap[pair.Key].Patterns = pair.Value;
			}

			return jobmap.Values;

		}

	}

}
