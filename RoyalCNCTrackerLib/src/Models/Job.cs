using RoyalCNCTrackerLib.DAL;
using System.Collections.Generic;

namespace RoyalCNCTrackerLib.Models {

	public class Job : BaseRepoClass {

		public string Name { get; set; }
		
		public string Path { get; set; }

		public string DBName { get; set; }

		public string LabelDBPath { get; set; }
		
		public IEnumerable<Pattern> Patterns { get; set; }

	}

}


