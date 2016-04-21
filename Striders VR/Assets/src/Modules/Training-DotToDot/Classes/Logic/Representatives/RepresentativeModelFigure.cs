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
		private GameObject playerFigureContainer;

		private ContextCreateModel contextCreateModel;

		private ScriptableObjectFigureModel figureModelData;

		private List<VertexPoint> inGameVertexPointList;

		private int numberOfPoints;

		private StrategyCreateModelComposite compositeStrategyAbstract1;

		public RepresentativeModelFigure(GameObject figureContainer, GameObject playerFigureContainer)
		{
			this.contextCreateModel = new ContextCreateModel ();
			this.inGameVertexPointList = new List<VertexPoint> ();
			this.numberOfPoints = 0;
			this.figureContainer = figureContainer;
			this.playerFigureContainer = playerFigureContainer;
			this.assignObjtectData ();

			this.compositeStrategyAbstract1 = new StrategyCreateModelComposite ();
			this.instantiateComposite ();
		}


		public void createFigure()
		{
			foreach (IStrategyCreateModel _strategy in this.compositeStrategyAbstract1.StategyCreateModelList) 
			{
				this.contextCreateModel.StrategyCreateModel = _strategy;
				this.contextCreateModel.selectGameFigure (this.figureModelData);
				this.contextCreateModel.createModelFigure ();
				this.contextCreateModel.gameVertexPoint (ref this.inGameVertexPointList);
				this.numberOfPoints += this.contextCreateModel.numberOfPoints ();
			}
			this.reduceSizeFigurePlatform ();
			this.addParentToPlayerContainer ();
			//this.contextCreateModel.StrategyCreateModel = new StrategyCreateModelBase (this.figureContainer);


//			foreach (VertexPoint asd in inGameVertexPointList) 
//			{
//				Debug.Log(asd.VertexPointPosition);
//				Debug.Log ("Hijos: " + asd.NeighbourVectorList.Count);
//				foreach(Vector3 qq in asd.NeighbourVectorList)
//				{
//					Debug.Log (qq);
//				}
//				Debug.Log ("-----------------------");
//			}

//			this.contextCreateModel.StrategyCreateModel = new StrategyCreateModelTwoTrianglesEq (this.figureContainer);
//			this.contextCreateModel.selectGameFigure (this.figureModelData);
//			this.contextCreateModel.createModelFigure ();
//			this.contextCreateModel.gameVertexPoint (ref this.inGameVertexPointList);
//			this.numberOfPoints += this.contextCreateModel.numberOfPoints ();
		}

		private void instantiateComposite()
		{
			IStrategyCreateModel _strategy;

			_strategy = new StrategyCreateModelBase (this.figureContainer);
			this.compositeStrategyAbstract1.addStrategy (_strategy);

			_strategy= new StrategyCreateModelTwoTrianglesEq (this.figureContainer);
			this.compositeStrategyAbstract1.addStrategy (_strategy);

		}

		private void reduceSizeFigurePlatform()
		{
			this.figureContainer.transform.parent.localScale = new Vector3 (0.05f, 0.05f, 0.05f);
		}

		private void increaseSizeFigurePlatform()
		{
			this.figureContainer.transform.parent.localScale = new Vector3 (1, 1, 1);
		}

		private void addParentToPlayerContainer()
		{
			this.figureContainer.transform.parent = this.playerFigureContainer.transform;
			this.figureContainer.transform.localPosition = Vector3.zero;
		}

		private void removeParentFromPlayerContainer()
		{
			this.figureContainer.transform.parent = null;
			this.figureContainer.transform.localPosition = new Vector3 (100, 0, 0);
		}

		private void assignObjtectData()
		{
			this.figureModelData = this.figureContainer.GetComponent<PlatformModelController> ().figureModelData;
		}

		#region Properties
		public List<VertexPoint> VertexPointList
		{
			get { return this.inGameVertexPointList; }
		}

		public int NumberOfPoints
		{
			get { return this.numberOfPoints; }
		}
		#endregion
	}
}