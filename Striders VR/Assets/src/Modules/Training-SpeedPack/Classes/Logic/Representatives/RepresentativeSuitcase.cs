using UnityEngine;
using System.Collections.Generic;
using StridersVR.Domain.SpeedPack;
using StridersVR.Modules.SpeedPack.Logic.Contexts;
using StridersVR.Modules.SpeedPack.Logic.Strategies;

namespace StridersVR.Modules.SpeedPack.Logic.Representatives
{
	public class RepresentativeSuitcase 
	{
		private GameObject suitcaseContainer;

		private ScriptableObject suitcasePartData;
		private ScriptableObject itemData;

		private ContextSuitcaseCreation contextSuitcaseCreation;

		public RepresentativeSuitcase(GameObject suitcaseContainer)
		{
			this.suitcaseContainer = suitcaseContainer;
			this.contextSuitcaseCreation = new ContextSuitcaseCreation ();

//			this.contextSuitcaseCreation.StrategyGenerateObjects = new StrategySuitcaseCreation4x2 (this.suitcaseContainer);
			this.contextSuitcaseCreation.StrategyGenerateObjects = new StrategySuitcaseCreation4x2Three (this.suitcaseContainer);
		}


		public Suitcase getSuitcase()
		{
			return this.contextSuitcaseCreation.createSuitcase (this.suitcasePartData);
		}

		public void spawnItems(Suitcase currentSuitcase)
		{
			this.contextSuitcaseCreation.assignItemsMain (this.itemData,currentSuitcase);
			this.contextSuitcaseCreation.createItem (currentSuitcase);
		}

		public void spawnPlayerItem()
		{
			GameObject _itemPrefab = Resources.Load("Prefabs/Training-SpeedPack/ItemCellphone", typeof(GameObject)) as GameObject;
			GameObject _clone;

			_clone = (GameObject)GameObject.Instantiate (_itemPrefab, Vector3.zero, Quaternion.Euler (Vector3.zero));
			_clone.transform.parent = this.suitcaseContainer.transform.parent;
			_clone.transform.localPosition = new Vector3 (1.17f, 1.27f, 1.015f);
			_clone.transform.localRotation = Quaternion.Euler (new Vector3 (295, -90, 0));
		}

		#region Properties
		public ScriptableObject SetPartData
		{
			set { this.suitcasePartData = value; }
		}

		public ScriptableObject SetItemData
		{
			set { this.itemData = value; }
		}
		#endregion
	}
}
