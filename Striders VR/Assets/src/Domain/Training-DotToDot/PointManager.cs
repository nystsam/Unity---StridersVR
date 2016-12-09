using System;
using UnityEngine;

namespace StridersVR.Domain.DotToDot
{
	public class PointManager
	{
		private GameObject pointsContainer;
		private GameObject stripePrefab;
		private GameObject endStripePrefab;
		private GameObject currentStripe;

		private Point currentPoint = null;
		private Point nextPoint = null;
		private Point previousPoint = null;

		private Model currentModel = null;

		private RedoCollector collector;

		private bool isFirstPoint;

		private int errorCount;
		private int stripesPlaced;

		public PointManager (GameObject pointsContainer)
		{
			this.pointsContainer = pointsContainer;
			this.stripePrefab = Resources.Load ("Prefabs/Training-DotToDot/Stripe", typeof(GameObject)) as GameObject;
			this.endStripePrefab = Resources.Load ("Prefabs/Training-DotToDot/EndPointStripe", typeof(GameObject)) as GameObject;
			this.isFirstPoint = true;
			this.errorCount = -1;
			this.stripesPlaced = 0;
		}


		#region In-Game features
		/// <summary>
		/// Instantiates the current points in the game.
		/// </summary>
		/// <param name="container">Container that stores all game points</param>
		public void instantiatePoints()
		{
			GameObject _gamePoint;

			foreach(Point _point in this.currentModel.Points.Values)
			{
				_gamePoint = (GameObject)GameObject.Instantiate(_point.PointPrefab, Vector3.zero, Quaternion.Euler(Vector3.zero));
				_gamePoint.transform.parent = this.pointsContainer.transform;
				_gamePoint.transform.localPosition = _point.Position;
				_gamePoint.GetComponent<PointController>().setLocalPoint(_point);
			}
		}
		#endregion

		public bool finishModel()
		{
			//Deshabilitar los puntos
			if(this.stripesPlaced == this.currentModel.StripesCount  && this.errorCount <= 0)
			{
				return true;
			}

			return false;
		}

		public bool setTouchingPoint(Point point)
		{
			if(this.isFirstPoint)
			{
				this.isFirstPoint = false;
				this.collector = new RedoCollector ();
				this.currentPoint = point;
				this.createNewStripe(this.currentPoint.Position);
				this.currentPoint.turnOn();

				return true;
			}

			return false;
		}

		public bool isAvailablePoint(Point point)
		{
			if (point != this.currentPoint) 
			{
				if(point != this.previousPoint)
				{
					this.nextPoint = point;
					return true;
				}
			}

			return false;
		}
		
		public void placeStripe()
		{
			GameObject _endStripe;
			// Distance entre la punta del stripe y el punto... necesaria para indicar si el stripe esta sostenido o no...
			// lo idea seria: si la distancia < 1 entonces coloca
//			Vector3 asd = this.currentStripe.transform.GetChild (0).GetComponent<StripeController> ().HandlePosition;
//
//			Debug.Log (Vector3.Distance (asd, this.nextPoint.Position));
			this.collector.addCurrentPoint (this.currentPoint);
			this.currentStripe.transform.GetChild(0).GetComponent<StripeController> ().placeStripe (this.nextPoint.Position);
			this.nextPoint.IsSelectedPoint = false;
			this.currentPoint.turnOff ();
			_endStripe = (GameObject)GameObject.Instantiate(this.endStripePrefab, 
		                                                        this.nextPoint.Position, Quaternion.Euler(Vector3.zero));
			_endStripe.transform.parent = this.pointsContainer.transform;

			this.collector.addEndStripe(_endStripe);

			this.pointValidation ();

			this.previousPoint = this.currentPoint;
			this.currentPoint = this.nextPoint;
			this.nextPoint = null;
			this.createNewStripe (this.currentPoint.Position);

		}

		public void cancelCurrentStripe()
		{
			this.currentStripe = this.collector.cancelSelection ();

			if(this.previousPoint != null)
			{
				this.currentPoint = this.previousPoint;
				this.currentPoint.turnOn();
				this.previousPoint = this.collector.getPreviousPoint();

				if(this.collector.isErrorPoint(this.currentPoint))
				{
					this.errorCount --;
				}
			}
			else
			{
				this.isFirstPoint = true;
				this.currentPoint.turnOff();
				this.currentPoint = null;
				this.errorCount = -1;
			}

			if(this.currentStripe != null)
			{
				this.collector.removeLasEndStripe();
				this.currentStripe.GetComponentInChildren<StripeController>().resetStripe();
			}

			this.stripesPlaced --;
		}

		private void pointValidation()
		{
			if(!this.currentPoint.isAlreadyNeighbor(this.nextPoint))
		   	{
				if(this.errorCount == -1)
				{
					this.errorCount = 0;
				}

				this.collector.addErrorPoint(this.currentPoint);
				this.errorCount ++;
			}
			this.stripesPlaced ++;
		}

		private void createNewStripe(Vector3 position)
		{
			this.currentStripe = (GameObject)GameObject.Instantiate(this.stripePrefab, position, 
			                                                        Quaternion.Euler(Vector3.zero));
			this.currentStripe.transform.parent = this.pointsContainer.transform;

			this.collector.addCurrentStripe (this.currentStripe);
		}

		#region Properties
		public Point CurrentPoint
		{
			get { return this.currentPoint; }
		}

		public Model CurrentModel
		{
			set { this.currentModel = value; }
		}

		public int ErrorCount
		{
			get { return this.errorCount; }
		}
		#endregion
	}
}

