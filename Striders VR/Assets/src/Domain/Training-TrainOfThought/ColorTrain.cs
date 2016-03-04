using System;
using UnityEngine;

namespace StridersVR.Domain.TrainOfThought
{
	[System.Serializable]
	public class ColorTrain
	{

		[SerializeField] private String trainName;
		[SerializeField] private GameObject prefab;
		[SerializeField] private ColorStation trainDestination;


		public ColorTrain ()
		{
		}

		#region Properties
		public String TrainName
		{
			get { return this.trainName; }
			set { this.trainName = value; }
		}

		public GameObject Prefab
		{
			get { return this.prefab; }
			set { this.prefab = value; }
		}

		public ColorStation TrainDestination
		{
			get { return this.trainDestination; }
			set { this.trainDestination = value; }
		}
		#endregion
	}
}

