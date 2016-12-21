using System.Collections.Generic;
using UnityEngine;
using StridersVR.Domain.DotToDot;
using StridersVR.Modules.DotToDot.Logic.Contexts;
using StridersVR.Modules.DotToDot.Logic.Strategies;

namespace StridersVR.Modules.DotToDot.Logic.Representatives
{
	public class RepresentativeModelFigure
	{
		private ContextCreateModel contextCreateModel;

		private GameObject modelContainer;

		private Model currentModel;

		private StrategyCreateModelComposite strategyComposite;
		
		public RepresentativeModelFigure(GameObject modelContainer, string difficulty)
		{
			this.contextCreateModel = new ContextCreateModel ();
			this.modelContainer = modelContainer;
			this.createStrategies (difficulty);

		}

		public void createModel()
		{
			this.strategyComposite.StrategyIndex = Random.Range (0, this.strategyComposite.StrategyCount);

			this.currentModel = this.contextCreateModel.createModel ();
		}

		public void clearContainer(GameObject container)
		{
			foreach(Transform child in container.transform)
			{
				GameObject.Destroy(child.gameObject);
			}
		}

		public void replicateModel(GameObject container)
		{
			GameObject _clone;
			Material _newMaterial = Resources.Load("Materials/Training-DotToDot/MatEndiPointVisual", typeof(Material)) as Material;


			foreach(Transform child in this.modelContainer.transform)
			{
				_clone = (GameObject)GameObject.Instantiate(child.gameObject, Vector3.zero, Quaternion.Euler(Vector3.zero));
				_clone.transform.parent = container.transform;
				_clone.transform.localPosition = child.localPosition;

				if(_clone.transform.childCount > 0)
				{
					if(_clone.transform.GetChild(0).GetComponent<StripeController>() != null)
					{
						_clone.transform.localRotation = child.localRotation;
						_clone.transform.localScale = child.localScale;
						_clone.transform.GetChild(0).GetComponent<StripeController>().setUndraggable();
					}
				}
				else
				{
					_clone.transform.localScale = new Vector3(0.3f,0.3f,0.3f);
					_clone.GetComponent<MeshRenderer>().material = _newMaterial;
				}
			}
		}

		private void createStrategies(string difficulty)
		{
			this.strategyComposite = new StrategyCreateModelComposite ();

			if(difficulty.Equals("Easy"))
			{
				this.strategyComposite.addStrategy(new StrategyCreateModelEasy(this.modelContainer));
			}

			this.contextCreateModel.StrategyCreateModel = this.strategyComposite;
		}

