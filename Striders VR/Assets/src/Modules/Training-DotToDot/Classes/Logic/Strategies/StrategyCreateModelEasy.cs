using UnityEngine;
using System.Collections.Generic;
using StridersVR.Modules.DotToDot.Logic.StrategyInterfaces;
using StridersVR.Domain.DotToDot;

namespace StridersVR.Modules.DotToDot.Logic.Strategies
{
	public class StrategyCreateModelEasy : IStrategyCreateModel
	{
		private GameObject modelContainer;

		private Model currentModel;

		private int maxPoints = 8;
		private int maxStripes = 5;

		public StrategyCreateModelEasy (GameObject modelContainer)
		{
			this.modelContainer = modelContainer;
		}

		#region IStrategyCreateModel
		public Model createModel()
		{
			this.currentModel = new Model();
			this.createPoints ();
			this.createPositions ();
			this.createStripes ();

			return this.currentModel;
		}
		#endregion

		private void createStripes()
		{
			Point _startPoint, _basePoint, _endPoint = null, _previousPoint = null;

			_basePoint = this.currentModel.Points [Random.Range (1, this.maxPoints + 1)];

			_startPoint = _basePoint;

			for(int count = 0; count < this.maxStripes; count++)
			{
				if(count + 1 != this.maxStripes)
				{
					_endPoint = this.getRandomPoint(_startPoint, _previousPoint);
				}
				else
				{
					if(_endPoint.PointId != _basePoint.PointId && !_basePoint.isAlreadyNeighbor(_endPoint))
					{
						_endPoint = _basePoint;
					}
					else
					{
						_endPoint = this.getRandomPoint(_startPoint, _endPoint);
					}
				}
				this.currentModel.addPointNeighbor(_startPoint, _endPoint);
				this.currentModel.createStripe(this.modelContainer, _startPoint, _endPoint);
				_previousPoint = _startPoint;
				_startPoint = _endPoint;
			}
		}

		private void createPoints()
		{
			Point _newPoint;

			for (int id = 1; id <= this.maxPoints; id ++)
			{
				_newPoint = new Point(id);
				this.currentModel.addPoint(_newPoint);
			}
		}

		private void createPositions()
		{
			float _xAxis, _yAxis, _zAxis;

			_xAxis = -1f;
			_yAxis = 20f;
			_zAxis = 1f;

			foreach(Point point in this.currentModel.Points.Values)
			{
				point.setPosition(new Vector3(_xAxis,_yAxis, _zAxis));
				_xAxis += 2f;
				if(_xAxis > 1f)
				{
					_xAxis = -1f;
					_zAxis -= 2f;
					if(_zAxis < -1f)
					{
						_zAxis = 1f;
						_yAxis += 2f; 
					}
				}

			}
		}

		private Point getRandomPoint(Point startPoint, Point previousPoint)
		{
			Point _endPoint;
			int _randomIndex;

			while(true)
			{
				_randomIndex = Random.Range(1, this.maxPoints + 1);	
				_endPoint = this.currentModel.Points[_randomIndex];
				if(_endPoint.PointId != startPoint.PointId)
				{
					if(previousPoint == null || previousPoint.PointId != _endPoint.PointId)
					{
						if(!startPoint.isAlreadyNeighbor(_endPoint))
						{
							break;
						}
					}
				}
			}

			return _endPoint;
		}

//		private GameObject figureContainer;
//		private GameObject stripeContainerPrefab;
//
//		private List<Vector3> edges;
//
//		private PointDot basePoint;
//		private PointDot previousPoint;
//		private PointDot currentPoint;
//
//		public StrategyCreateModelAbstractEasy (GameObject figureContainer, GameObject stripeContainerPrefab)
//		{
//			this.figureContainer = figureContainer;
//			this.stripeContainerPrefab = stripeContainerPrefab;
//
//			this.edges = new List<Vector3> ();
//			this.fillEdgesList ();
//
//			this.previousPoint = null;
//		}
//
//
//		#region IStrategyCreateModel
//		public void createModel(PointsContainer pointsFromController)
//		{
//			int _randomBasePosition = Random.Range (0, this.edges.Count);
//
//			this.basePoint = pointsFromController.findPoint (this.edges [_randomBasePosition]);
//
//			for (int numberOfStripes = 0; numberOfStripes < 4; numberOfStripes ++) 
//			{
//				this.setNextPoint (pointsFromController);
//				this.instatiateStripeModel (this.previousPoint, this.currentPoint);
//			}
//
//			this.instatiateStripeModel (this.previousPoint, this.basePoint);
//			pointsFromController.newStripe ();
//		}
//		#endregion
//
//		private void setNextPoint(PointsContainer pointsFromController)
//		{
//			if (this.previousPoint == null) 
//			{
//				Vector3 _newVectorPosition;
//				_newVectorPosition = -this.basePoint.PointPosition;
//
//				this.currentPoint = pointsFromController.findPoint (_newVectorPosition);
//				this.previousPoint = this.basePoint;
//			} 
//			else 
//			{
//				PointDot _newPoint;
//				int _randomIndex;
//
//
//				while(true)
//				{
//					_randomIndex = Random.Range(0, pointsFromController.numberOfPoints());
//					_newPoint = pointsFromController.getPointAtIndex(_randomIndex);
//
//					if(this.isAvailablePoint(_newPoint))
//						break;
//				}
//
//				this.currentPoint = _newPoint;
//			}
//
//			pointsFromController.newStripe ();
//		}
//
//		private void instatiateStripeModel(PointDot firstPoint, PointDot secondPoint)
//		{
//			GameObject _clone; 
//			Vector3 _scale = Vector3.one;
//			Vector3 _direction = (secondPoint.PointPosition - firstPoint.PointPosition).normalized;
//			Quaternion _lookDirection = Quaternion.LookRotation (_direction);
//
//			_clone = (GameObject)GameObject.Instantiate (this.stripeContainerPrefab, Vector3.zero, Quaternion.identity);
//			_clone.transform.parent = this.figureContainer.transform;
//			_clone.transform.localPosition = firstPoint.PointPosition;
//			_clone.transform.localRotation = _lookDirection;
//			
//			_scale.z = Vector3.Distance (firstPoint.PointPosition, secondPoint.PointPosition) / 2;
//			_clone.transform.GetChild (0).transform.localScale = _scale;
//			_clone.GetComponent<StripeModelController> ().setEndStripe ();
//			
//			firstPoint.addNeighbour (secondPoint.PointPosition);
//			firstPoint.Selected = true;
//			secondPoint.addNeighbour (firstPoint.PointPosition);
//
//			this.previousPoint = secondPoint;
//		}
//
//		private bool isAvailablePoint(PointDot possibleNewPoint)
//		{
//			if (possibleNewPoint.PointPosition.z != this.basePoint.PointPosition.z 
//				&& possibleNewPoint.PointPosition != this.previousPoint.PointPosition
//				&& possibleNewPoint.numberOfNeighbour() < 2)
//				return true;
//
//			return false;
//		}
//
//		private void fillEdgesList()
//		{
//			float _posZ = -15;
//			for (int i = 0; i < 2; i ++) 
//			{
//				this.edges.Add(new Vector3(-15,-15,_posZ));
//				this.edges.Add(new Vector3(15,-15,_posZ));
//				this.edges.Add(new Vector3(-15,15,_posZ));
//				this.edges.Add(new Vector3(15,15,_posZ));
//
//				_posZ *= -1;
//			}
//		}
	}
}

