using UnityEngine;
using System.Collections.Generic;
using StridersVR.Modules.SpeedPack.Logic.StrategyInterfaces;
using StridersVR.Domain.SpeedPack;
using StridersVR.ScriptableObjects.SpeedPack;

namespace StridersVR.Modules.SpeedPack.Logic.Strategies
{
	public class StrategySuitcaseCreation4x2 : IStrategySuitcaseCreation
	{
		private GameObject suitcaseContainer;

		private int numberOfSuitcaseParts = 2;
		private int numberOfItems = 4;

		private bool isTopInstance;

		private ScriptableObjectSuitcasePart suitcasePartData;
		private ScriptableObjectItem itemData;

		public StrategySuitcaseCreation4x2 (GameObject suitcaseContainer)
		{
			this.suitcaseContainer = suitcaseContainer;
		}


		#region IStrategySuitcaseCreation
		public Suitcase createSuitcase(ScriptableObject genericSuitcasePartData)
		{
			Suitcase _newSuitcase = new Suitcase ();

			this.suitcasePartData = (ScriptableObjectSuitcasePart)genericSuitcasePartData;	
			this.addIndicatedParts (ref _newSuitcase);

			_newSuitcase.setMainPart ();
			this.instantiateSuitcasePart (_newSuitcase);

			return _newSuitcase;
		}

		public void createItems(ScriptableObject genericItemsData, Suitcase currentSuitcase)
		{
			int _randomIndex;
			Item _newItem;
			List<Item> _itemList;
			SuitcasePart _mainPart = currentSuitcase.getMainPart ();

			this.itemData = (ScriptableObjectItem)genericItemsData;

			_itemList = this.itemData.items ();

			for (int counter = 0; counter < this.numberOfItems; counter ++) 
			{
				_randomIndex = Random.Range (0, _itemList.Count);
				_newItem = _itemList[_randomIndex];

				if(_newItem.Spacing == 2)
				{
					
				}
			}

		}
		#endregion

		private void instantiateSuitcasePart(Suitcase newSuitcase)
		{
			int _randomPointOrientation;
			GameObject _clone;

			foreach (SuitcasePart part in newSuitcase.SuitcasePartList) 
			{
				_randomPointOrientation = Random.Range(0, part.OrientationPointsCount);
				part.activeOrientationPoint(_randomPointOrientation);

				_clone = (GameObject)GameObject.Instantiate(part.SuitcasePartPrefab, Vector3.zero, Quaternion.Euler(Vector3.zero));
				_clone.transform.parent = this.suitcaseContainer.transform;
				_clone.transform.localPosition = Vector3.zero;
				_clone.GetComponent<SuitcasePartController>().LocalPart = part;

			}

		}

		private void addIndicatedParts(ref Suitcase newSuitcase)
		{
			for (int quantity = 0; quantity < this.numberOfSuitcaseParts; quantity ++) 
			{
				newSuitcase.addSuitcasePart(this.suitcasePartData.getSuitcasePart4x2 ());
			}
		}
	}
}

