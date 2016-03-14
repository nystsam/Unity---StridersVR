using System.Collections.Generic;
using UnityEngine;

namespace StridersVR.Domain.DotToDot
{
	public class VertexPoint
	{
		private Vector3 vertexPointPosition;
		private List<GameObject> listGameFigureModel;

		public VertexPoint (Vector3 vertexPointPosition)
		{
			this.vertexPointPosition = vertexPointPosition;
			this.listGameFigureModel = new List<GameObject>();
		}


		#region Properties
		public Vector3 VertexPointPosition
		{
			get { return this.vertexPointPosition; }
			set { this.vertexPointPosition = value; }
		}
		public List<GameObject> ListGameFigureModel
		{
			get { return this.listGameFigureModel; }
			set { this.listGameFigureModel = value; }
		}
		#endregion
	}
}

