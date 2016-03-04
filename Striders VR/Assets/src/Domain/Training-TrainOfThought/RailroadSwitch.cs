using System;
using UnityEngine;
using System.Collections.Generic;

namespace StridersVR.Domain.TrainOfThought
{
	[System.Serializable]
	public class RailroadSwitch
	{

		[SerializeField] private String name;
		[SerializeField] private GameObject prefab;
		[SerializeField] private Vector3 position;
		[SerializeField] private Vector3 rotationEuler;
		[SerializeField] private List<Vector3> directions;

		public RailroadSwitch ()
		{
		}

		#region Properties
		public String Name
		{
			get { return this.name; }
			set { this.name = value; }
		}

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

		public Vector3 RotationEuler
		{
			get { return this.rotationEuler; }
			set { this.rotationEuler = value; }
		}

		public List<Vector3> Directions
		{
			get { return this.directions; }
			set { this.directions = value; }
		}
		#endregion
	}
}

