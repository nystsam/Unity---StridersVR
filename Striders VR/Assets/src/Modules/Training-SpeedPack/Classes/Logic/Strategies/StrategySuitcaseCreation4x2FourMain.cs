using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using StridersVR.Modules.SpeedPack.Logic.StrategyInterfaces;
using StridersVR.Domain.SpeedPack;
using StridersVR.ScriptableObjects.SpeedPack;

namespace StridersVR.Modules.SpeedPack.Logic.Strategies
{
	public class StrategySuitcaseCreation4x2FourMain : IStrategySuitcaseCreation
	{
		private GameObject suitcaseContainer;
		private GameObject inGameSuitcasePart;
		
		private int numberOfSuitcaseParts = 4;
		private int numberOfItems = 7;
		private int score = 425;
		
		private ScriptableObjectSuitcasePart suitcasePartData;
		private ScriptableObjectItem itemData;
		
		private List<OrientationPoint> orientationPoints;
		
		private List<Spot> selectedSpots;

		public StrategySuitcaseCreation4x2FourMain (GameObject suitcaseContainer)
		{
			this.suitcaseContainer = suitcaseContainer;
		}


		#region IStrategySuitcaseCreation
		public Suitcase createSuitcase(ScriptableObject genericSuitcasePartData)
		{
			Suitcase _newSuitcase = new Suitcase ();
			
			this.orientationPoints = new List<OrientationPoint> ();
			this.suitcasePartData = (ScriptableObjectSuitcasePart)genericSuitcasePartData;	
			this.addIndicatedParts (ref _newSuitcase);
			
			_newSuitcase.setSuicaseScore (this.score);
			
			this.orientationPoints = _newSuitcase.getMainPart ().OrientationPoints;
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
					this.numberOfItems = _mainPart.getUsedSpots ().Count;
					break;
				}
			}
			
			this.selectedSpots = _mainPart.getUsedSpots ();
		}
		
		public void createItem (Suitcase currentSuitcase)
		{
			int _itemsInThisPart = 0;
			int _totalItems = this.numberOfItems;
			
			SuitcasePart _currentPart;
			
			for (int partIndex = 0; partIndex < this.numberOfSuitcaseParts; partIndex ++) 
			{
				if(partIndex == this.numberOfSuitcaseParts - 1)
				{
					_itemsInThisPart = _totalItems;
				}
				else
				{
					_itemsInThisPart = Random.Range(1,3);
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
				currentPart.findSpotMatrixIndex(_spot, ref _currentX, ref _currentY);
				this.instantiateItem(currentPart, _spot, _currentX, _currentY);
				this.selectedSpots.Remove(_spot);
			}
		}
		
		private void instantiateItem(SuitcasePart currentPart, Spot currentSpot, int currentX, int currentY)
		{
			Spot _newSpot;
			Vector3 _currentPosition = Vector3.zero;
			GameObject _clone;
			
			if (currentPart.AttachedPart != null) 
			{
				currentPart.AttachedPart.getOppositeIndex (currentPart.AttachedOrientation, ref currentX, ref currentY);
			} 
			
			_newSpot = currentPart.getSpotAtIndex (currentX, currentY);
			
			if (currentPart.IsMainPart) 
			{
				_newSpot.setMainSpot();
			}
			
			_currentPosition.y = currentSpot.SpotPosition.y;
			_currentPosition.x = _newSpot.SpotPosition.x;
			_currentPosition.z = _newSpot.SpotPosition.z;
			_clone = (GameObject)GameObject.Instantiate (currentSpot.CurrentItem.ItemPrefab,
			                                             Vector3.zero,
			                                             Quaternion.Euler (Vector3.zero));
			_clone.transform.parent = this.inGameSuitcasePart.transform.Find ("SuitcasePart").Find ("Items");
			_clone.transform.localPosition = _currentPosition;
			_newSpot.setItem(currentSpot.CurrentItem);
		}
		
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
				if(part.AttachedPart != null)
				{
					while(true)
					{
						_randomPointOrientation = Random.Range(0, part.OrientationPointsCount);
						
						if(this.orientationPoints[_randomPointOrientation].IsAvailable)
						{
							part.setAttachedOrientation(this.orientationPoints[_randomPointOrientation]);
							this.orientationPoints[_randomPointOrientation].changeAvailability();
							
							break;
						}
					}
				}
				
				_clone = (GameObject)GameObject.Instantiate(part.SuitcasePartPrefab, Vector3.zero, Quaternion.Euler(Vector3.zero));
				_clone.transform.parent = this.suitcaseContainer.transform;
				_clone.transform.localPosition = Vector3.zero;
				_clone.GetComponent<SuitcasePartController>().LocalPart = part;
				
				if(part.IsMainPart)
				{
					_clone.GetComponent<SuitcasePartController>().changeMainPartColor();
				}
				else
					_clone.GetComponent<SuitcasePartController>().assignSpots();
			}
			
		}
		
		private bool checkDiferentOrientation(OrientationPoint previous, int _randomIndex)
		{
			if ((this.orientationPoints [_randomIndex].IsHorizontal && !previous.IsHorizontal) ||
			    (!this.orientationPoints [_randomIndex].IsHorizontal && previous.IsHorizontal))
				return true;
			return false;
		}
		
		private void addIndicatedParts(ref Suitcase newSuitcase)
		{		
			for (int quantity = 0; quantity < this.numberOfSuitcaseParts; quantity ++) 
			{
				newSuitcase.addSuitcasePartMain(this.suitcasePartData.getSuitcasePart4x2 ());
			}
		}
		
	}
}

