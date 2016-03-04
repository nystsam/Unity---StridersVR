using System;
using UnityEngine;

namespace StridersVR.Domain.TrainOfThought
{
	[System.Serializable]
	public class ColorStation
	{

		[SerializeField] private String stationName;
		[SerializeField] private GameObject prefab;


		public ColorStation ()
		{
		}


		#region Properties
		public String StationName
		{
			get { return this.stationName; }
			set { this.stationName = value; }
		}

		public GameObject Prefab
		{
			get { return this.prefab; }
			set { this.prefab = value; }
		}
		#endregion
	}
}

