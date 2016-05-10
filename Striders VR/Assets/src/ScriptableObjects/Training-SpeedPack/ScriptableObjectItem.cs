using UnityEngine;
using System.Collections.Generic;
using StridersVR.Domain.SpeedPack;

namespace StridersVR.ScriptableObjects.SpeedPack
{
	public class ScriptableObjectItem : ScriptableObject
	{
		[SerializeField]
		private GameObject myCellphone;

		[SerializeField]
		private GameObject keyringPrefab;

		[SerializeField]
		private GameObject beltPrefab;


		public List<Item> items()
		{
			List<Item> _myItemList = new List<Item> ();

			_myItemList.Add (new Item (this.keyringPrefab, 1));
			_myItemList.Add (new Item (this.beltPrefab, 2));

			return _myItemList;
		}

		public Item playerItem()
		{
			return new Item (this.myCellphone, 1);
		}

	}
}

