namespace RoyalCNCTrackerLib {

	public interface IOutputWriter {

		void OutputInformation(string output);
		
		void OutputWarning(string output);
		
		void OutputError(string output);


	}

}
