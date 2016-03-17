using UnityEngine;
using System.Collections.Generic;
using StridersVR.Modules.DotToDot.Logic.StrategyInterfaces;
using StridersVR.ScriptableObjects.DotToDot;
using StridersVR.Domain.DotToDot;

namespace StridersVR.Modules.DotToDot.Logic.Strategies
{
	public class StrategyCreateModelTwoTrianglesEq :  IStrategyCreateModel
	{
		private GameObject figureContainer;
		private GameObject gameFigureBase;
		private FigureModel figureModel;
		private int stripeIndex;
		private float figureRotationX;
		private float figureRotationY;
		private List<VertexPoint> vertexPointList;


		public StrategyCreateModelTwoTrianglesEq(GameObject figureContainer)
		{
			this.figureContainer = figureContainer;
		}


		#region IStrategyCreateModel
		public void selectGameFigure(ScriptableObject figureData)
		{
			ScriptableObjectFigureModel _scriptableObjectFigureModel = (ScriptableObjectFigureModel)figureData;

			this.figureModel = _scriptableObjectFigureModel.getTriangleEquilateral ();
			this.selectBase ();
			this.gameFigureBase.GetComponent<FigureModelController> ().IsBase = true;
		}

		public void createModelFigure()
		{
			this.vertexPointList = new List<VertexPoint> ();

			this.figureRotationX = Random.Range (-90, -100);
			this.figureRotationY = Random.Range (100, 150);
			this.instantiateFigure ();

			if (this.gameFigureBase.GetComponent<FigureModelController> ().freeVertexAvailable ())
				this.selectStripe ();
			else	
				this.selectBase ();

			this.figureRotationY = Random.Range (200, 250);
			this.instantiateFigure ();
			
		}

		public void gameVertexPoint(ref List<VertexPoint> currentVertexPointList)
		{
			int _indexInList;
			foreach(VertexPoint vertexPoint in this.vertexPointList)
			{
				_indexInList = currentVertexPointList.FindIndex(x => x.VertexPointPosition == vertexPoint.VertexPointPosition);
				if(_indexInList > -1)
				{
					foreach(Vector3 neighbour in vertexPoint.NeighbourVectorList)
					{
						currentVertexPointList[_indexInList].NeighbourVectorList.Add(neighbour);
					}
				}
				else
				{
					currentVertexPointList.Add(vertexPoint);
				}
			}
		}
		#endregion

		#region Private methods
		private void instantiateFigure()
		{
			GameObject _cloneFigureBase, _gameNewFigure;
			Transform _cloneStripe;

			_cloneFigureBase = this.gameFigureBase.GetComponent<FigureModelController> ().FigureModel.setStripesList (this.gameFigureBase);
			_cloneStripe = _cloneFigureBase.transform.GetChild (this.stripeIndex);
			_cloneStripe.parent = this.figureContainer.transform;

			_gameNewFigure = (GameObject)GameObject.Instantiate(this.figureModel.Prefab,new Vector3(0,0,0), Quaternion.Euler(new Vector3(0,0,0)));
			_gameNewFigure.transform.parent = this.figureContainer.transform;
			_gameNewFigure.name = this.figureModel.FigureName;
			_gameNewFigure.transform.localPosition = _cloneStripe.localPosition;
			_gameNewFigure.transform.localRotation = Quaternion.Euler(new Vector3(this.figureRotationX, this.figureRotationY, 0));

			this.figureModel.updateNeighbourVectorList (this.figureContainer, _gameNewFigure);
			_gameNewFigure.GetComponent<FigureModelController> ().FigureModel = this.figureModel;
			_gameNewFigure.GetComponent<FigureModelController> ().setFigureVertexCount ();
			this.gameFigureBase.GetComponent<FigureModelController> ().addNewNeighbour (this.stripeIndex, this.figureModel);
			this.gameFigureBase.GetComponent<FigureModelController> ().NumberFiguresSupported ++;
			this.iterateListUpdated ();

			_cloneStripe.parent = _cloneFigureBase.transform;
			_cloneFigureBase.transform.parent = null;
			GameObject.Destroy (_cloneFigureBase);
		}

		private void iterateListUpdated()
		{
			foreach (VertexPoint newVertexPoint in this.figureModel.VertexPointList) 
			{
				this.vertexPointList.Add(newVertexPoint);
			}
		}

		private void selectBase()
		{
			int _randomContainerindex;
			bool _available = false;

			while (!_available) 
			{
				_randomContainerindex = Random.Range(0, this.figureContainer.transform.childCount);
				this.gameFigureBase = this.figureContainer.transform.GetChild(_randomContainerindex).gameObject;
				if(this.gameFigureBase.GetComponent<FigureModelController>().freeVertexAvailable())
				{
					_available = this.selectStripe();
				}
			}
		}

		private bool selectStripe()
		{
			bool _available = false;
			int _randomStripeIndex = -1, _neighbourVertexCount;
			while(!_available)
			{
				_randomStripeIndex = Random.Range (1, this.gameFigureBase.transform.childCount);
				_neighbourVertexCount = this.gameFigureBase.GetComponent<FigureModelController>().neighbourVertexCount(_randomStripeIndex);
				if(_neighbourVertexCount < 4)
					_available = true;
			}
			Debug.Log ("Consigui vertice");
			this.stripeIndex = _randomStripeIndex;
			return true;
		}
		#endregion

		#region Properties
		public List<VertexPoint> VertexPointList
		{
			get { return this.vertexPointList; }
		}
		#endregion
	}
}