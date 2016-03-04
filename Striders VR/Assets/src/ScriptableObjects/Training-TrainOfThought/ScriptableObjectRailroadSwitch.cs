using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StridersVR.Domain.TrainOfThought;

namespace StridersVR.ScriptableObjects.TrainOfThought
{
	public class ScriptableObjectRailroadSwitch : ScriptableObject
	{
		[SerializeField]
		private List<RailroadSwitch> railroadSwitchListEasyMode;

		#region
		public List<RailroadSwitch> RailroadSwitchListEasyMode
		{
			get { return this.railroadSwitchListEasyMode; }
		}
		#endregion
	}
}

