using UnityEngine;
using StridersVR.Domain.SpeedPack;

namespace StridersVR.Modules.SpeedPack.Logic.StrategyInterfaces
{
	public interface IStrategySuitcaseCreation
	{
		Suitcase createSuitcase(ScriptableObject genericSuitcasePartData);
	}
}

