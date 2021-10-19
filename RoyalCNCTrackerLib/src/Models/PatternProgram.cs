using System.Collections.Generic;

namespace RoyalCNCTrackerLib.Models {

	public class PatternProgram : CADCodeProgram {

		private IEnumerable<CADCodeProgram> _components = null;

		public PatternProgram(IEnumerable<CADCodeProgram> components) {
			_components = components;
		}

		public override IEnumerable<CADCodeProgram> GetComponents() {
			return _components;
		}

	}

}
