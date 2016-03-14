using UnityEngine;
using System.Collections;
using StridersVR.Modules.DotToDot.Logic.StrategyInterfaces;
using StridersVR.Domain.DotToDot;

namespace StridersVR.Modules.DotToDot.Logic.Strategies
{
	public class StrategyCreateModelTriangleEquilateral :  IStrategyCreateModel
	{
		private GameObject figureContainer;
		private GameObject gameFigureBase;
		private GameObject gameNewFigure;
		private FigureModel figureModel;

		public StrategyCreateModelTriangleEquilateral(GameObject figureContainer)
		{
			this.figureContainer = figureContainer;
		}


		#region IStrategyCreateModel
		public void createModelFigure(FigureModel figure)
		{
			int _randomContainerindex;
			this.figureModel = figure;

			if (this.figureContainer.transform.childCount > 0) 
			{
				_randomContainerindex = Random.Range(0, this.figureContainer.transform.childCount);
				this.gameFigureBase = this.figureContainer.transform.GetChild(_randomContainerindex).gameObject;
				this.instantiateAtVertex();
			} 
			else 
			{
				this.gameNewFigure = (GameObject)GameObject.Instantiate(this.figureModel.Prefab,new Vector3(0,0,0), Quaternion.Euler(new Vector3(0,0,0)));
				this.gameNewFigure.transform.parent = this.figureContainer.transform;
				this.gameNewFigure.name = this.figureModel.FigureName;
				this.gameNewFigure.transform.localPosition = new Vector3(0,-20,0);
				this.setFigureRotation(0, -180, 0, 360);
			}
		}
		#endregion

		#region Private methods
		private void instantiateAtVertex()
		{
			int _randomVertexChild = Random.Range (0, this.gameFigureBase.transform.childCount);
			Transform _stripe = this.gameFigureBase.transform.GetChild (_randomVertexChild);

			_stripe.parent = this.figureContainer.transform;
			this.gameNewFigure = (GameObject)GameObject.Instantiate(this.figureModel.Prefab,new Vector3(0,0,0), Quaternion.Euler(new Vector3(0,0,0)));
			this.gameNewFigure.transform.parent = this.figureContainer.transform;
			this.gameNewFigure.name = this.figureModel.FigureName;
			this.gameNewFigure.transform.localPosition = _stripe.localPosition;
			_stripe.parent = this.gameFigureBase.transform;

			this.setFigureRotation ((int)this.gameFigureBase.transform.localPosition.x, -160,
			                       (int)this.gameFigureBase.transform.localPosition.y, 320);



		}

		private void setFigureRotation(int minX, int maxX, int minY, int maxY)
		{
			float _rotationX = Random.Range (minX, maxX);
			float _rotationY = Random.Range (minY, maxY);

			this.gameNewFigure.transform.localRotation = Quaternion.Euler(new Vector3(_rotationX, _rotationY, 0));
		}
		#endregion
	}
}