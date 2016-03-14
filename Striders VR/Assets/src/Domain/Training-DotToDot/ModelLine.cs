using UnityEngine;
using StridersVR.Domain.DotToDot;
using StridersVR.Modules.DotToDot.Logic.StrategyInterfaces;
using StridersVR.Modules.DotToDot.Logic.Strategies;

namespace StridersVR.Domain.DotToDot
{
	[System.Serializable]
	public class ModelLine : FigureModel
	{

		public ModelLine (string figureName, GameObject prefab) : base(figureName, prefab)
		{
		}


		public override bool isResizableFigure()
		{
			return true;
		}

		public override IStrategyCreateModel generator(GameObject container)
		{
			return null;
		}
	}
}

