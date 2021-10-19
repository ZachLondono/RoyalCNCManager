using System.Collections.Generic;

namespace RoyalCNCTrackerLib.Models {

	public class SinglePart : CADCodeProgram {
		// <summary>
		// A single part does not contain any components/sub parts 
		// </summary>
		public override IEnumerable<CADCodeProgram> GetComponents() {
			return null;
		}	
	}

}
