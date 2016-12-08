using UnityEngine;
using System.Collections.Generic;
using StridersVR.Modules.DotToDot.Logic.StrategyInterfaces;
using StridersVR.Domain.DotToDot;

namespace StridersVR.Modules.DotToDot.Logic.Strategies
{
	public class StrategyCreateModelComposite : IStrategyCreateModel
	{
		private int strategyIndex = 0;
		private List<IStrategyCreateModel> stategyCreateModelList;

		public StrategyCreateModelComposite ()
		{
			this.stategyCreateModelList = new List<IStrategyCreateModel> ();
		}


		#region IStrategyCreateModel
		public Model createModel()
		{
			return this.stategyCreateModelList [this.strategyIndex].createModel ();
		}
//		public void selectGameFigure(ScriptableObject figureData)
//		{
//			this.stategyCreateModelList [this.selectedStrategyIndex].selectGameFigure (figureData);
//		}
//
//		public void createModelFigure()
//		{
//			this.stategyCreateModelList [this.selectedStrategyIndex].createModelFigure ();
//		}
//
//		public void gameVertexPoint(ref List<VertexPoint> currentVertexPointList)
//		{
//			this.stategyCreateModelList [this.selectedStrategyIndex].gameVertexPoint (ref currentVertexPointList);
//		}
//
//		public int numberOfPoints()
//		{
//			return 	this.stategyCreateModelList [this.selectedStrategyIndex].numberOfPoints ();
//		}
		#endregion

		public void addStrategy(IStrategyCreateModel strategy)
		{
			this.stategyCreateModelList.Add (strategy);
		}

		#region Properties
		public int StrategyIndex
		{
			get { return this.strategyIndex; }
			set { this.strategyIndex = value; }
		}
		
		public int StrategyCount
		{
			get { return this.stategyCreateModelList.Count; }
		}
		#endregion
	}
}

