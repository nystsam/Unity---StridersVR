using System;

namespace StridersVR.Modules.TrainOfThought.Logic.StrategyInterfaces
{
	public interface IStrategyTrainActions<Paremeter, ReturnValue>
	{
		ReturnValue performAction(Paremeter parameter);
	}
}

