using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RoyalCNCTrackerLib.Models;

namespace RoyalCNCTrackerLib.DAL {
	public interface IProgramRepository : IRepository<CADCodeProgram> {
		CADCodeProgram GetByName(string programName);
	}
}
