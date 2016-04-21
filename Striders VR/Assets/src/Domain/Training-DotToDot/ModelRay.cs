using System.Collections.Generic;
using UnityEngine;

namespace StridersVR.Domain.DotToDot
{
	[System.Serializable]
	public class ModelRay : FigureModel
	{
		public ModelRay (string figureName, GameObject prefab) : base(figureName, prefab)
		{
		}


		public override bool isLine()
		{
			return false;
		}

		public override void updateNeighbourVectorList(GameObject containerLocal, GameObject gameFigureGame)
		{
			Transform _vertex, _firstNeighbour, _secondNeighbour;
			GameObject _cloneFigure = this.setStripesList (gameFigureGame);
			this.vertexPointList = new List<VertexPoint> ();
			
			_vertex = this.stripesList [0];
			_firstNeighbour = this.stripesList [1];
			_secondNeighbour = this.stripesList [3];
			this.assignParent (ref _vertex, ref _firstNeighbour, ref _secondNeighbour, containerLocal);
			this.vertexWithTwoChild (_vertex, _firstNeighbour, _secondNeighbour);
			this.assignParent (ref _vertex, ref _firstNeighbour, ref _secondNeighbour, _cloneFigure);
			
			_vertex = this.stripesList [1];
			_firstNeighbour = this.stripesList [0];
			_secondNeighbour = this.stripesList [2];
			this.assignParent (ref _vertex, ref _firstNeighbour, ref _secondNeighbour, containerLocal);
			this.vertexWithTwoChild (_vertex, _firstNeighbour, _secondNeighbour);
			this.assignParent (ref _vertex, ref _firstNeighbour, ref _secondNeighbour, _cloneFigure);
			
			_vertex = this.stripesList [2];
			_firstNeighbour = this.stripesList [1];
			_secondNeighbour = this.stripesList [3];
			this.assignParent (ref _vertex, ref _firstNeighbour, ref _secondNeighbour, containerLocal);
			this.vertexWithTwoChild (_vertex, _firstNeighbour, _secondNeighbour);
			this.assignParent (ref _vertex, ref _firstNeighbour, ref _secondNeighbour, _cloneFigure);
			
			_vertex = this.stripesList [3];
			_firstNeighbour = this.stripesList [2];
			_secondNeighbour = this.stripesList [0];
			this.assignParent (ref _vertex, ref _firstNeighbour, ref _secondNeighbour, containerLocal);
			this.vertexWithTwoChild (_vertex, _firstNeighbour, _secondNeighbour);
			this.assignParent (ref _vertex, ref _firstNeighbour, ref _secondNeighbour, _cloneFigure);
			
			_cloneFigure.transform.parent = null;
			GameObject.Destroy (_cloneFigure);
		}
		
		private void assignParent(ref Transform vertex, ref Transform firstNeighbour, ref Transform secondNeighbour, GameObject container)
		{
			vertex.parent = container.transform;
			firstNeighbour.parent = container.transform;
			secondNeighbour.parent = container.transform;
		}


	}
}

