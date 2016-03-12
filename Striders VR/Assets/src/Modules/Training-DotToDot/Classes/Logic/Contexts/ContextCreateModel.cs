using UnityEngine;
using StridersVR.Domain.DotToDot;
using StridersVR.Modules.DotToDot.Logic.StrategyInterfaces;

namespace StridersVR.Modules.DotToDot.Logic.Contexts
{
	public class ContextCreateModel
	{
		private IStrategyCreateModel strategyCreateModel;

		public ContextCreateModel()
		{
		
		}


		#region Service methods
		public void createModelFigure(FigureModel figure)
		{
			this.strategyCreateModel.createModelFigure (figure);
		}
		#endregion

		#region Properties
		public IStrategyCreateModel StrategyCreateModel
		{
			get { return this.strategyCreateModel; }
			set { this.strategyCreateModel = value; }
		}
		#endregion
	}
}