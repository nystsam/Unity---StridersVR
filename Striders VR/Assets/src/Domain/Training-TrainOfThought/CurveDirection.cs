using System;
using UnityEngine;
using System.Collections.Generic;

namespace StridersVR.Domain.TrainOfThought
{
	[System.Serializable]
	public class CurveDirection
	{

		[SerializeField] private GameObject prefab;
		[SerializeField] private Vector3 direction;
		[SerializeField] private Vector3 position;


		public CurveDirection ()
		{
		}


		#region Properties
		public GameObject Prefab
		{
			get { return this.prefab; }
			set { this.prefab = value; }	
		}

		public Vector3 Direction
		{
			get { return this.direction; }
			set { this.direction = value; }	
		}

		public Vector3 Position
		{
			get { return this.position; }
			set { this.position = value; }	
		}
		#endregion
	}
}

