using System.Collections.Generic;

using RoyalCNCTrackerLib.Models;

namespace RoyalCNCTrackerLib.DAL {

	// <summary>
	// Interface for interacting with CADCode's barcode database 
	// </summary>
	public interface ICCRepository {

		// Get the job with the given name
		Job GetJobFromName(string jobname);

		// Get the job to which the given pattern belongs
		Job GetJobFromPattern(string pattern);

		IEnumerable<Job> GetAllJobs();

	}


}
