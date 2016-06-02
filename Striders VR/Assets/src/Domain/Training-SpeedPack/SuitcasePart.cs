using System.Collections.Generic;
using UnityEngine;

namespace StridersVR.Domain.SpeedPack
{
	[System.Serializable]
	public class SuitcasePart
	{
		[SerializeField]
		private GameObject suitcasePartPrefab;

		private Vector3 gamePosition;

		private Spot[,] spotMatrix;
		private List<Spot> spotList;

		private SuitcasePart attachedPart;

		private OrientationPoint currentOrientation;
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

		public void calculateIndex(OrientationPoint orientation, ref  int currentX, ref int currentY)
		{
			if(orientation.AttachedPosition.x != 0)
			{
				// izquierda o derecha
				currentX =  (this.MatrixMaxLengthX - 1) - currentX;
				
			}
			else
			{
				// arriba o abajo
				currentY = (this.MatrixMaxLengthY - 1) - currentY;
			}
		}

		public void getOppositeIndex(OrientationPoint orientation, ref int currentX, ref int currentY)
		{
			if (this.attachedPart != null) 
			{

				this.attachedPart.getOppositeIndex(this.AttachedOrientation, ref currentX, ref currentY);
				this.calculateIndex(orientation, ref currentX, ref currentY);
			}
			else
			{
				this.calculateIndex(orientation, ref currentX, ref currentY);
			}
		}

		public Spot getSpotAtIndex(int indexX, int indexY)
		{
			return this.spotMatrix [indexX, indexY];
		}

		public Spot getSpotAtIndex(int index)
		{
			return this.spotList[index];
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

			return false;
		}

		public void setAttachedOrientation(OrientationPoint orientation)
		{
			this.currentOrientation = orientation;
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

		public Vector3 getGamePosition(Vector3 position)
		{
			if (this.attachedPart != null) 
			{
				position += this.attachedPart.getGamePosition (position);
				return this.gamePosition + position;
			} 
			else
			{
				return this.gamePosition;
			}
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
			this.spotList = new List<Spot>();

			_spotsFromPrefab = this.suitcasePartPrefab.transform.GetChild(0).FindChild("Spots");

			for (int _indexGameSpot = 0; _indexGameSpot < _spotsFromPrefab.childCount; _indexGameSpot ++) 
			{
				_xPosition = _spotsFromPrefab.GetChild(_indexGameSpot).localPosition.x;
				_zPosition = _spotsFromPrefab.GetChild(_indexGameSpot).localPosition.z;

				this.spotMatrix[_xIndex, _yIndex] = new Spot(_indexGameSpot,
				                                             _spotsFromPrefab.GetChild(_indexGameSpot).gameObject,
				                                             new Vector3(_xPosition, 0.1f, _zPosition));

				this.spotList.Add(this.spotMatrix[_xIndex,_yIndex]);
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

				_newPoint = new OrientationPoint(_indexGameSpot + 1, _pointPosition, _pointRotation);

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

		#region Properties
		public Vector3 GamePosition
		{
			get { return this.gamePosition; }
			set { this.gamePosition = value; }
		}

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

		public OrientationPoint AttachedOrientation
		{
			get { return this.currentOrientation; }
		}

		public List<OrientationPoint> OrientationPoints
		{
			get { return this.orientationPoints; }
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

