using System.Collections.Generic;
using UnityEngine;
using StridersVR.Domain.DotToDot;
using StridersVR.Modules.DotToDot.Logic.Contexts;
using StridersVR.Modules.DotToDot.Logic.Strategies;
using StridersVR.Modules.DotToDot.Logic.StrategyInterfaces;
using StridersVR.ScriptableObjects.DotToDot;

namespace StridersVR.Modules.DotToDot.Logic.Representatives
{
	public class RepresentativeModelFigure
	{
		private GameObject figureContainer;
		private ContextCreateModel contextCreateModel;
		private StrategyCreateModelComposite compositeStrategy;
		private ScriptableObjectFigureModel figureModelData;
		private List<VertexPoint> myList;

		public RepresentativeModelFigure(GameObject figureContainer)
		{
			this.contextCreateModel = new ContextCreateModel ();
			this.myList = new List<VertexPoint> ();
			this.figureContainer = figureContainer;
			this.assignObjtectData ();
		}
		

		public void createFigure()
		{
			FigureModel _figure = this.figureModelData.getHourglass210();

			//_figure.initializeVertexPoints ();
			this.contextCreateModel.StrategyCreateModel = new StrategyCreateModelBase (this.figureContainer, _figure);
			this.contextCreateModel.createModelFigure ();
			this.contextCreateModel.gameVertexPoint (ref this.myList);

			_figure = this.figureModelData.getTriangleEquilateral();
			this.contextCreateModel.StrategyCreateModel = new StrategyCreateModelTwoTrianglesEq (this.figureContainer, _figure);
			this.contextCreateModel.createModelFigure ();
			this.contextCreateModel.gameVertexPoint (ref this.myList);
			Debug.Log (this.myList.Count);
//			foreach (VertexPoint asd in this.myList) 
//			{
//				Debug.Log(asd.VertexPointPosition);
//				Debug.Log ("Hijos:");
//				foreach(Vector3 qq in asd.NeighbourVectorList)
//				{
//					Debug.Log (qq);
//				}
//				Debug.Log ("-----------------------");
//			}
//			this.contextCreateModel.createModelFigure (_figure);
//			this.contextCreateModel.createModelFigure (_figure);
//			this.contextCreateModel.createModelFigure (_figure);

		}

		private void initializeStrategies()
		{
			FigureModel _figure;


		}

		private void assignObjtectData()
		{
			this.figureModelData = this.figureContainer.GetComponent<PlatformModelController> ().figureModelData;
		}
	}
}