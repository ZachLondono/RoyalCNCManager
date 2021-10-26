using System;
using System.Collections.Generic;

using Microsoft.Data.Sqlite;

using RoyalCNCTrackerLib.Models;

namespace RoyalCNCTrackerLib.DAL.Sqlite {

	public class SqliteJobRepository : IJobRepository {

		private readonly string _jobTableName = "jobs";
		private readonly string _idCol = "id";
		private readonly string _nameCol = "name";
		private readonly string _pathCol = "path";
		private readonly string _dbNameCol = "db_name";
		private readonly string _labelPathCol = "label_db_path";

		private readonly SqliteConnection _connection;

		public SqliteJobRepository(SqliteConnection connection) {
			_connection = connection;
		}
	
		// Create a new job in the database
		public Job Insert(Job job) {

			SqliteCommand command = _connection.CreateCommand();

			command.CommandText = $@"INSERT INTO {_jobTableName} 
									({_nameCol}, {_pathCol}, {_dbNameCol}, {_labelPathCol})
									VALUES (@name, @path, @dbname, @labelpath);
									SELECT seq FROM sqlite_sequence WHERE name = '{_jobTableName}';";

			command.Parameters.AddWithValue("@name", job.Name);
			command.Parameters.AddWithValue("@path", job.Path);
			command.Parameters.AddWithValue("@dbname", job.DBName);
			command.Parameters.AddWithValue("@labelpath", job.LabelDBPath);

			using (var reader = command.ExecuteReader()) {

				if (!reader.HasRows) return null;

				reader.Read();

				job.Id = reader.GetInt32(0);

			}

			return job;

		}
		
		public Job GetByName(string jobName) {

			SqliteCommand command = _connection.CreateCommand();
			command.CommandText = $"SELECT {_idCol}, {_pathCol}, {_dbNameCol}, {_labelPathCol} FROM {_jobTableName} WHERE {_nameCol} = @name";

			command.Parameters.AddWithValue("@name", jobName);

			using (var reader = command.ExecuteReader()) {

				if (!reader.HasRows) return null;

				reader.Read();

				Job job = new Job {
					Id = reader.GetInt32(0),
					Name = jobName,
					Path = reader.GetString(1),
					DBName = reader.GetString(2),
					LabelDBPath = reader.GetString(3)
				};

				return job;

			}

		}
		
		public Job GetById(int id) {
			throw new NotImplementedException();
		}

		public void Update(Job job) {

			SqliteCommand command = _connection.CreateCommand();

			command.CommandText = $@"UPDATE {_jobTableName}
									SET {_nameCol} = @name, {_pathCol} = @path, {_dbNameCol} = @dbname, {_labelPathCol} = @labelpath
									WHERE {_idCol} = @id";

			command.Parameters.AddWithValue("@name", job.Name);
			command.Parameters.AddWithValue("@path", job.Path);
			command.Parameters.AddWithValue("@dbname", job.DBName);
			command.Parameters.AddWithValue("@labelpath", job.LabelDBPath);

			command.ExecuteNonQuery();

		}

		public void Remove(int id) {
			throw new NotImplementedException();
		}
	}
}
