using System.Collections.Generic;
using UnityEngine;

namespace StridersVR.Domain.DotToDot
{
	public class PointDot
	{
		private int pointId;

		private bool selected;

		private Vector3 pointPosition;

		private List<Vector3> neighbourPoints;


		public PointDot (int pointId, Vector3 pointPosition)
		{
			this.pointId = pointId;
			this.pointPosition = pointPosition;

			this.selected = false;
			this.neighbourPoints = new List<Vector3> ();
		}


		public void addNeighbour(Vector3 neighbour)
		{
			this.neighbourPoints.Add (neighbour);
		}

		public bool validateNeighbour(Vector3 currentVector)
		{
			foreach (Vector3 neighbour in this.neighbourPoints) 
			{
				if(neighbour == currentVector)
				{
					return true;
				}
			}

			return false;
		}

		public int numberOfNeighbour()
		{
			return this.neighbourPoints.Count;
		}

		#region Properties
		public int PointId
		{
			get { return this.pointId; }
		}

		public Vector3 PointPosition
		{
			get { return this.pointPosition; }
		}

		public bool Selected
		{
			get { return this.selected; }
			set { this.selected = value; }
		}
		#endregion
	}
}

