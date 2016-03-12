using UnityEngine;
using System.Collections;
using StridersVR.Modules.DotToDot.Logic.StrategyInterfaces;
using StridersVR.Domain.DotToDot;

namespace StridersVR.Modules.DotToDot.Logic.Strategies
{
	public class StrategyCreateModelTriangleEquilateral :  IStrategyCreateModel
	{
		private GameObject figureContainer;

		public StrategyCreateModelTriangleEquilateral(GameObject figureContainer)
		{
			this.figureContainer = figureContainer;
		}


		#region IStrategyCreateModel
		public void createModelFigure(FigureModel figure)
		{
			GameObject _newFigure;

			if (this.figureContainer.transform.childCount > 0) 
			{
			
			} 
			else 
			{
				_newFigure = (GameObject)GameObject.Instantiate(figure.Prefab,new Vector3(0,0,0), Quaternion.Euler(new Vector3(0,0,0)));
				_newFigure.transform.parent = this.figureContainer.transform;
				_newFigure.name = figure.FigureName;
				_newFigure.transform.localPosition = new Vector3(0,-20,0);
				this.rotateFigure(_newFigure);
			}
		}
		#endregion

		#region Private methods
		private void rotateFigure(GameObject newFigure)
		{
			float _rotationX = Random.Range (0, -180);
			float _rotationY = Random.Range (0, 360);
			Vector3 _newRotation = new Vector3 (0,0,0);

			_newRotation.x = _rotationX;
			_newRotation.y = _rotationY;

			newFigure.transform.localRotation = Quaternion.Euler(_newRotation);
		}
		#endregion
	}
}