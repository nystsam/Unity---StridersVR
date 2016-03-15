using UnityEngine;
using System.Collections.Generic;
using StridersVR.Modules.DotToDot.Logic.StrategyInterfaces;
using StridersVR.Domain.DotToDot;

namespace StridersVR.Modules.DotToDot.Logic.Strategies
{
	public class StrategyCreateModelComposite : IStrategyCreateModel
	{
		private int selectedStrategyIndex = 0;
		private List<IStrategyCreateModel> childStategyCreateModelList;

		public StrategyCreateModelComposite ()
		{
			this.childStategyCreateModelList = new List<IStrategyCreateModel> ();
		}


		#region IStrategyCreateModel
		public void createModelFigure()
		{
			this.childStategyCreateModelList [this.selectedStrategyIndex].createModelFigure ();
		}

		public void gameVertexPoint(ref List<VertexPoint> currentVertexPointList)
		{
		}
		#endregion

		#region Properties
		public int SelectedStrategyIndex
		{
			get { return this.selectedStrategyIndex; }
			set { this.selectedStrategyIndex = value; }
		}
		
		public int StrategyCreateModelListCount
		{
			get { return this.childStategyCreateModelList.Count; }
		}
		#endregion
	}
}

