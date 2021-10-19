using System;
using System.Collections.Generic;

using Xunit;

using RoyalCNCTrackerLib.Models;
using RoyalCNCTrackerLib.DAL;
using RoyalCNCTrackerLib.DAL.MSAccess;
using System.Diagnostics;
using Xunit.Abstractions;

namespace CNCLibTests {
	public class BarcodeDBTests {

		private readonly string _testDbPath = "C:\\Users\\Zachary Londono\\Documents\\GitHub\\RoyalCNC\\CNCLibTests\\TestData\\CLIndex.mdb";
		private readonly ICCRepository _barcodeDB;
		private readonly ITestOutputHelper _testOutput;

		public BarcodeDBTests(ITestOutputHelper testOutput) {
			_barcodeDB = new AccessCCRepository(_testDbPath);
			_testOutput = testOutput;
		}

		[Fact]
		public void GetAllTest() {

		}

		[Fact]
		public void GetKnownJob() {
			
		}

		[Fact]
		public void GetKnownPattern() {
						
		}

	}
}
