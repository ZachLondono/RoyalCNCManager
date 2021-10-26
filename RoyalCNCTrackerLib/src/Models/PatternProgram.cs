using System.Collections.Generic;
using System.Linq;

namespace RoyalCNCTrackerLib.Models {

	public class PatternProgram : CADCodeProgram {

		private IEnumerable<SinglePart> _components = null;

		public PatternProgram(IEnumerable<SinglePart> components) {
			_components = components;
		}

		public override IEnumerable<SinglePart> GetComponents() {
			foreach (SinglePart component in _components) component.ParentId = Id;
			return _components;
		}

	}

}
