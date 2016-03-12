using System.Collections.Generic;
using UnityEngine;
using StridersVR.Modules.DotToDot.Logic.StrategyInterfaces;

namespace StridersVR.Domain.DotToDot
{
	[System.Serializable]
	public abstract class FigureModel 
	{
		private Vector3 minVectorToSpawn;
		private Vector3 maxVectorToSpawn;

		[SerializeField] protected string figureName;
		[SerializeField] protected GameObject prefab;

		public FigureModel()
		{
		
		}


		public List<Transform> getStripes()
		{
			List<Transform> _list = new List<Transform>();
			
			for (int _index = 0; _index < this.prefab.transform.childCount; _index++) 
			{
				_list.Add(this.prefab.transform.GetChild(_index));
			}
			
			return _list;
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

		public Vector3 MinVectorToSpawn
		{
			get { return this.minVectorToSpawn; }
			set { this.minVectorToSpawn = value; }
		}

		public Vector3 MaxVectorToSpawn
		{
			get { return this.maxVectorToSpawn; }
			set { this.maxVectorToSpawn = value; }
		}
		#endregion
	}
}
