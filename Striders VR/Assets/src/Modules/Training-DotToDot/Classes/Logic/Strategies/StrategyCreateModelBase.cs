using UnityEngine;
using System.Collections.Generic;
using StridersVR.Modules.DotToDot.Logic.StrategyInterfaces;
using StridersVR.Domain.DotToDot;

namespace StridersVR.Modules.DotToDot.Logic.Strategies
{
	public class StrategyCreateModelBase : IStrategyCreateModel
	{
		private GameObject figureContainer;
		private FigureModel figureModel;
		private List<VertexPoint> vertexPointList;

		public StrategyCreateModelBase (GameObject figureContainer, FigureModel figureModel)
		{
			this.figureContainer = figureContainer;
			this.figureModel = figureModel;
			this.vertexPointList = new List<VertexPoint> ();
		}


		#region IStrategyCreateModel
		public void createModelFigure()
		{
			GameObject _gameNewFigure;
			float _rotationX = Random.Range (0, -45);
			float _rotationY = Random.Range (0, 360);

			_gameNewFigure = (GameObject)GameObject.Instantiate(this.figureModel.Prefab,new Vector3(0,0,0), Quaternion.Euler(new Vector3(0,0,0)));
			_gameNewFigure.transform.parent = this.figureContainer.transform;
			_gameNewFigure.name = this.figureModel.FigureName;
			_gameNewFigure.transform.localPosition = new Vector3(0,-20,0);
			_gameNewFigure.transform.localRotation = Quaternion.Euler(new Vector3(_rotationX, _rotationY, 0));

			this.figureModel.updateNeighbourVectorList (this.figureContainer, _gameNewFigure);
			this.vertexPointList = this.figureModel.VertexPointList;
			_gameNewFigure.GetComponent<FigureModelController> ().FigureModel = this.figureModel;
		}

		public void gameVertexPoint(ref List<VertexPoint> currentVertexPointList)
		{
			foreach(VertexPoint vertexPoint in this.vertexPointList)
			{
				currentVertexPointList.Add(vertexPoint);
			}
		}
		#endregion
	}
}

