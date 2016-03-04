using UnityEngine;
using StridersVR.Domain.TrainOfThought;
using StridersVR.Modules.TrainOfThought.Logic.Contexts;
using StridersVR.Modules.TrainOfThought.Logic.Strategies;
using StridersVR.Modules.TrainOfThought.Logic.StrategyInterfaces;

namespace StridersVR.Modules.TrainOfThought.Logic.Representatives
{
	/// <summary>
	/// Train platform object that implements context functions for game objects and trains.
	/// </summary>
	public class RepresentativeTrainPlatform
	{
		private GameObject platform;
		private ContextGenerateObjects contextGenerateObjects;
		private ContextTrainGeneration contextTrainGeneration;
		private StrategyTrainGenerationComposite compositeStrategy;
		private ScriptableObject gameColorStationsData;
		private ScriptableObject gameCurvesDirectionData;
		private ScriptableObject gameRailroadSwitchData;
		private ScriptableObject gameColorTrainsData;
		private float spawnTrainTimer;
		private bool changeStrategy;

		/// <summary>
		/// Initializes a new instance of the <see cref="StridersVR.Modules.TrainOfThought.Logic.TrainPlatform"/> class.
		/// The strategies varies depending on the selected difficulty, where each uses the train platform.
		/// </summary>
		/// <param name="gameDifficulty">Game difficulty.</param>
		/// <param name="platform">Platform.</param>
		public RepresentativeTrainPlatform (string gameDifficulty, GameObject platform)
		{
			this.platform = platform;
			this.contextGenerateObjects = new ContextGenerateObjects ();
			this.contextTrainGeneration = new ContextTrainGeneration ();
			this.compositeStrategy = new StrategyTrainGenerationComposite ();
			this.spawnTrainTimer = 0f;
			this.changeStrategy = true;
			this.assignObjtectData ();

			if (gameDifficulty.Equals ("Easy")) 
			{
				IStrategyGeneratePlatformObjects _strategyGenerateObjectsEasy = new StrategyGeneratePlatformObjectsEasy (this.platform);
				this.contextGenerateObjects.StrategyGenerateObjects = _strategyGenerateObjectsEasy;
			}
		}
			

		/// <summary>
		/// Instantiates the games objects based on the selected difficulty.
		/// </summary>
		public void instantiateObjects()
		{
			this.contextGenerateObjects.generateStations (this.gameColorStationsData);
			this.contextGenerateObjects.generateCurvesDirection (this.gameCurvesDirectionData);
			this.contextGenerateObjects.generateSwitchs (this.gameRailroadSwitchData);
		}

		/// <summary>
		/// First, select one color train available, then instantiates that train.
		/// </summary>
		public void instantiateTrain()
		{
			this.changeStrategy = this.contextTrainGeneration.instantiateTrain ();
		}
			
		/// <summary>
		/// Sets the train generation strategy via Composite pattern.
		/// </summary>
		public void setTrainGenerationStrategy()
		{
			int _selectStrategyIndex = 0;
			if (this.changeStrategy) 
			{
				_selectStrategyIndex = Random.Range (0, this.compositeStrategy.StrategyTrainGenerationListCount);
				this.compositeStrategy.SelectedStrategyIndex = _selectStrategyIndex;

				this.spawnTrainTimer = this.contextTrainGeneration.selectTrain (this.gameColorTrainsData);
			}
		}

		/// <summary>
		/// Creates the train generation strategies and adds to the composite strategy.
		/// </summary>
		public void createTrainGenerationStrategies()
		{
			IStrategyTrainGeneration _strategyTrainGenerationBasic = new StrategyTrainGenerationBasic(this.platform);
			IStrategyTrainGeneration _strategyTrainGenerationThreeSeries = new StrategyTrainGenerationThreeSeries (this.platform);
			IStrategyTrainGeneration _strategyTrainGenerationEveryoneRandom = new StrategyTrainGenerationEveryoneRandom (this.platform);

			this.compositeStrategy.addStrategy (_strategyTrainGenerationBasic);
			this.compositeStrategy.addStrategy (_strategyTrainGenerationThreeSeries);
			this.compositeStrategy.addStrategy (_strategyTrainGenerationEveryoneRandom);
			this.contextTrainGeneration.StrategyTrainGeneration = this.compositeStrategy;
		}

		/// <summary>
		/// Assigns the objtect data, aka ScriptableObjects from the editor, uses the gameobject script component "PlatformController".
		/// </summary>
		private void assignObjtectData()
		{
			this.gameColorStationsData = this.platform.GetComponent<PlatformController> ().gameColorStationsData;
			this.gameCurvesDirectionData = this.platform.GetComponent<PlatformController> ().gameCurvesDirectionData;
			this.gameRailroadSwitchData = this.platform.GetComponent<PlatformController> ().gameRailroadSwitchData;
			this.gameColorTrainsData = this.platform.GetComponent<PlatformController> ().gameColorTrainsData;
		}

		#region Properties
		public float SpawnTrainTimer
		{
			get { return this.spawnTrainTimer; }
			set { this.spawnTrainTimer = value; }
		}
		#endregion
	}
}

