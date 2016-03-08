using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StridersVR.Domain.TrainOfThought;

namespace StridersVR.ScriptableObjects.TrainOfThought
{
	public class ScriptableObjectRailroadSwitch : ScriptableObject
	{
		[SerializeField] private List<RailroadSwitch> railroadSwitchListEasyMode;
		[SerializeField] private List<RailroadSwitch> railroadSwitchListMediumMode;
		[SerializeField] private List<RailroadSwitch> railroadSwitchListHardMode;
		[SerializeField] private List<Track> trackListMediumMode;
		[SerializeField] private List<Track> trackListHardMode;
		[SerializeField] private GameObject playerButton;


		#region Properties
		public List<RailroadSwitch> RailroadSwitchListEasyMode
		{
			get { return this.railroadSwitchListEasyMode; }
		}

		public List<RailroadSwitch> RailroadSwitchListMediumMode
		{
			get { return this.railroadSwitchListMediumMode; }
		}

		public List<RailroadSwitch> RailroadSwitchListHardMode
		{
			get { return this.railroadSwitchListHardMode; }
		}

		public List<Track> TrackListMediumMode
		{
			get { return this.trackListMediumMode; }
		}

		public List<Track> TrackListHardMode
		{
			get { return this.trackListHardMode; }
		}

		public GameObject PlayerButton
		{
			get { return this.playerButton; }
		}
		#endregion
	}
}

