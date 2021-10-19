using RoyalCNCTrackerLib.DAL;
using System.Collections.Generic;
namespace RoyalCNCTrackerLib.Models {

	// <summary>
	// Model which represents a cadcode program
	// </summary>
	public abstract class CADCodeProgram : BaseRepoClass {

		public string ProgramName { get; set; }

		public string JobDBName { get; set; }
	
		public string ImageFile { get; set; }

		public string GCodeFile { get; set; }

		public bool IsComplete { get; set; }

		public abstract IEnumerable<CADCodeProgram> GetComponents();

		public new string ToString => ProgramName;

	}

}
