using System.Collections;
using UnityEngine;

namespace StridersVR.Domain.SpeedPack
{
	public class Item
	{
		private GameObject itemPrefab;
	
		private int spacing;

		public Item (GameObject prefab, int spacing)
		{
			this.itemPrefab = prefab;
			this.spacing = spacing;
		}


		#region Properties
		public GameObject ItemPrefab 
		{
			get {
				return itemPrefab;
			}
		}

		public int Spacing 
		{
			get {
				return spacing;
			}
		}
		#endregion
	}
}

