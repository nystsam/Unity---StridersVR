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
		private bool isMainSpot;

		
		public Spot (int spotId,GameObject prefab, Vector3 position)
		{
			this.spotId = spotId;
			this.spotPrefab = prefab;
			this.spotPosition = position;

			this.currentItem = null;
			this.isAvailableSpot = true;
			this.isMainSpot = false;
		}


		public void setItem(Item newItem)
		{
			this.currentItem = newItem;
			this.isAvailableSpot = false;
		}

		public void setMainSpot()
		{
			this.isMainSpot = true;
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
		public bool IsMainSpot
		{
			get { return this.isMainSpot; }
		}
		#endregion
	}
}

