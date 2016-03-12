using System.Collections.Generic;
using UnityEngine;
using StridersVR.Domain.DotToDot;
using StridersVR.Modules.DotToDot.Logic.Contexts;
using StridersVR.ScriptableObjects.DotToDot;

namespace StridersVR.Modules.DotToDot.Logic.Representatives
{
	public class RepresentativeModelFigure
	{
		private GameObject figureContainer;
		private ContextCreateModel contextCreateModel;
		private ScriptableObjectFigureModel figureModelData;

		public RepresentativeModelFigure(GameObject figureContainer)
		{
			this.contextCreateModel = new ContextCreateModel ();
			this.figureContainer = figureContainer;
			this.assignObjtectData ();
		}


		public void createFigure()
		{
			FigureModel _figure;

			_figure = this.figureModelData.obtainModels() [0];
			this.contextCreateModel.StrategyCreateModel = _figure.generator (this.figureContainer);

			this.contextCreateModel.createModelFigure (_figure);

		}

		private void assignObjtectData()
		{
			this.figureModelData = this.figureContainer.GetComponent<FigureModelController> ().figureModelData;
		}
	}
}