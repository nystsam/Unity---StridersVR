using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StridersVR.Domain.TrainOfThought;

namespace StridersVR.ScriptableObjects.TrainOfThought
{
	public class ScriptableObjectColorStation : ScriptableObject 
	{

		[SerializeField]
		private List<ColorStation> stationsList;
		[SerializeField]
		private List<ColorStationSpawnPoint> stationSpawnPoints;

		#region Properties
		public List<ColorStation> StationsList
		{
			get { return this.stationsList; }
		}

		public List<ColorStationSpawnPoint> StationSpawnPoints
		{
			get { return this.stationSpawnPoints; }
		}
		#endregion

	}
}

