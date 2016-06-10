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

		private StrategySuitcaseCreationComposite strategyComposite;

		public RepresentativeSuitcase(GameObject suitcaseContainer, string dificulty)
		{
			this.suitcaseContainer = suitcaseContainer;
			this.contextSuitcaseCreation = new ContextSuitcaseCreation ();

			this.createStrategies (dificulty);
		}


		public Suitcase getSuitcase()
		{
			int _randomStrategy = Random.Range(0, this.contextSuitcaseCreation.strategyCompositeCount());

			this.contextSuitcaseCreation.strategyCompositeIndex (_randomStrategy);

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
			_clone.transform.localPosition = new Vector3 (1.202f, 1.227f, 0.978f);
			_clone.transform.localRotation = Quaternion.Euler (new Vector3 (275, 270, 29));
		}

		private void createStrategies(string dificulty)
		{
			this.strategyComposite = new StrategySuitcaseCreationComposite ();

			if(dificulty.Equals("Easy"))
			{
				this.strategyComposite.addStrategy (new StrategySuitcaseCreation2x2 (this.suitcaseContainer));
				this.strategyComposite.addStrategy (new StrategySuitcaseCreation3x2 (this.suitcaseContainer));
				this.strategyComposite.addStrategy (new StrategySuitcaseCreation2x2Three (this.suitcaseContainer));
			}
			else if (dificulty.Equals ("Medium")) 
			{
				this.strategyComposite.addStrategy (new StrategySuitcaseCreation4x2 (this.suitcaseContainer));
				this.strategyComposite.addStrategy (new StrategySuitcaseCreation4x2Three (this.suitcaseContainer));
				this.strategyComposite.addStrategy (new StrategySuitcaseCreation3x2FourMain (this.suitcaseContainer));
				this.strategyComposite.addStrategy (new StrategySuitcaseCreation3x3NotFull (this.suitcaseContainer));
			}
			else if (dificulty.Equals("Hard"))
			{
				this.strategyComposite.addStrategy (new StrategySuitcaseCreation4x2FourMain (this.suitcaseContainer));
				this.strategyComposite.addStrategy (new StrategySuitcaseCreation3x3 (this.suitcaseContainer));
				this.strategyComposite.addStrategy (new StrategySuitcaseCreation3x3Three (this.suitcaseContainer));
				this.strategyComposite.addStrategy (new StrategySuitcaseCreation3x3FourMain (this.suitcaseContainer));
			}

			this.contextSuitcaseCreation.assignStrategy(this.strategyComposite);
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
