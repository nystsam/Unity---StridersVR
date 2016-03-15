using UnityEngine;
using System.Collections.Generic;
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
		public void createModelFigure()
		{
			this.strategyCreateModel.createModelFigure ();
		}

		public void gameVertexPoint(ref List<VertexPoint> currentVertexPointList)
		{
			this.strategyCreateModel.gameVertexPoint(ref currentVertexPointList);
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