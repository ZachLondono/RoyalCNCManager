using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using RoyalCNCTrackerLib.Models;

namespace RoyalCNCTrackerLib.DAL.Sqlite {
	public class SqliteProgramRepository : IProgramRepository {

		private readonly string _jobTableName = "programs";
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
			throw new NotImplementedException();
		}

		public CADCodeProgram Insert(CADCodeProgram entity) {
			throw new NotImplementedException();
		}

		public void Remove(int id) {
			throw new NotImplementedException();
		}

		public void Update(CADCodeProgram entity) {
			throw new NotImplementedException();
		}
	}
}
