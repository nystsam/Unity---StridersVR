using UnityEngine;

namespace StridersVR.Modules.TrainOfThought.Logic.StrategyInterfaces
{
	public interface IStrategyGeneratePlatformObjects
	{

		void generateStations(ScriptableObject genericGameColorStationsData);
		void generateCurvesDirection(ScriptableObject genericGameCurvesDirectionData);
		void generateSwitchs(ScriptableObject genericGameRailroadSwitchData);

	}
}

