using System.Collections;
using UnityEngine;

namespace StridersVR.Domain.SpeedPack
{
	public class Item
	{
		private int itemId;

		private GameObject itemPrefab;

		private bool isCreated;

		public Item (GameObject prefab)
		{
			this.itemPrefab = prefab;
			this.isCreated = false;
		}


		#region Properties
		public int ItemId {
			get {
				return itemId;
			}
			set {
				itemId = value;
			}
		}
		public GameObject ItemPrefab 
		{
			get {
				return itemPrefab;
			}
		}
		public bool IsCreated {
			get {
				return isCreated;
			}
			set {
				isCreated = value;
			}
		}
		#endregion
	}
}

