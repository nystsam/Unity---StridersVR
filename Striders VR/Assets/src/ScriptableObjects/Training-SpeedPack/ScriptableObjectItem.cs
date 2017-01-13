using UnityEngine;
using System.Collections.Generic;
using StridersVR.Domain.SpeedPack;

namespace StridersVR.ScriptableObjects.SpeedPack
{
	public class ScriptableObjectItem : ScriptableObject
	{
		[SerializeField]
		private GameObject cellphonePrefab;

		[SerializeField]
		private GameObject keyringPrefab;

		[SerializeField]
		private GameObject diaryClockPrefab;

		[SerializeField]
		private GameObject combMirrorPrefab;

		[SerializeField]
		private GameObject flashLightPrefab;

		[SerializeField]
		private GameObject notebookPrefab;

		[SerializeField]
		private GameObject spatulaPrefab;

		[SerializeField]
		private GameObject headphonesPrefab;

		[SerializeField]
		private GameObject capPrefab;

		[SerializeField]
		private GameObject shoesPrefab;

		[SerializeField]
		private GameObject rubikPrefab;


		public List<Item> items()
		{
			List<Item> _myItemList = new List<Item> ();

			_myItemList.Add (new Item (this.keyringPrefab));
			_myItemList.Add (new Item (this.diaryClockPrefab));
			_myItemList.Add (new Item (this.combMirrorPrefab));
			_myItemList.Add (new Item (this.flashLightPrefab));
			_myItemList.Add (new Item (this.notebookPrefab));
			_myItemList.Add (new Item (this.spatulaPrefab));
			_myItemList.Add (new Item (this.headphonesPrefab));
			_myItemList.Add (new Item (this.capPrefab));
			_myItemList.Add (new Item (this.shoesPrefab));
			_myItemList.Add (new Item (this.cellphonePrefab));

			return _myItemList;
		}

		public Item playerItem()
		{
			return new Item (this.rubikPrefab);
		}

	}
}

