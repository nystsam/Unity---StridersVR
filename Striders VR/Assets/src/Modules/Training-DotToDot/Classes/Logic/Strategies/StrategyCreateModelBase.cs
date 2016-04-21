using UnityEngine;
using System.Collections.Generic;
using StridersVR.Modules.DotToDot.Logic.StrategyInterfaces;
using StridersVR.ScriptableObjects.DotToDot;
using StridersVR.Domain.DotToDot;

namespace StridersVR.Modules.DotToDot.Logic.Strategies
{
	public class StrategyCreateModelBase : IStrategyCreateModel
	{
		private GameObject figureContainer;

		private FigureModel figureModel;

		private float figureRotationX;
		private float figureRotationY;

		private int numberOfPointsUsed;

		private List<VertexPoint> vertexPointList;

		public StrategyCreateModelBase (GameObject figureContainer)
		{
			this.figureContainer = figureContainer;
			this.figureRotationX = Random.Range (45, -45);
			this.figureRotationY = Random.Range (0, 45);
			this.numberOfPointsUsed = 0;
		}


		#region IStrategyCreateModel
		public void selectGameFigure(ScriptableObject figureData)
		{
			ScriptableObjectFigureModel _scriptableObjectFigureModel = (ScriptableObjectFigureModel)figureData;
			List<FigureModel> _listBase = _scriptableObjectFigureModel.obtainModels ();

			while (true) 
			{
				this.figureModel = _listBase[Random.Range(0,_listBase.Count)];
				if(!this.figureModel.isLine())
				{
					break;
				}
			}

			this.vertexPointList = new List<VertexPoint> ();
		}

		public void createModelFigure()
		{
			GameObject _gameNewFigure;

			_gameNewFigure = (GameObject)GameObject.Instantiate(this.figureModel.Prefab,new Vector3(0,0,0), Quaternion.Euler(new Vector3(0,0,0)));
			_gameNewFigure.transform.parent = this.figureContainer.transform;
			_gameNewFigure.name = this.figureModel.FigureName;
			_gameNewFigure.transform.localPosition = new Vector3(0,-20,0);
			_gameNewFigure.transform.localRotation = Quaternion.Euler(new Vector3(this.figureRotationX, this.figureRotationY, 0));

			this.figureModel.updateNeighbourVectorList (this.figureContainer, _gameNewFigure);
			this.numberOfPointsUsed += this.figureModel.StripesList.Count;
			this.vertexPointList = this.figureModel.VertexPointList;

			_gameNewFigure.GetComponent<FigureModelController> ().FigureModel = this.figureModel;
			_gameNewFigure.GetComponent<FigureModelController> ().setFigureVertexCount ();
			_gameNewFigure.GetComponent<FigureModelController> ().IsBase = true;
		}

		public void gameVertexPoint(ref List<VertexPoint> currentVertexPointList)
		{
			foreach(VertexPoint vertexPoint in this.vertexPointList)
			{
				currentVertexPointList.Add(vertexPoint);
			}
		}

		public int numberOfPoints()
		{
			return this.numberOfPointsUsed;
		}
		#endregion
	}
}

