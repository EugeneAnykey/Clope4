using System;

namespace ClopeLib
{
	public interface IClopeInfo
	{
		// event
		event EventHandler StepDone;



		// field
		string CurrentStepName { get; }



		// method
		string CurrentOutput();

		string CurrentInfo();
	}
}
