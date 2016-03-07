using UnityEngine;
using System.Collections.Generic;
using StridersVR.Domain.TrainOfThought;
using StridersVR.ScriptableObjects.TrainOfThought;
using StridersVR.Modules.TrainOfThought.Logic.StrategyInterfaces;

namespace StridersVR.Modules.TrainOfThought.Logic.Strategies
{
	public class StrategyTrainGenerationThreeSeries : IStrategyTrainGeneration
	{
		private GameObject gamePlatform;
		private Vector3 startPoint;
		private List<ColorTrain> newColorTrainList;

		public StrategyTrainGenerationThreeSeries (GameObject gamePlatform)
		{
			this.gamePlatform = gamePlatform;
			this.newColorTrainList = new List<ColorTrain>();
		}


		#region IStrategyTrainGeneration
		public float selectTrain(ScriptableObject genericColorTrainData)
		{
			int _randomColorStationIndex;
			ColorStation _trainDestination;
			ColorTrain _trainToStart;
			ColorTrain _previousTrain = null;
			ScriptableObjectColorTrain _gameColorStationsData = (ScriptableObjectColorTrain)genericColorTrainData;
			GameObject _stationContainer = this.gamePlatform.transform.FindChild ("ColorStationContainer").gameObject;
			float _randomTimer = Random.Range (4, 7);

			this.startPoint = _gameColorStationsData.StartPoint;
			for (int count = 0; count < 3; count++) 
			{
				_randomColorStationIndex = Random.Range (0, _stationContainer.transform.childCount);
				_trainDestination = _stationContainer.transform.GetChild (_randomColorStationIndex).gameObject.GetComponent<StationController> ().ColorStation;
				_trainToStart = _gameColorStationsData.TrainsList.Find (x => x.TrainDestination.StationName.GetHashCode() == _trainDestination.StationName.GetHashCode());
				if (_previousTrain == null) 
				{
					_previousTrain = _trainToStart;
					this.addColorTrain (_trainToStart);
				} 
				else if (_previousTrain == _trainToStart) 
				{
					count--;
				} 
				else 
				{
					_previousTrain = _trainToStart;
					this.addColorTrain (_trainToStart);
				}
			}

			return _randomTimer;
		}

		public bool instantiateTrain()
		{
			GameObject _newTrain;
			ColorTrain _newColorTrain = this.newColorTrainList[0];
			GameObject _trainContainer = this.gamePlatform.transform.FindChild ("ColorTrainContainer").gameObject;

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

		#region Private methods
		private void addColorTrain(ColorTrain newColorTrain)
		{
			if (newColorTrain != null) 
			{
				this.newColorTrainList.Add(newColorTrain);
			}
		}
		#endregion
	}
}

