using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace StridersVR.Domain.DotToDot
{
	public class DotToDotCollector
	{
		private List<GameObject> stripesList;
		private List<GameObject> endStripeList;

		private List<Point> pointList;


		public DotToDotCollector ()
		{
			this.stripesList = new List<GameObject> ();
			this.endStripeList = new List<GameObject> ();
			this.pointList = new List<Point> ();
		}


		public void addCurrentStripe(GameObject stripe)
		{
			this.stripesList.Add(stripe);
		}

		public void addCurrentPoint(Point point)
		{
			this.pointList.Add (point);
		}

		public void addEndStripe(GameObject endStripe)
		{
			this.endStripeList.Add (endStripe);
		}

		public GameObject cancelSelection()
		{
			GameObject _lastStripe = this.stripesList.Last ();

			this.stripesList.Remove (_lastStripe);
			GameObject.Destroy (_lastStripe);

			if(this.pointList.Count > 0)
			{
				this.pointList.RemoveAt(this.pointList.Count - 1);
			}

			if(this.stripesList.Count > 0)
			{
				return this.stripesList.Last();
			}
			else
			{
				return null;
			}
		}

		public void removeLasEndStripe()
		{
			if(this.endStripeList.Count > 0)
			{
				GameObject _endStripe;

				_endStripe = this.endStripeList.Last();
				GameObject.Destroy(_endStripe);
				this.endStripeList.Remove(_endStripe);
			}
		}

		public Point getPreviousPoint()
		{
			if(this.pointList.Count > 0)
			{
				return this.pointList.Last();
			}
			else
			{
				return null;
			}
		}
	}
}

