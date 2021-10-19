using System.Collections.Generic;
using RoyalCNCTrackerLib.Models;

namespace RoyalCNCTrackerLib.DAL {

	// <summary>
	// Interface for interacting with a data repository which will hold job information
	// </summary>
	public interface IJobRepository : IRepository<Job> {
		public Job GetByName(string jobName);
	}

}
