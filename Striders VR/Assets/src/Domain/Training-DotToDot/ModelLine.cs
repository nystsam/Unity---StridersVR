using System.Collections.Generic;
using UnityEngine;

namespace StridersVR.Domain.DotToDot
{
	[System.Serializable]
	public class ModelLine : FigureModel
	{
		public ModelLine (string figureName, GameObject prefab) : base(figureName, prefab)
		{
		}


		public override bool isLine()
		{
			return true;
		}
		
		public override void updateNeighbourVectorList(GameObject containerLocal, GameObject gameFigureGame)
		{
			Transform _vertex, _firstNeighbour;
			GameObject _cloneFigure = this.setStripesList (gameFigureGame);
			this.vertexPointList = new List<VertexPoint> ();
			
			_vertex = this.stripesList [0];
			_firstNeighbour = this.stripesList [1];
			this.assignParent (ref _vertex, ref _firstNeighbour, containerLocal);
			this.vertexWithOneChild (_vertex, _firstNeighbour);
			this.assignParent (ref _vertex, ref _firstNeighbour, _cloneFigure);
			
			_vertex = this.stripesList [1];
			_firstNeighbour = this.stripesList [0];
			this.assignParent (ref _vertex, ref _firstNeighbour, containerLocal);
			this.vertexWithOneChild (_vertex, _firstNeighbour);
			this.assignParent (ref _vertex, ref _firstNeighbour, _cloneFigure);
			
			_cloneFigure.transform.parent = null;
			GameObject.Destroy (_cloneFigure);
		}

		private void assignParent(ref Transform vertex, ref Transform firstNeighbour, GameObject container)
		{
			vertex.parent = container.transform;
			firstNeighbour.parent = container.transform;
		}
	}
}

