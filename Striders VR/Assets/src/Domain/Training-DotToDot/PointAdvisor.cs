using System;
using UnityEngine;

namespace StridersVR.Domain.DotToDot
{
	public class PointAdvisor
	{
		private GameObject stripePrefab;
		private GameObject endStripePrefab;
		private GameObject currentStripe;

		private Point currentPoint = null;
		private Point nextPoint = null;
		private Point previousPoint = null;

		private DotToDotCollector collector;

		private bool isFirstPoint;		

		public PointAdvisor ()
		{
			this.stripePrefab = Resources.Load ("Prefabs/Training-DotToDot/Stripe", typeof(GameObject)) as GameObject;
			this.endStripePrefab = Resources.Load ("Prefabs/Training-DotToDot/EndPointStripe", typeof(GameObject)) as GameObject;
			this.collector = new DotToDotCollector ();
			this.isFirstPoint = true;
		}


		public bool setTouchingPoint(Point point)
		{
			if(this.isFirstPoint)
			{
				this.isFirstPoint = false;
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

			this.collector.addEndStripe(_endStripe);
			// Chequear puntuacion y separar en otra funcion
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
			}
			else
			{
				this.isFirstPoint = true;
				this.currentPoint.turnOff();
				this.currentPoint = null;
			}

			if(this.currentStripe != null)
			{
				this.collector.removeLasEndStripe();
				this.currentStripe.GetComponentInChildren<StripeController>().resetStripe();
			}
			
		}

		private void createNewStripe(Vector3 position)
		{
			this.currentStripe = (GameObject)GameObject.Instantiate(this.stripePrefab, position, 
			                                                        Quaternion.Euler(Vector3.zero));
			this.collector.addCurrentStripe (this.currentStripe);
		}

		#region Properties
		public Point CurrentPoint
		{
			get { return this.currentPoint; }
		}
		#endregion
	}
}

