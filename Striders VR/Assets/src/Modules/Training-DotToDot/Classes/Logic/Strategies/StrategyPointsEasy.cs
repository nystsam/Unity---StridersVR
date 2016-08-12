using UnityEngine;
using StridersVR.Modules.DotToDot.Logic.StrategyInterfaces;
using StridersVR.Domain.DotToDot;

namespace StridersVR.Modules.DotToDot.Logic.Strategies
{
	public class StrategyPointsEasy : IStrategyPoints
	{
		private GameObject dotContainer;
		private GameObject pointPrefab;

		private int distanceSeparation = 25;
		private int pointId = 1;

		public StrategyPointsEasy (GameObject dotContainer, GameObject pointPrefab)
		{
			this.dotContainer = dotContainer;
			this.pointPrefab = pointPrefab;
		}


		#region IStrategyPoints
		public PointsContainer createPoints()
		{
			PointsContainer _pointsContainer = new PointsContainer();
			int _positionZ = -this.distanceSeparation/2;

			for (int vectorZ = 0; vectorZ < 2; vectorZ ++) 
			{
				this.drawRect(_positionZ, _pointsContainer);
				_positionZ += this.distanceSeparation;
			}

			return _pointsContainer;
		}
		#endregion

		private void drawRect(int vectorZ, PointsContainer pointsContainer)
		{
			GameObject _newDot;
			PointDot _newPoint;

			for (int vectorY = 0; vectorY < this.distanceSeparation * 3; vectorY += this.distanceSeparation) 
			{
				for(int vectorX = 0; vectorX < this.distanceSeparation * 3; vectorX += this.distanceSeparation)
				{
					_newDot = (GameObject)GameObject.Instantiate(this.pointPrefab, Vector3.zero, Quaternion.Euler(Vector3.zero));	
					_newDot.transform.parent = this.dotContainer.transform;
					_newDot.transform.localPosition = new Vector3(vectorX-this.distanceSeparation, vectorY-this.distanceSeparation, vectorZ );

					_newPoint = new PointDot(this.pointId, _newDot.transform.localPosition);
					pointsContainer.addPoint(_newPoint);

					_newDot.GetComponentInChildren<PointController>().setLocalPointDot(_newPoint);
					this.pointId ++;
				}
			}
		}
	}
}

