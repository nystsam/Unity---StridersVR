using System.Collections;
using UnityEngine;

namespace StridersVR.Domain.SpeedPack
{
	public class Spot
	{
		private int spotId;

		private GameObject spotPrefab;

		private Vector3 spotPosition;

		private Item currentItem;

		private bool isAvailableSpot;

		
		public Spot (int spotId,GameObject prefab, Vector3 position)
		{
			this.spotId = spotId;
			this.spotPrefab = prefab;
			this.spotPosition = position;

			this.currentItem = null;
			this.isAvailableSpot = true;

		}


		public void setItem(Item newItem)
		{
			this.currentItem = newItem;
			this.isAvailableSpot = false;
		}

		#region Properties
		public int SpotId
		{
			get { return this.spotId; }
		}
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
		}
		public Item CurrentItem
		{
			get { return this.currentItem; }
		}
		#endregion
	}
}

