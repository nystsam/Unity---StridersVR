using UnityEngine;
using System.Collections.Generic;
using StridersVR.Domain.TrainOfThought;
using StridersVR.ScriptableObjects.TrainOfThought;
using StridersVR.Modules.TrainOfThought.Logic.StrategyInterfaces;

namespace StridersVR.Modules.TrainOfThought.Logic.Strategies
{
	public class StrategyTrainGenerationEveryoneRandom : IStrategyTrainGeneration
	{
		private GameObject gamePlatform;
		private Vector3 startPoint;
		private List<ColorTrain> newColorTrainList;

		public StrategyTrainGenerationEveryoneRandom (GameObject gamePlatform)
		{
			this.gamePlatform = gamePlatform;
			this.newColorTrainList = new List<ColorTrain> ();
		}


		#region IStrategyTrainGeneration
		public float selectTrain(ScriptableObject genericColorTrainData)
		{
			ColorStation _trainDestination;
			ColorTrain _trainToStart;
			ScriptableObjectColorTrain _gameColorStationsData = (ScriptableObjectColorTrain)genericColorTrainData;
			GameObject _stationContainer = this.gamePlatform.transform.FindChild ("ColorStationContainer").gameObject;
			float _randomTimer = Random.Range (5, 7);

			this.startPoint = _gameColorStationsData.StartPoint;
			for (int count = 0; count < _stationContainer.transform.childCount; count++) 
			{
				_trainDestination = _stationContainer.transform.GetChild (count).gameObject.GetComponent<StationController> ().ColorStation;
				_trainToStart = _gameColorStationsData.TrainsList.Find (x => x.TrainDestination.StationName.GetHashCode() == _trainDestination.StationName.GetHashCode());
				this.newColorTrainList.Add (_trainToStart);
			}

			return _randomTimer;
		}

		public bool instantiateTrain()
		{
			GameObject _newTrain;
			ColorTrain _newColorTrain;
			GameObject _trainContainer = this.gamePlatform.transform.FindChild ("ColorTrainContainer").gameObject;
			int _randomIndex = Random.Range (0, this.newColorTrainList.Count);

			_newColorTrain = this.newColorTrainList [_randomIndex];
			_newTrain = (GameObject)GameObject.Instantiate (_newColorTrain.Prefab, this.startPoint, Quaternion.Euler(new Vector3(0,0,0)));
			_newTrain.name = _newColorTrain.TrainName;
			_newTrain.GetComponent<TrainController> ().ColorTrain = _newColorTrain;
			_newTrain.transform.parent = _trainContainer.transform;

			this.newColorTrainList.Remove (_newColorTrain);
			if (this.newColorTrainList.Count <= 0)
				return true;

			return false;
		}
		#endregion
	}
}

