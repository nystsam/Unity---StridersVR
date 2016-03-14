using System.Collections.Generic;
using UnityEngine;
using StridersVR.Modules.DotToDot.Logic.StrategyInterfaces;

namespace StridersVR.Domain.DotToDot
{
	[System.Serializable]
	public abstract class FigureModel 
	{
		[SerializeField] protected string figureName;
		[SerializeField] protected GameObject prefab;
		protected List<VertexPoint> listVertexPoint;

		public FigureModel(string figureName, GameObject prefab)
		{
			this.figureName = figureName;
			this.prefab = prefab;
		}


		public void initializeVertexPoints()
		{
			this.listVertexPoint = new List<VertexPoint> ();
			
			for (int _index = 0; _index < this.prefab.transform.childCount; _index++) 
			{
				Transform _child = this.prefab.transform.GetChild(_index);
				this.listVertexPoint.Add(new VertexPoint(_child.localPosition));
			}
		}

		public abstract bool isResizableFigure();

		public abstract IStrategyCreateModel generator(GameObject container);

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

		public List<VertexPoint> ListVertexPoint
		{
			get { return this.listVertexPoint; }
		}
		#endregion
	}
}
