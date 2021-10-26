using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using RoyalCNCTrackerLib.Models;

namespace RoyalCNCTrackerLib.DAL.Sqlite {
	public class SqliteProgramRepository : IProgramRepository {

		private readonly string _programTable = "programs";
		private readonly string _idCol = "id";
		private readonly string _nameCol = "program_name";
		private readonly string _jobDbNameCol = "job_db_name";
		private readonly string _imagePathCol = "image_file";
		private readonly string _gcodePathCol = "gcode_file";
		private readonly string _isCompleteCol = "is_complete";
		private readonly string _parentIDCol = "parent_id";

		private readonly SqliteConnection _connection;

		public SqliteProgramRepository(SqliteConnection connection) {
			_connection = connection;
		}

		public CADCodeProgram GetById(int id) {
			throw new NotImplementedException();
		}

		public CADCodeProgram GetByName(string programName) {

			SqliteCommand command = _connection.CreateCommand();
			command.CommandText = $@"SELECT {_idCol}, {_jobDbNameCol}, {_imagePathCol}, {_gcodePathCol}, {_isCompleteCol}, {_parentIDCol}
									FROM {_programTable}
									WHERE {_nameCol} = @name";

			command.Parameters.AddWithValue("@name", programName);

			CADCodeProgram program;
			using (var reader = command.ExecuteReader()) {

				if (!reader.HasRows) return null;
				reader.Read();

				int id = reader.GetInt32($"{_idCol}");
				int parentId = reader.GetInt32($"{_parentIDCol}");

				if (parentId == -1) {

					SqliteCommand subcommand = _connection.CreateCommand();
					subcommand.CommandText = $@"SELECT {_idCol}, {_nameCol}, {_jobDbNameCol}, {_imagePathCol}, {_gcodePathCol}, {_isCompleteCol}
												FROM {_programTable}
												WHERE {_parentIDCol} = @parentId";

					subcommand.Parameters.AddWithValue("@parentId", id);

					List<SinglePart> components = new List<SinglePart>();
					using (var subreader = subcommand.ExecuteReader()) {

						while (subreader.Read()) {

							SinglePart component = new SinglePart();
							component.Id = subreader.GetInt32($"{_idCol}");
							component.ProgramName = subreader.GetString($"{_nameCol}");
							component.JobDBName = subreader.GetString($"{_jobDbNameCol}");
							component.ImageFile = subreader.GetString($"{_imagePathCol}");
							component.GCodeFile = subreader.GetString($"{_gcodePathCol}");
							component.IsComplete = subreader.GetBoolean($"{_isCompleteCol}");
							component.ParentId = id;

							components.Add(component);

						}

					}

					program = new PatternProgram(components);
				} else {
					program = new SinglePart();
				}

				program.Id = reader.GetInt32(0);
				program.ProgramName = programName;
				program.JobDBName = reader.GetString(1);
				program.ImageFile = reader.GetString(2);
				program.GCodeFile = reader.GetString(3);
				program.IsComplete = reader.GetBoolean(4);
				program.ParentId = parentId;

			}

			return program;


		}

		public CADCodeProgram Insert(CADCodeProgram entity) {
			SqliteCommand command = _connection.CreateCommand();
			command.CommandText = $@"INSERT INTO {_programTable}
									({_nameCol}, {_jobDbNameCol}, {_imagePathCol}, {_gcodePathCol}, {_isCompleteCol}, {_parentIDCol})
									VALUES (@name, @job, @image, @gcode, @status, @parent_id);
									SELECT seq FROM sqlite_sequence WHERE name = '{_programTable}';";

			command.Parameters.AddWithValue("@name", entity.ProgramName);
			command.Parameters.AddWithValue("@job", entity.JobDBName);
			command.Parameters.AddWithValue("@image", entity.ImageFile);
			command.Parameters.AddWithValue("@gcode", entity.GCodeFile);
			command.Parameters.AddWithValue("@status", entity.IsComplete);
			command.Parameters.AddWithValue("@parent_id", entity.ParentId);

			using (var reader = command.ExecuteReader()) {
				if (!reader.HasRows) return null;
				reader.Read();
				entity.Id = reader.GetInt32(0);
			}

			return entity;

		}

		public void Remove(int id) {
			throw new NotImplementedException();
		}

		public void Update(CADCodeProgram entity) {
			throw new NotImplementedException();
		}
	}
}
