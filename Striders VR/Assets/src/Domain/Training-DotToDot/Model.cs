using System.Collections.Generic;
using UnityEngine;

namespace StridersVR.Domain.DotToDot
{
	public class Model
	{
		private GameObject stripePrefab;
		private GameObject endStripePrefab;

		private Dictionary<int, Point> pointDictionary;

		private int stripesCount;

		public Model ()
		{
			this.pointDictionary = new Dictionary<int, Point> ();
			this.stripePrefab = Resources.Load ("Prefabs/Training-DotToDot/Stripe", typeof(GameObject)) as GameObject;
			this.endStripePrefab = Resources.Load ("Prefabs/Training-DotToDot/EndPointStripe", typeof(GameObject)) as GameObject;
			this.stripesCount = 0;
		}


		public void addPoint(Point point)
		{
			this.pointDictionary.Add (point.PointId, point);
		}

		public void addPointNeighbor(Point firstPoint, Point secondPoint)
		{
			firstPoint.addNeighbor (secondPoint);
			secondPoint.addNeighbor (firstPoint);
		}

		#region In-Game features
		/// <summary>
		/// Create a Stripe between one point to another
		/// </summary>
		/// <param name="container">The container that stores the objects.</param>
		/// <param name="startPoint">The Start point.</param>
		/// <param name="endPoint">The End point.</param>
		public void createStripe(GameObject container, Point startPoint, Point endPoint)
		{
			GameObject _newStripe, _newEndStripe;
			Vector3 _stripeDirection, _stripeScale, _modelPosition;

			_stripeDirection = (endPoint.Position - startPoint.Position).normalized;
		
			_stripeScale = Vector3.one;

			_newStripe = (GameObject)GameObject.Instantiate (this.stripePrefab, Vector3.zero, Quaternion.Euler (Vector3.zero));
			_newStripe.transform.parent = container.transform;
			_newStripe.transform.GetChild(0).GetComponent<StripeController>().setUndraggable();

			_modelPosition = startPoint.Position;
			_modelPosition.y = _modelPosition.y - 20f;

			_newStripe.transform.localPosition = _modelPosition;
			_newStripe.transform.localRotation = Quaternion.LookRotation (_stripeDirection);

			_newEndStripe = (GameObject)GameObject.Instantiate (this.endStripePrefab, Vector3.zero, Quaternion.Euler (Vector3.zero));
			_newEndStripe.transform.parent = container.transform;

			_modelPosition = endPoint.Position;
			_modelPosition.y = _modelPosition.y - 20f;

			_newEndStripe.transform.localPosition = _modelPosition;

			_stripeScale.z = Vector3.Distance (_newStripe.transform.position, _newEndStripe.transform.position)*25;
			_newStripe.transform.localScale = _stripeScale;

			this.stripesCount ++;
		}
		#endregion

		#region Properties
		public Dictionary<int, Point> Points
		{
			get { return this.pointDictionary; }
		}

		public int StripesCount
		{
			get { return this.stripesCount; }
		}
		#endregion
	}
}

