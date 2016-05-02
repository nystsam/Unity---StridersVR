using System.Collections.Generic;
using UnityEngine;

namespace StridersVR.Domain.DotToDot
{
	public class PointsContainer
	{
		private List<PointDot> pointDotList;

		private int totalStripesAssigned = 0;

		public PointsContainer ()
		{
			this.pointDotList = new List<PointDot> ();
		}


		public int numberOfPoints()
		{
			return this.pointDotList.Count;
		}

		public void newStripe()
		{
			this.totalStripesAssigned ++;
		}

		public void addPoint(PointDot point)
		{
			this.pointDotList.Add (point);
		}

		public PointDot getPointAtIndex(int index)
		{
			return this.pointDotList [index];
		}

		public PointDot findPoint(int id)
		{
			return this.pointDotList.Find (x => x.PointId == id);
		}

		public PointDot findPoint(Vector3 pointPosition)
		{
			return this.pointDotList.Find (x => x.PointPosition == pointPosition);
		}

		#region Properties
		public int TotalStripesAssigned
		{
			get { return this.totalStripesAssigned; }
		}
		#endregion
	}
}

