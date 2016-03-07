using UnityEngine;
using StridersVR.Domain.TrainOfThought;
using StridersVR.ScriptableObjects.TrainOfThought;
using StridersVR.Modules.TrainOfThought.Logic.StrategyInterfaces;

namespace StridersVR.Modules.TrainOfThought.Logic.Strategies
{
	public class StrategyTrainGenerationBasic : IStrategyTrainGeneration
	{
		private GameObject gamePlatform;
		private Vector3 startPoint;
		private ColorTrain newColorTrain;
		private float instantiateTrainTimer = 5;

		public StrategyTrainGenerationBasic (GameObject gamePlatform)
		{
			this.gamePlatform = gamePlatform;
		}


		#region IStrategyTrainGeneration
		public float selectTrain(ScriptableObject genericColorTrainData)
		{
			ScriptableObjectColorTrain _gameColorStationsData = (ScriptableObjectColorTrain)genericColorTrainData;
			GameObject _stationContainer = this.gamePlatform.transform.FindChild ("ColorStationContainer").gameObject;
			int _randomColorStationIndex = Random.Range (0, _stationContainer.transform.childCount);

			this.startPoint = _gameColorStationsData.StartPoint;
			ColorStation _trainDestination = _stationContainer.transform.GetChild (_randomColorStationIndex).gameObject.GetComponent<StationController> ().ColorStation;
			ColorTrain _trainToStart = _gameColorStationsData.TrainsList.Find (x => x.TrainDestination.StationName.GetHashCode() == _trainDestination.StationName.GetHashCode());
			if (_trainToStart != null) 
			{
				this.newColorTrain = _trainToStart;
			}
			return this.instantiateTrainTimer;
		}

		public bool instantiateTrain()
		{
			GameObject _newTrain;
			GameObject _trainContainer = this.gamePlatform.transform.FindChild ("ColorTrainContainer").gameObject;

			_newTrain = (GameObject)GameObject.Instantiate (this.newColorTrain.Prefab, this.startPoint, Quaternion.Euler(new Vector3(0,0,0)));
			_newTrain.name = this.newColorTrain.TrainName;
			_newTrain.GetComponent<TrainController> ().ColorTrain = this.newColorTrain;
			_newTrain.transform.parent = _trainContainer.transform;

			return true;
		}
		#endregion
	}
}

