using System.Collections.Generic;
using UnityEngine;

namespace StridersVR.Domain.DotToDot
{
	public class VertexPoint
	{
		private Vector3 vertexPointPosition;
		private List<Vector3> neighbourVectorList;

		public VertexPoint (Vector3 vertexPointPosition)
		{
			this.vertexPointPosition = vertexPointPosition;
			this.neighbourVectorList = new List<Vector3>();
		}


		#region Properties
		public Vector3 VertexPointPosition
		{
			get { return this.vertexPointPosition; }
			set { this.vertexPointPosition = value; }
		}
		public List<Vector3> NeighbourVectorList
		{
			get { return this.neighbourVectorList; }
			set { this.neighbourVectorList = value; }
		}
		#endregion
	}
}
