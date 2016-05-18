using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using StridersVR.Modules.SpeedPack.Logic.StrategyInterfaces;
using StridersVR.Domain.SpeedPack;
using StridersVR.ScriptableObjects.SpeedPack;

namespace StridersVR.Modules.SpeedPack.Logic.Strategies
{
	public class StrategySuitcaseCreation4x2 : IStrategySuitcaseCreation
	{
		private GameObject suitcaseContainer;
		private GameObject inGameSuitcasePart;

		private int numberOfSuitcaseParts = 2;
		private int numberOfItems = 4;

		private bool isTopInstance;

		private ScriptableObjectSuitcasePart suitcasePartData;
		private ScriptableObjectItem itemData;

		private SuitcasePart mainPart;

		private List<Spot> selectedSpots;

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

		public void assignItemsMain(ScriptableObject genericItemsData, Suitcase currentSuitcase)
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
				_newItem.ItemId = counter + 1;
				if(!_mainPart.placeItem(_newItem))
				{
					Debug.Log ("No mas disponibles");
					break;
				}
			}

			this.selectedSpots = _mainPart.getUsedSpots ();

		}

		public void createItem (Suitcase currentSuitcase)
		{
			int _itemsInThisPart;
			int _totalItems = this.numberOfItems;

			SuitcasePart _currentPart;

			this.mainPart = currentSuitcase.getMainPart ();

			for (int partIndex = 0; partIndex < this.numberOfSuitcaseParts; partIndex ++) 
			{
				if(_totalItems == this.numberOfItems || _totalItems >= this.numberOfItems/2 && partIndex != this.numberOfSuitcaseParts - 1)
				{
					_itemsInThisPart = Random.Range(1,_totalItems);
				}
				else
				{
					_itemsInThisPart = _totalItems;
				}

				_totalItems -= _itemsInThisPart;
				_currentPart = currentSuitcase.getPartAtIndex(partIndex);
				this.getInGameSuitcasePart(_currentPart);
				this.itemsInPart(_currentPart, _itemsInThisPart);

			}
		}
		#endregion

		private void itemsInPart(SuitcasePart currentPart,int itemsInThisPart)
		{
			int _currentX = 0, _currentY = 0;
			Spot _spot;

			for(int counter = 0; counter < itemsInThisPart; counter ++)
			{
				_spot = this.selectedSpots.Last();
				this.mainPart.findSpotMatrixIndex(_spot, ref _currentX, ref _currentY);
				this.instantiateItem(currentPart, _spot, _currentX, _currentY);
				this.selectedSpots.Remove(_spot);
			}
		}
		
		private void instantiateItem(SuitcasePart currentPart, Spot currentSpot, int currentX, int currentY)
		{
			Spot _newSpot;
			OrientationPoint _orientationPoint;
			Vector3 _currentPosition = Vector3.zero;
			Vector3 _gamePosition;
			GameObject _clone;

			if(currentPart.AttachedPart != null)
			{
				_orientationPoint = currentPart.getActivePoint();
				if(_orientationPoint.AttachedPosition.x != 0)
				{
					// izquierda o derecha
					currentX =  (currentPart.MatrixMaxLengthX - 1) - currentX;
					
				}
				else
				{
					// arriba o abajo
					currentY = (currentPart.MatrixMaxLengthY - 1) - currentY;
				}
			}
			
			_newSpot = currentPart.getSpotAtIndex (currentX, currentY);

			_currentPosition.y = currentSpot.SpotPosition.y;
			_currentPosition.x = _newSpot.SpotPosition.x;
                	_currentPosition.z = _newSpot.SpotPosition.z;
			_clone = (GameObject)GameObject.Instantiate (currentSpot.CurrentItem.ItemPrefab,
			                                            Vector3.zero,
			                                            Quaternion.Euler (Vector3.zero));
			_clone.transform.parent = this.inGameSuitcasePart.transform.Find ("SuitcasePart").Find ("Items");
			_clone.transform.localPosition = _currentPosition;

//			if (currentSpot.CurrentItem.Spacing == 2) 
//			{
//				Vector3 _neighbourPosition = this.getNeighbourVector(currentPart, currentSpot);
//				_currentPosition.x = (_neighbourPosition.x + _newSpot.SpotPosition.x) / 2;
//				_currentPosition.z = (_neighbourPosition.z + _newSpot.SpotPosition.z) / 2;
//				_clone.transform.localPosition = _currentPosition;
//
//				//_clone.transform.localRotation = Quaternion.LookRotation(new Vector3(270,_neighbourPosition.y,0));
//			} 
//			else 
//			{
//				_currentPosition.x = _newSpot.SpotPosition.x;
//				_currentPosition.z = _newSpot.SpotPosition.z;
//				_clone.transform.localPosition = _currentPosition;
//			}
		}

//		private Vector3 getNeighbourVector(SuitcasePart currentPart,Spot currentSpot)
//		{
//			Spot _secondSpot;
//			Vector3 _secondSpotPosition;
//			OrientationPoint _orientationPoint;
//			int _secondX = 0, _secondY = 0;
//
//			_secondSpot = this.selectedSpots.Find (spot => spot.CurrentItem.ItemId == currentSpot.CurrentItem.ItemId &&
//			                                        spot.SpotPosition != currentSpot.SpotPosition);
//
//			this.mainPart.findSpotMatrixIndex (_secondSpot, ref _secondX, ref _secondY);
//			if(currentPart.AttachedPart != null)
//			{
//				_orientationPoint = currentPart.getActivePoint();
//				if(_orientationPoint.AttachedPosition.x != 0)
//				{
//					// izquierda o derecha
//					_secondX =  (currentPart.MatrixMaxLengthX - 1) - _secondX;
//					
//				}
//				else
//				{
//					// arriba o abajo
//					_secondY = (currentPart.MatrixMaxLengthY - 1) - _secondY;
//				}
//			}
//			this.selectedSpots.Remove (_secondSpot);
//
//			return currentPart.getSpotAtIndex (_secondX, _secondY).SpotPosition;
//		}


		private void getInGameSuitcasePart(SuitcasePart currentPart)
		{
			GameObject[] _parts;

			_parts = GameObject.FindGameObjectsWithTag("SuitcasePart");

			foreach (GameObject _inGamePart in _parts) 
			{
				if(_inGamePart.GetComponent<SuitcasePartController>().LocalPart == currentPart)
				{
					this.inGameSuitcasePart = _inGamePart;
					break;
				}
			}
		}

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

				if(part.IsMainPart)
				{
					_clone.GetComponent<SuitcasePartController>().changeMainPartColor();
				}
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

