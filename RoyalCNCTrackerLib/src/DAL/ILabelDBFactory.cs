using RoyalCNCTrackerLib.Models;

namespace RoyalCNCTrackerLib.DAL {

	public interface ILabelDBFactory {
		ILabelDatabase Create(string connectionstring);
	}

}

