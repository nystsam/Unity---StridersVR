using UnityEngine;
using System.Collections.Generic;
using StridersVR.Modules.DotToDot.Logic.StrategyInterfaces;
using StridersVR.Domain.DotToDot;

namespace StridersVR.Modules.DotToDot.Logic.Strategies
{
	public class StrategyCreateModelMedium : IStrategyCreateModel
	{
		private GameObject modelContainer;
		
		private Model currentModel;
		
		private int maxPoints = 12;
		private int maxStripes = 8;

		public StrategyCreateModelMedium (GameObject modelContainer)
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
			this.currentModel.createSelectedPoints(this.modelContainer,_startPoint);

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
				this.currentModel.createSelectedPoints(this.modelContainer,_endPoint);
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
			
			_xAxis = 1f;
			_yAxis = 20f;
			_zAxis = 0.5f;
			
			foreach(Point point in this.currentModel.Points.Values)
			{
				point.setPosition(new Vector3(_xAxis,_yAxis, _zAxis));
				_xAxis -= 1f;
				if(_xAxis < -1f)
				{
					_xAxis = 1f;
					_zAxis -= 1f;
					if(_zAxis < -0.5f)
					{
						_zAxis = 0.5f;
						_yAxis += 1.5f; 
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
	}
}

