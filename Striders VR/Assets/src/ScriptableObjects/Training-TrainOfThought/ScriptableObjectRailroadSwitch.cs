using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StridersVR.Domain.TrainOfThought;

namespace StridersVR.ScriptableObjects.TrainOfThought
{
	public class ScriptableObjectRailroadSwitch : ScriptableObject
	{
		[SerializeField] private List<RailroadSwitch> railroadSwitchListEasyMode;
		[SerializeField] private GameObject playerButton;

		#region
		public List<RailroadSwitch> RailroadSwitchListEasyMode
		{
			get { return this.railroadSwitchListEasyMode; }
		}

		public GameObject PlayerButton
		{
			get { return this.playerButton; }
		}
		#endregion
	}
}

