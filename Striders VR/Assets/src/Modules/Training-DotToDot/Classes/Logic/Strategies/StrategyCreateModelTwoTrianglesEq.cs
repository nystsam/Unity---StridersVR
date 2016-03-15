using UnityEngine;
using System.Collections.Generic;
using StridersVR.Modules.DotToDot.Logic.StrategyInterfaces;
using StridersVR.Domain.DotToDot;

namespace StridersVR.Modules.DotToDot.Logic.Strategies
{
	public class StrategyCreateModelTwoTrianglesEq :  IStrategyCreateModel
	{
		private GameObject figureContainer;
		private FigureModel figureModel;
		private GameObject gameFigureBase;
		private List<VertexPoint> vertexPointList;
		private int randomVertexChild = -1;


		public StrategyCreateModelTwoTrianglesEq(GameObject figureContainer, FigureModel figureModel)
		{
			this.figureContainer = figureContainer;
			this.figureModel = figureModel;
			this.vertexPointList = new List<VertexPoint> ();
		}


		#region IStrategyCreateModel
		public void createModelFigure()
		{
			int _randomContainerindex;

			_randomContainerindex = Random.Range(0, this.figureContainer.transform.childCount);
			this.gameFigureBase = this.figureContainer.transform.GetChild(_randomContainerindex).gameObject;
			this.instantiateFigure (0,150);
			this.instantiateFigure (200,300);

		}

		public void gameVertexPoint(ref List<VertexPoint> currentVertexPointList)
		{
			int _indexInList = -1;
			foreach(VertexPoint vertexPoint in this.vertexPointList)
			{
				_indexInList = currentVertexPointList.FindIndex(x => x.VertexPointPosition == vertexPoint.VertexPointPosition);
				if(_indexInList > -1)
				{
					foreach(Vector3 neighbour in vertexPoint.NeighbourVectorList)
					{
						currentVertexPointList[_indexInList].NeighbourVectorList.Add(neighbour);
					}
					_indexInList = -1;
					//this.vertexPointList.Remove(vertexPoint);
				}
				else
				{
					currentVertexPointList.Add(vertexPoint);
				}
			}
		}
		#endregion

		#region Private methods
		private void instantiateFigure(int minY, int maxY)
		{
			GameObject _cloneFigureBase, _gameNewFigure;
			Transform _cloneStripe;
			int _random = this.randomVertexChild;
			float _rotationX = Random.Range (-90, -150);
			float _rotationY = Random.Range (minY, maxY);

			while(this.randomVertexChild == _random)
				_random = Random.Range (0, this.gameFigureBase.transform.childCount);

			this.randomVertexChild = _random;
			_cloneFigureBase = this.gameFigureBase.GetComponent<FigureModelController> ().FigureModel.setStripesList (this.gameFigureBase);
			_cloneStripe = _cloneFigureBase.transform.GetChild (_random);
			_cloneStripe.parent = this.figureContainer.transform;

			_gameNewFigure = (GameObject)GameObject.Instantiate(this.figureModel.Prefab,new Vector3(0,0,0), Quaternion.Euler(new Vector3(0,0,0)));
			_gameNewFigure.transform.parent = this.figureContainer.transform;
			_gameNewFigure.name = this.figureModel.FigureName;
			_gameNewFigure.transform.localPosition = _cloneStripe.localPosition;
			_gameNewFigure.transform.localRotation = Quaternion.Euler(new Vector3(_rotationX, _rotationY, 0));

			this.figureModel.updateNeighbourVectorList (this.figureContainer, _gameNewFigure);
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
		#endregion

		#region Properties
		public List<VertexPoint> VertexPointList
		{
			get { return this.vertexPointList; }
		}
		#endregion
	}
}