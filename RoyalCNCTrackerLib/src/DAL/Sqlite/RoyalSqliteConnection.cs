using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

namespace RoyalCNCTrackerLib.DAL.Sqlite {

	public class RoyalSqliteConnection {

		private SqliteConnection _connection { get; set; }
		private bool _isConnected;

		public RoyalSqliteConnection(string path) {
			_connection = new SqliteConnection($"Data Source='{path}';");
			_isConnected = false;
		}

		public int ExecuteQuery(string query) {
			throw new NotImplementedException();
		} 
		
		public int ExecuteQuery(string query, Dictionary<string, object> queryParams) {
			throw new NotImplementedException();
		}

		public void ExecuteReaderQuery(string query, Action<SqliteDataReader> onRead) {
			throw new NotImplementedException();
		}
		
		public void ExecuteReaderQuery(string query, Dictionary<string, object> queryParams, Action<SqliteDataReader> onRead) {
			throw new NotImplementedException();
		}

		public void Open() {
			if (_isConnected) return;
			_connection.Open();
			_isConnected = true;
		}
		
		public void Close() {
			if (!_isConnected) return;
			_connection.Close();
			_isConnected = false;
		}

	}

}
