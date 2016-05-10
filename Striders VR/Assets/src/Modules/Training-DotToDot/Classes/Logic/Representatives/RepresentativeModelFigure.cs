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
		
		private PointsContainer pointsFromController;

		private bool refreshPosition;

		private StrategyCreateModelComposite compositeStrategyAbstract1;

		public RepresentativeModelFigure(GameObject figureContainer, GameObject playerFigureContainer)
		{
			this.contextCreateModel = new ContextCreateModel ();
			this.figureContainer = figureContainer;
			this.playerFigureContainer = playerFigureContainer;
			this.refreshPosition = false;

			this.assignObjtectData ();

			this.compositeStrategyAbstract1 = new StrategyCreateModelComposite ();
			this.instantiateComposite ();
		}

		public void createModel()
		{
			if (this.refreshPosition) 
			{
				this.removeParentFromPlayerContainer();
				this.increaseSizeFigurePlatform();

				for (int i = 0; i < this.figureContainer.transform.childCount; i++) 
				{
					Transform _child = this.figureContainer.transform.GetChild (i);
					GameObject.Destroy (_child.gameObject);
				}
			}

			this.contextCreateModel.StrategyCreateModel = new StrategyCreateModelAbstractEasy (this.figureContainer, figureModelData.stripeContainer ());
			this.contextCreateModel.createModel (this.pointsFromController);
			this.reduceSizeFigurePlatform ();
			this.addParentToPlayerContainer ();
			this.refreshPosition = true;
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
			this.figureContainer.transform.parent.localScale = new Vector3 (0.02f, 0.02f, 0.02f);
		}

		private void increaseSizeFigurePlatform()
		{
			this.figureContainer.transform.parent.localScale = new Vector3 (1, 1, 1);
		}

		private void addParentToPlayerContainer()
		{
			this.figureContainer.transform.parent.parent = this.playerFigureContainer.transform;
			this.figureContainer.transform.parent.localPosition = Vector3.zero;
			this.playerFigureContainer.transform.localRotation = Quaternion.Euler (new Vector3 (0, -45, 0)); 
		}

		private void removeParentFromPlayerContainer()
		{
			this.figureContainer.transform.parent.parent = null;
			this.figureContainer.transform.parent.localPosition = new Vector3 (100, 0, 0);
			this.playerFigureContainer.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, 0)); 
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

		public int NumberOfStripesAssigned
		{
			get { return this.pointsFromController.TotalStripesAssigned; }
		}
		#endregion
	}
}