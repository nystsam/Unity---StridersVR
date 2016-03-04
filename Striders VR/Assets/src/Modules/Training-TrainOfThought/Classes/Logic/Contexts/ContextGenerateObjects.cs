using UnityEngine;
using StridersVR.ScriptableObjects.TrainOfThought;
using StridersVR.Modules.TrainOfThought.Logic.StrategyInterfaces;

namespace StridersVR.Modules.TrainOfThought.Logic.Contexts
{
	public class ContextGenerateObjects
	{
		private IStrategyGeneratePlatformObjects strategyGenerateObjects;

		public ContextGenerateObjects ()
		{
		}


		#region Service methods
		public void generateStations(ScriptableObject gameColorStationsData)
		{
			this.strategyGenerateObjects.generateStations (gameColorStationsData);
		}

		public void generateCurvesDirection(ScriptableObject gameCurvesDirectionData)
		{
			this.strategyGenerateObjects.generateCurvesDirection (gameCurvesDirectionData);
		}

		public void generateSwitchs(ScriptableObject gameRailroadSwitchData)
		{
			this.strategyGenerateObjects.generateSwitchs (gameRailroadSwitchData);
		}
		#endregion

		#region Properties
		public IStrategyGeneratePlatformObjects StrategyGenerateObjects
		{
			get { return this.strategyGenerateObjects; }
			set { this.strategyGenerateObjects = value; }
		}
		#endregion
		
	}
}

