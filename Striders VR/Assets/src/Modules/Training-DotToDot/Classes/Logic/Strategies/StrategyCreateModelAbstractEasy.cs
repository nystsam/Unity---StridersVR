using UnityEngine;
using System.Collections.Generic;
using StridersVR.Modules.DotToDot.Logic.StrategyInterfaces;
using StridersVR.Domain.DotToDot;

namespace StridersVR.Modules.DotToDot.Logic.Strategies
{
	public class StrategyCreateModelAbstractEasy : IStrategyCreateModel
	{
		private GameObject figureContainer;
		private GameObject stripeContainerPrefab;

		private List<Vector3> edges;

		private PointDot basePoint;
		private PointDot previousPoint;
		private PointDot currentPoint;

		public StrategyCreateModelAbstractEasy (GameObject figureContainer, GameObject stripeContainerPrefab)
		{
			this.figureContainer = figureContainer;
			this.stripeContainerPrefab = stripeContainerPrefab;

			this.edges = new List<Vector3> ();
			this.fillEdgesList ();

			this.previousPoint = null;
		}


		#region IStrategyCreateModel
		public void createModel(PointsContainer pointsFromController)
		{
			int _randomBasePosition = Random.Range (0, this.edges.Count);

			this.basePoint = pointsFromController.findPoint (this.edges [_randomBasePosition]);

			for (int numberOfStripes = 0; numberOfStripes < 4; numberOfStripes ++) 
			{
				this.setNextPoint (pointsFromController);
				this.instatiateStripeModel (this.previousPoint, this.currentPoint);
			}

			this.instatiateStripeModel (this.previousPoint, this.basePoint);
		}
		#endregion

		private void setNextPoint(PointsContainer pointsFromController)
		{
			if (this.previousPoint == null) 
			{
				Vector3 _newVectorPosition;
				_newVectorPosition = -this.basePoint.PointPosition;

				this.currentPoint = pointsFromController.findPoint (_newVectorPosition);
				this.previousPoint = this.basePoint;
			} 
			else 
			{
				PointDot _newPoint;
				int _randomIndex;


				while(true)
				{
					_randomIndex = Random.Range(0, pointsFromController.numberOfPoints());
					_newPoint = pointsFromController.getPointAtIndex(_randomIndex);

					if(this.isAvailablePoint(_newPoint))
						break;
				}

				this.currentPoint = _newPoint;
			}

			pointsFromController.newStripe ();
		}

		private void instatiateStripeModel(PointDot firstPoint, PointDot secondPoint)
		{
			GameObject _clone; 
			Vector3 _scale = Vector3.one;
			Vector3 _direction = (secondPoint.PointPosition - firstPoint.PointPosition).normalized;
			Quaternion _lookDirection = Quaternion.LookRotation (_direction);

			_clone = (GameObject)GameObject.Instantiate (this.stripeContainerPrefab, Vector3.zero, Quaternion.identity);
			_clone.transform.parent = this.figureContainer.transform;
			_clone.transform.localPosition = firstPoint.PointPosition;
			_clone.transform.localRotation = _lookDirection;
			
			_scale.z = Vector3.Distance (firstPoint.PointPosition, secondPoint.PointPosition) / 2;
			_clone.transform.GetChild (0).transform.localScale = _scale;
			_clone.GetComponent<StripeModelController> ().setEndStripe ();
			
			firstPoint.addNeighbour (secondPoint.PointPosition);
			firstPoint.Selected = true;
			secondPoint.addNeighbour (firstPoint.PointPosition);

			this.previousPoint = secondPoint;
		}

		private bool isAvailablePoint(PointDot possibleNewPoint)
		{
			if (possibleNewPoint.PointPosition.z != this.basePoint.PointPosition.z 
				&& possibleNewPoint.PointPosition != this.previousPoint.PointPosition
				&& possibleNewPoint.numberOfNeighbour() < 2)
				return true;

			return false;
		}

		private void fillEdgesList()
		{
			float _posZ = -15;
			for (int i = 0; i < 2; i ++) 
			{
				this.edges.Add(new Vector3(-15,-15,_posZ));
				this.edges.Add(new Vector3(15,-15,_posZ));
				this.edges.Add(new Vector3(-15,15,_posZ));
				this.edges.Add(new Vector3(15,15,_posZ));

				_posZ *= -1;
			}
		}
	}
}

