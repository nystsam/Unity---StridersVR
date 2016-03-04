using System;
using UnityEngine;

namespace StridersVR.Domain.TrainOfThought
{
	[System.Serializable]
	public class ColorStationSpawnPoint
	{
		
		[SerializeField] private Vector3 stationPosition;
		[SerializeField] private Vector3 stationRotation;

		public ColorStationSpawnPoint ()
		{
		}


		#region Properties
		public Vector3 StationPosition
		{
			get { return this.stationPosition; }
			set { this.stationPosition = value; }
		}

		public Vector3 StationRotation
		{
			get { return this.stationRotation; }
			set { this.stationRotation = value; }
		}
		#endregion
	}
}

