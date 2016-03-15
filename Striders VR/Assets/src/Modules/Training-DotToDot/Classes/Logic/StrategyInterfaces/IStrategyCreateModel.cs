using UnityEngine;
using System.Collections.Generic;
using StridersVR.Domain.DotToDot;

namespace StridersVR.Modules.DotToDot.Logic.StrategyInterfaces
{
	public interface IStrategyCreateModel 
	{
		void createModelFigure();
		void gameVertexPoint(ref List<VertexPoint> currentVertexPointList);
	}
}
