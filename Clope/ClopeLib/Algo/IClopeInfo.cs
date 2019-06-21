using System;

namespace ClopeLib.Algo
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
