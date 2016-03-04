using UnityEngine;
using StridersVR.Modules.TrainOfThought.Logic.StrategyInterfaces;

namespace StridersVR.Modules.TrainOfThought.Logic.Contexts
{
	public class ContextTrainActions
	{

		public ContextTrainActions ()
		{
		}

		#region Service methods
		public Vector3 changeDirection(IStrategyTrainActions<Collider,Vector3> strategyTrainActions, Collider parameter)
		{
			return strategyTrainActions.performAction (parameter);
		}

		public float changeRotation(IStrategyTrainActions<float, float> strategyTrainActions, float parameter)
		{
			return strategyTrainActions.performAction (parameter);
		}
		#endregion



	}
}

