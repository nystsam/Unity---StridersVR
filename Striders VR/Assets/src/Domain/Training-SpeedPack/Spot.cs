using System.Collections;
using UnityEngine;

namespace StridersVR.Domain.SpeedPack
{
	public class Spot
	{
		private GameObject spotPrefab;

		private Vector3 spotPosition;

		private Item currentItem;

		private bool isAvailableSpot;

		
		public Spot (GameObject prefab, Vector3 position)
		{
			this.spotPrefab = prefab;
			this.spotPosition = position;

			this.currentItem = null;
			this.isAvailableSpot = true;

		}


		#region Properties
		public GameObject SpotPrefab
		{
			get { return this.spotPrefab; }
		}
		public Vector3 SpotPosition
		{
			get { return this.spotPosition; }
		}
		public bool IsAvailableSpot 
		{
			get { return this.isAvailableSpot;}
			set { this.isAvailableSpot = value;}
		}
		#endregion
	}
}

