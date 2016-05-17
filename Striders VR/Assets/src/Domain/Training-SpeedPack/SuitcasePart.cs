using System.Collections.Generic;
using UnityEngine;

namespace StridersVR.Domain.SpeedPack
{
	[System.Serializable]
	public class SuitcasePart
	{
		[SerializeField]
		private GameObject suitcasePartPrefab;

		private Spot[,] spotMatrix;

		private SuitcasePart attachedPart;

		private List<OrientationPoint> orientationPoints;

		private bool isMainPart;

		public SuitcasePart (GameObject prefab)
		{
			this.suitcasePartPrefab = prefab;
			this.attachedPart = null;
			this.isMainPart = false;

			this.orientationPoints = new List<OrientationPoint> ();
		}


		public void findSpotMatrixIndex(Spot spot, ref int xIndex, ref int yIndex)
		{
			bool _flag = false;

			for (int x = 0; x < this.spotMatrix.GetLength(0); x ++) 
			{
				for (int y = 0; y < this.spotMatrix.GetLength(1); y ++)
				{
					if(this.spotMatrix[x,y].SpotId == spot.SpotId )
					{
						xIndex = x;
						yIndex = y;
						_flag = true;
						break;
					}
				}
				if(_flag)
				{
					break;
				}
			}
		}

		public Spot getSpotAtIndex(int indexX, int indexY)
		{
			return this.spotMatrix [indexX, indexY];
		}

		public List<Spot> getUsedSpots()
		{
			List<Spot> _newSpotList = new List<Spot> ();

			foreach (Spot usedSpot in this.spotMatrix) 
			{
				if(!usedSpot.IsAvailableSpot)
				{
					_newSpotList.Add(usedSpot);
				}
			}

			return _newSpotList;
		}		

		public bool placeItem(Item newItem)
		{
			int _xAxis = 0, _yAxis = 0;

			if (this.getSpotIndex (ref _xAxis, ref _yAxis)) 
			{
				this.spotMatrix [_xAxis, _yAxis].setItem (newItem);
				return true;
			}


//			if (newItem.Spacing == 2) 
//			{
//				int _axisRotation = Random.Range (0, 2);
//				int _newPos;
//
//				// Eje x
//				if (_axisRotation == 0) 
//				{
//					_newPos = this.rotateItem (this.spotMatrix.GetLength (0), _xAxis);
//					if (this.spotMatrix [_newPos, _yAxis].IsAvailableSpot) {
//						this.spotMatrix [_xAxis, _yAxis].setItem (newItem);
//						this.spotMatrix [_newPos, _yAxis].setItem (newItem);
//					} else {
//						return false;
//					}
//
//				}
//				// Eje y
//				else 
//				{
//					_newPos = this.rotateItem (this.spotMatrix.GetLength (1), _yAxis);
//					if (this.spotMatrix [_xAxis, _newPos].IsAvailableSpot) {
//						this.spotMatrix [_xAxis, _yAxis].setItem (newItem);
//						this.spotMatrix [_xAxis, _newPos].setItem (newItem);
//					} else {
//						return false;
//					}
//				}
//			} 
//			else 
//			{
//				this.spotMatrix [_xAxis, _yAxis].setItem (newItem);
//			}

			return false;
		}

		public void activeOrientationPoint(int index)
		{
			this.orientationPoints [index].activePoint ();
		}

		public OrientationPoint getActivePoint()
		{
			foreach (OrientationPoint point in this.orientationPoints) 
			{
				if(point.IsActive)
				{
					return point;
				}
			}

			return null;
		}

		public void setAttachedPart(SuitcasePart part)
		{
			this.attachedPart = part;
		}

		public void setMainPart()
		{
			this.isMainPart = true;
		}

		public void setSpotsFromData(int dimesionX, int dimensionY)
		{
			int _xIndex = 0, _yIndex = 0;
			float _xPosition, _zPosition;
			Transform _spotsFromPrefab;

			this.spotMatrix = new Spot[dimesionX, dimensionY];

			_spotsFromPrefab = this.suitcasePartPrefab.transform.GetChild(0).FindChild("Spots");

			for (int _indexGameSpot = 0; _indexGameSpot < _spotsFromPrefab.childCount; _indexGameSpot ++) 
			{
				_xPosition = _spotsFromPrefab.GetChild(_indexGameSpot).localPosition.x;
				_zPosition = _spotsFromPrefab.GetChild(_indexGameSpot).localPosition.z;

				this.spotMatrix[_xIndex, _yIndex] = new Spot(_indexGameSpot,
				                                             _spotsFromPrefab.GetChild(_indexGameSpot).gameObject,
				                                             new Vector3(_xPosition, 0.1f, _zPosition));

				_xIndex ++;

				if(_xIndex >= dimesionX)
				{
					_yIndex ++;
					_xIndex = 0;
				}
			}

			this.setAttachedPoints ();
		}

		private void setAttachedPoints()
		{
			Vector3 _pointPosition, _pointRotation;
			Transform _points;
			OrientationPoint _newPoint;

			_points = this.suitcasePartPrefab.transform.GetChild (1);

			for (int _indexGameSpot = 0; _indexGameSpot < _points.childCount; _indexGameSpot ++) 
			{
				_pointPosition = _points.GetChild(_indexGameSpot).localPosition;
				_pointRotation = _points.GetChild(_indexGameSpot).rotation.eulerAngles;

				_newPoint = new OrientationPoint(_pointPosition, _pointRotation);

				this.orientationPoints.Add(_newPoint);
			}
		}

		private bool getSpotIndex(ref int xAxis, ref int yAxis)
		{
			int _constraint = 0;

			while (true) 
			{
				xAxis = Random.Range (0, this.spotMatrix.GetLength (0));
				yAxis = Random.Range (0, this.spotMatrix.GetLength (1));
				
				if(this.spotMatrix[xAxis, yAxis].IsAvailableSpot)
					return true;
				else if(_constraint >= this.spotMatrix.Length*2)
					return false;

				_constraint ++;
			}
		}

//		private int rotateItem(int max, int currentPos)
//		{
//			int _direction = 1;
//
//			if (max - currentPos <= max / 2)
//				_direction = -_direction;
//
//			return currentPos + _direction;
//		}
		
		#region Properties
		public bool IsMainPart
		{
			get { return this.isMainPart; }
		}

		public GameObject SuitcasePartPrefab
		{
			get { return this.suitcasePartPrefab; }
		}

		public SuitcasePart AttachedPart
		{
			get { return this.attachedPart; }
		}

		public int OrientationPointsCount
		{
			get { return this.orientationPoints.Count; }
		}

		public int MatrixMaxLengthX
		{
			get { return this.spotMatrix.GetLength(0); }
		}

		public int MatrixMaxLengthY
		{
			get { return this.spotMatrix.GetLength(1); }
		}
		#endregion
	}
}

