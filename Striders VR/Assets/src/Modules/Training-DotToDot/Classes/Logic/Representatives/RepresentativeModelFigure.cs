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
		private GameObject dotContainer;
		private GameObject endPointsContainer;

		private ContextCreateModel contextCreateModel;

		private ScriptableObjectFigureModel figureModelData;
		
		private PointsContainer pointsFromController;

		private StrategyCreateModelComposite compositeStrategyAbstract1;

		public RepresentativeModelFigure(GameObject figureContainer, GameObject playerFigureContainer)
		{
			this.contextCreateModel = new ContextCreateModel ();
			this.figureContainer = figureContainer;
			this.playerFigureContainer = playerFigureContainer;
			this.assignObjtectData ();

			this.compositeStrategyAbstract1 = new StrategyCreateModelComposite ();
			this.instantiateComposite ();
		}


		public void createModel()
		{
			this.contextCreateModel.StrategyCreateModel = new StrategyCreateModelAbstractEasy (this.figureContainer, figureModelData.stripeContainer ());
			this.contextCreateModel.createModel (this.pointsFromController);
			this.reduceSizeFigurePlatform ();
			this.addParentToPlayerContainer ();
		}

		public void createFigure()
		{
//			foreach (IStrategyCreateModel _strategy in this.compositeStrategyAbstract1.StategyCreateModelList) 
//			{
//				this.contextCreateModel.StrategyCreateModel = _strategy;
//				this.contextCreateModel.selectGameFigure (this.figureModelData);
//				this.contextCreateModel.createModelFigure ();
//				this.contextCreateModel.gameVertexPoint (ref this.inGameVertexPointList);
//				this.numberOfPoints += this.contextCreateModel.numberOfPoints ();
//				break;
//			}
//			this.reduceSizeFigurePlatform ();
//			this.addParentToPlayerContainer ();
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

		public void removeCurrentFigureModel()
		{
			for (int i = 0; i < this.dotContainer.transform.childCount; i++) {
				Transform _child = this.dotContainer.transform.GetChild (i);
				GameObject.Destroy (_child.gameObject);
			}
			
			for (int i = 0; i < this.endPointsContainer.transform.childCount; i++) {
				Transform _child = this.endPointsContainer.transform.GetChild (i);
				GameObject.Destroy (_child.gameObject);
			}
		}

		private void instantiateComposite()
		{
//			IStrategyCreateModel _strategy;
//
//			_strategy = new StrategyCreateModelBase (this.figureContainer);
//			this.compositeStrategyAbstract1.addStrategy (_strategy);
//
//			_strategy= new StrategyCreateModelTwoTrianglesEq (this.figureContainer);
//			this.compositeStrategyAbstract1.addStrategy (_strategy);

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
		public PointsContainer PointsFromController
		{
			set { this.pointsFromController = value; }
		}

		public GameObject DotContainer
		{
			set { this.dotContainer = value; }
		}

		public GameObject EndPointContainer
		{
			set { this.endPointsContainer = value; }
		}

		public int NumberOfStripesAssigned
		{
			get { return this.pointsFromController.TotalStripesAssigned; }
		}
		#endregion
	}
}