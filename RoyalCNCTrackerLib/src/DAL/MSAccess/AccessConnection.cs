using System;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Runtime.Versioning;

using RoyalCNCTrackerLib.Models;

namespace RoyalCNCTrackerLib.DAL.MSAccess {


	[SupportedOSPlatform("windows")]
	public class AccessConnection {

		private bool _isOpen { get; set; }
		private OleDbConnection _connection { get; set; }

		public AccessConnection(string connectionString) {
			_connection = new OleDbConnection(connectionString);
			Open();
		}

		public int ExecuteQuery(string query) {
	
			OleDbCommand command = new(query);
	
			command.Connection = _connection;
			Open();

			var rowsAffected = command.ExecuteNonQuery();
			Close();

			return rowsAffected;
	
		}
	
		public void GetReaderFromDb(string query, Action<OleDbDataReader> onRead) {
	
			OleDbCommand command = new(query);

			command.Connection = _connection;
			Open();

			OleDbDataReader reader = command.ExecuteReader();

			onRead(reader);				
	
			Close();
		}

		public void Open() {
			if (_isOpen) return;
			_isOpen = true;
			_connection.Open();
		}

		public void Close() {
			if (!_isOpen) return;
			_isOpen = false;
			_connection.Close();
		}

	}

}
