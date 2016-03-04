using UnityEngine;
using StridersVR.Domain.TrainOfThought;

namespace StridersVR.Modules.TrainOfThought.Logic.StrategyInterfaces
{
	public interface IStrategyTrainGeneration
	{
		float selectTrain(ScriptableObject genericColorTrainData);
		bool instantiateTrain ();
	}
}

