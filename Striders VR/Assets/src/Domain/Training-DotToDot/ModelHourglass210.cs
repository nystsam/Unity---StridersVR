using UnityEngine;
using StridersVR.Domain.DotToDot;
using StridersVR.Modules.DotToDot.Logic.StrategyInterfaces;
using StridersVR.Modules.DotToDot.Logic.Strategies;

namespace StridersVR.Domain.DotToDot
{
	[System.Serializable]
	public class ModelHourglass210 : FigureModel
	{

		public ModelHourglass210 (string figureName, GameObject prefab) : base(figureName, prefab)
		{
		}

		public override bool isResizableFigure()
		{
			return false;
		}

		public override IStrategyCreateModel generator(GameObject container)
		{
			return null;
		}
	}
}