		#region Properties
		public Model CurrentModel
		{
			get { return this.currentModel; }
		}
		#endregion

//		private GameObject figureContainer;
//		private GameObject playerFigureContainer;
//
//		private ContextCreateModel contextCreateModel;
//
//		private ScriptableObjectFigureModel figureModelData;
//		
//		private PointsContainer pointsFromController;
//
//		private bool refreshPosition;
//
//		private StrategyCreateModelComposite compositeStrategyAbstract1;
//
//		public RepresentativeModelFigure(GameObject figureContainer, GameObject playerFigureContainer)
//		{
//			this.contextCreateModel = new ContextCreateModel ();
//			this.figureContainer = figureContainer;
//			this.playerFigureContainer = playerFigureContainer;
//			this.refreshPosition = false;
//
//			this.assignObjtectData ();
//
//			this.compositeStrategyAbstract1 = new StrategyCreateModelComposite ();
//			this.instantiateComposite ();
//		}
//
//		public void createModel()
//		{
//			if (this.refreshPosition) 
//			{
//				this.removeParentFromPlayerContainer();
//				this.increaseSizeFigurePlatform();
//
//				for (int i = 0; i < this.figureContainer.transform.childCount; i++) 
//				{
//					Transform _child = this.figureContainer.transform.GetChild (i);
//					GameObject.Destroy (_child.gameObject);
//				}
//			}
//
//			this.contextCreateModel.StrategyCreateModel = new StrategyCreateModelAbstractEasy (this.figureContainer, figureModelData.stripeContainer ());
//			this.contextCreateModel.createModel (this.pointsFromController);
//			this.reduceSizeFigurePlatform ();
//			this.addParentToPlayerContainer ();
//			this.refreshPosition = true;
//		}
//
//		public void createFigure()
//		{
////			foreach (IStrategyCreateModel _strategy in this.compositeStrategyAbstract1.StategyCreateModelList) 
////			{
////				this.contextCreateModel.StrategyCreateModel = _strategy;
////				this.contextCreateModel.selectGameFigure (this.figureModelData);
////				this.contextCreateModel.createModelFigure ();
////				this.contextCreateModel.gameVertexPoint (ref this.inGameVertexPointList);
////				this.numberOfPoints += this.contextCreateModel.numberOfPoints ();
////				break;
////			}
////			this.reduceSizeFigurePlatform ();
////			this.addParentToPlayerContainer ();
//			//this.contextCreateModel.StrategyCreateModel = new StrategyCreateModelBase (this.figureContainer);
//
//
////			foreach (VertexPoint asd in inGameVertexPointList) 
////			{
////				Debug.Log(asd.VertexPointPosition);
////				Debug.Log ("Hijos: " + asd.NeighbourVectorList.Count);
////				foreach(Vector3 qq in asd.NeighbourVectorList)
////				{
////					Debug.Log (qq);
////				}
////				Debug.Log ("-----------------------");
////			}
//
////			this.contextCreateModel.StrategyCreateModel = new StrategyCreateModelTwoTrianglesEq (this.figureContainer);
////			this.contextCreateModel.selectGameFigure (this.figureModelData);
////			this.contextCreateModel.createModelFigure ();
////			this.contextCreateModel.gameVertexPoint (ref this.inGameVertexPointList);
////			this.numberOfPoints += this.contextCreateModel.numberOfPoints ();
//		}
//
//		private void instantiateComposite()
//		{
////			IStrategyCreateModel _strategy;
////
////			_strategy = new StrategyCreateModelBase (this.figureContainer);
////			this.compositeStrategyAbstract1.addStrategy (_strategy);
////
////			_strategy= new StrategyCreateModelTwoTrianglesEq (this.figureContainer);
////			this.compositeStrategyAbstract1.addStrategy (_strategy);
//
//		}
//
//		private void reduceSizeFigurePlatform()
//		{
//			this.figureContainer.transform.parent.localScale = new Vector3 (0.02f, 0.02f, 0.02f);
//		}
//
//		private void increaseSizeFigurePlatform()
//		{
//			this.figureContainer.transform.parent.localScale = new Vector3 (1, 1, 1);
//		}
//
//		private void addParentToPlayerContainer()
//		{
//			this.figureContainer.transform.parent.parent = this.playerFigureContainer.transform;
//			this.figureContainer.transform.parent.localPosition = Vector3.zero;
//			this.playerFigureContainer.transform.localRotation = Quaternion.Euler (new Vector3 (0, -45, 0)); 
//		}
//
//		private void removeParentFromPlayerContainer()
//		{
//			this.figureContainer.transform.parent.parent = null;
//			this.figureContainer.transform.parent.localPosition = new Vector3 (100, 0, 0);
//			this.playerFigureContainer.transform.localRotation = Quaternion.Euler (new Vector3 (0, 0, 0)); 
//		}
//
//		private void assignObjtectData()
//		{
//			this.figureModelData = this.figureContainer.GetComponent<PlatformModelController> ().figureModelData;
//		}
//
//		#region Properties
//		public PointsContainer PointsFromController
//		{
//			set { this.pointsFromController = value; }
//		}
//
//		public int NumberOfStripesAssigned
//		{
//			get { return this.pointsFromController.TotalStripesAssigned; }
//		}
//		#endregion
	}
}