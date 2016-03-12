using UnityEngine;
using StridersVR.Domain.DotToDot;
using StridersVR.Modules.DotToDot.Logic.StrategyInterfaces;
using StridersVR.Modules.DotToDot.Logic.Strategies;

namespace StridersVR.Domain.DotToDot
{
	[System.Serializable]
	public class ModelTriangleEquilateral : FigureModel
	{

		public ModelTriangleEquilateral ()
		{
		}


		public override bool isResizableFigure()
		{
			return false;
		}

		public override IStrategyCreateModel generator(GameObject container)
		{
			return new StrategyCreateModelTriangleEquilateral(container);
		}
	}
}

