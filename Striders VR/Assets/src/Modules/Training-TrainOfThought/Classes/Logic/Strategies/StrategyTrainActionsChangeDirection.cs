using UnityEngine;
using StridersVR.Modules.TrainOfThought.Logic.StrategyInterfaces;

namespace StridersVR.Modules.TrainOfThought.Logic.Strategies
{
	public class StrategyTrainActionsChangeDirection : IStrategyTrainActions<Collider, Vector3>
	{
		
		public StrategyTrainActionsChangeDirection ()
		{
		}


		#region IStrategyTrainOrientation
		public Vector3 performAction(Collider parameter)
		{
			Vector3 newDirection = Vector3.zero;
			if (parameter.tag.Equals ("Curve")) 
			{
				newDirection = parameter.GetComponent<CurveController> ().direction;
			}
			else if (parameter.tag.Equals ("RailroadSwitch")) 
			{
				newDirection = parameter.GetComponent<RailroadSwitchController> ().getSelectedDitecion;
			}
			return newDirection;
		}
		#endregion
	}
}

