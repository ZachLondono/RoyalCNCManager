using System.Collections.Generic;

using RoyalCNCTrackerLib.Models;

namespace RoyalCNCTrackerLib.DAL {

	public interface ILabelDatabase {

		IEnumerable<CADCodeProgram> GetAllPrograms(string jobname);

		SinglePart GetSinglePart(string jobDbName, string programName);

		IEnumerable<SinglePart> GetAllSubParts(string jobDbName, string name);

	}


}
