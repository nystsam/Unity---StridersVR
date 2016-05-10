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


		public void placeItem(Item newItem)
		{
			int _xAxis, _yAxis;

			_xAxis = Random.Range (0, this.spotMatrix.GetLength (0));
			_yAxis = Random.Range (0, this.spotMatrix.GetLength (1));

			if (newItem.Spacing == 2) 
			{
				int axisRotation = Random.Range(0,2);
				//spotMatrix.
			}
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
			/* CAMBIAR COLOR DEL FONDO */
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

				this.spotMatrix[_xIndex, _yIndex] = new Spot(_spotsFromPrefab.GetChild(_indexGameSpot).gameObject
				                                             ,new Vector3(_xPosition, 0.1f, _zPosition));

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
		#endregion
	}
}

