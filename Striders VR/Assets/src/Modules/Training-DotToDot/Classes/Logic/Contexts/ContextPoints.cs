using UnityEngine;
using StridersVR.Domain.DotToDot;
using StridersVR.Modules.DotToDot.Logic.StrategyInterfaces;

namespace StridersVR.Modules.DotToDot.Logic.Contexts
{
	public class ContextPoints
	{
		private IStrategyPoints strategyPoints;

		public ContextPoints ()
		{
		}


		#region Service methods
		public PointsContainer createPoints()
		{
			return this.strategyPoints.createPoints ();
		}
		#endregion

		#region Properties
		public IStrategyPoints StrategyPoints
		{
			get { return this.strategyPoints; }
			set { this.strategyPoints = value; }
		}
		#endregion
	}
}

