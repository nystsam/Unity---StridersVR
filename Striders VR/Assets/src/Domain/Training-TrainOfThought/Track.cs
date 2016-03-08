using System;
using UnityEngine;
using System.Collections.Generic;

namespace StridersVR.Domain.TrainOfThought
{
	[System.Serializable]
	public class Track
	{
		[SerializeField] private GameObject prefab;
		[SerializeField] private Vector3 position;
		[SerializeField] private Vector3 eulerRotation;
		[SerializeField] private Vector3 scale;

		public Track ()
		{
		}


		#region Properties
		public GameObject Prefab
		{
			get { return this.prefab; }
			set { this.prefab = value; }
		}

		public Vector3 Position
		{
			get { return this.position; }
			set { this.position = value; }
		}

		public Vector3 EulerRotation
		{
			get { return this.eulerRotation; }
			set { this.eulerRotation = value; }
		}

		public Vector3 Scale
		{
			get { return this.scale; }
			set { this.scale = value; }
		}
		#endregion
	}
}

