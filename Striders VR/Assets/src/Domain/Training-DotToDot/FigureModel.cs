using System.Collections.Generic;
using UnityEngine;

namespace StridersVR.Domain.DotToDot
{
	[System.Serializable]
	public abstract class FigureModel 
	{
		[SerializeField] protected string figureName;
		[SerializeField] protected GameObject prefab;
		protected List<Transform> stripesList;
		protected List<VertexPoint> vertexPointList;

		public FigureModel(string figureName, GameObject prefab)
		{
			this.figureName = figureName;
			this.prefab = prefab;
		}

		protected void vertexWithOneChild(Transform vertex, Transform firstNeighbour)
		{
			VertexPoint _newVertexPoint = new VertexPoint (vertex.localPosition);
			
			_newVertexPoint.NeighbourVectorList.Add (firstNeighbour.localPosition);
			
			this.vertexPointList.Add (_newVertexPoint);
		}

		protected void vertexWithTwoChild(Transform vertex, Transform firstNeighbour, Transform secondNeighbour)
		{
			VertexPoint _newVertexPoint = new VertexPoint (vertex.localPosition);

			_newVertexPoint.NeighbourVectorList.Add (firstNeighbour.localPosition);
			_newVertexPoint.NeighbourVectorList.Add (secondNeighbour.localPosition);

			this.vertexPointList.Add (_newVertexPoint);
		}

		public GameObject setStripesList(GameObject gameFigureModel)
		{
			Transform _child;
			this.stripesList = new List<Transform>();
			GameObject _cloneFigure = (GameObject)GameObject.Instantiate(this.prefab, 
			                                                             Vector3.zero,
			                                                             gameFigureModel.transform.localRotation);
			_cloneFigure.SetActive (false);
			_cloneFigure.transform.parent = gameFigureModel.transform.parent;
			_cloneFigure.transform.localPosition = gameFigureModel.transform.localPosition;
			
			for (int _index = 0; _index < _cloneFigure.transform.childCount; _index++) 
			{
				_child = _cloneFigure.transform.GetChild(_index);
				this.stripesList.Add(_child);
			}
			
			return _cloneFigure;
		}

		public abstract bool isLine();

		public abstract void updateNeighbourVectorList(GameObject containerLocal,  GameObject gameFigureGame);

		#region Properties
		public string FigureName
		{
			get { return this.figureName; }
			set { this.figureName = value;}
		}

		public GameObject Prefab
		{
			get { return this.prefab; }
			set { this.prefab = value; }
		}

		public List<Transform> StripesList
		{
			get { return this.stripesList; }
		}
		public List<VertexPoint> VertexPointList
		{
			get { return this.vertexPointList; }
			set { this.vertexPointList = value; }
		}
		#endregion
	}
}
