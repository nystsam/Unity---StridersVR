using UnityEngine;
using StridersVR.Modules.TrainOfThought.Logic.StrategyInterfaces;

namespace StridersVR.Modules.TrainOfThought.Logic.Strategies
{
	public class StrategyTrainActionsChangeRotation : IStrategyTrainActions<float, float>
	{

		private Vector3 newTrainDirection;
		private float currentYAngle;

		public StrategyTrainActionsChangeRotation (Vector3 newTrainDirection, float currentYAngle)
		{
			this.newTrainDirection = newTrainDirection;
			this.currentYAngle = currentYAngle;
		}


		#region IStrategyTrainOrientation
		public float performAction(float parameter)
		{
			int numberSignForZ = -1;
			if (Mathf.Round (this.currentYAngle) == 180) 
			{
				numberSignForZ = -numberSignForZ;
			}

			if (this.newTrainDirection.z != 0) 
			{
				return parameter = Vector3.Dot (Vector3.one, this.newTrainDirection) * numberSignForZ;
			} else 
			{
				return parameter = Vector3.Dot (Vector3.one, this.newTrainDirection);
			}

		}
		#endregion
	}
}

