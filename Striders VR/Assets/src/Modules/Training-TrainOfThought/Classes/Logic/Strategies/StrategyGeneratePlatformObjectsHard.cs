using System.Collections.Generic;
using UnityEngine;
using StridersVR.Domain.TrainOfThought;
using StridersVR.ScriptableObjects.TrainOfThought;
using StridersVR.Modules.TrainOfThought.Logic.StrategyInterfaces;

namespace StridersVR.Modules.TrainOfThought.Logic.Strategies
{
	public class StrategyGeneratePlatformObjectsHard : IStrategyGeneratePlatformObjects
	{
		private int totalStations = 8;
		private GameObject gamePlatform;
		private List<ColorStation> selectedRandomColorStationsList;
		
		public StrategyGeneratePlatformObjectsHard (GameObject trainPlatform)
		{
			this.gamePlatform = trainPlatform;
			this.selectedRandomColorStationsList = new List<ColorStation> ();
		}
		
		
		#region IStrategyGeneratePlatformObjects
		public void generateStations(ScriptableObject genericGameColorStationsData)
		{
			ColorStationSpawnPoint _stationSpawnPoint;
			ScriptableObjectColorStation _gameColorStationsData = (ScriptableObjectColorStation)genericGameColorStationsData;
			List<ColorStation> _auxList = this.fillAuxList (_gameColorStationsData.StationsList);
			int _indexSpawnPoint = 0;
			GameObject _colorStationsContainer = this.gamePlatform.transform.FindChild ("ColorStationContainer").gameObject;
			
			this.selectColorStations (_auxList);
			foreach (ColorStation _station in this.selectedRandomColorStationsList) 
			{
				_stationSpawnPoint = _gameColorStationsData.StationSpawnPoints [_indexSpawnPoint];
				GameObject _newStation = (GameObject) GameObject.Instantiate (_station.Prefab, _stationSpawnPoint.StationPosition, Quaternion.Euler(_stationSpawnPoint.StationRotation));
				_newStation.GetComponent<StationController> ().ColorStation = _station;
				_newStation.name = _station.StationName;
				_newStation.transform.parent = _colorStationsContainer.transform;
				_indexSpawnPoint++;
			}
		}
		
		public void generateCurvesDirection(ScriptableObject genericGameCurvesDirectionData)
		{
			ScriptableObjectCurveDirection _gameCurvesDirectionData = (ScriptableObjectCurveDirection)genericGameCurvesDirectionData;
			GameObject _trainTracksContainer = this.gamePlatform.transform.FindChild("TrainTracksContainer").gameObject;
			CurveDirection _curve = _gameCurvesDirectionData.CurveDirectionListEasyMode [1];

			GameObject _newCurve = (GameObject)GameObject.Instantiate (_curve.Prefab, _curve.Position, _curve.Prefab.transform.rotation);
			_newCurve.GetComponent<CurveController> ().direction = _curve.Direction;
			_newCurve.transform.parent = _trainTracksContainer.transform;
		}
		
		public void generateSwitchs(ScriptableObject genericGameRailroadSwitchData)
		{
			float _localHorizontalSeparation = 0;
			ScriptableObjectRailroadSwitch _gameRailroadSwitchData = (ScriptableObjectRailroadSwitch)genericGameRailroadSwitchData;
			GameObject _raildroadSwitchContainer = this.gamePlatform.transform.FindChild ("RailroadSwitchContainer").gameObject;
			GameObject _trainTracksContainer = this.gamePlatform.transform.FindChild ("TrainTracksContainer").gameObject;
			GameObject _playerPanelButtons = GameObject.FindGameObjectWithTag("PlayerPanelButtons");

			_playerPanelButtons.transform.localPosition = new Vector3 (-6, -6, 8);
			this.adjustPlayerPanel (_playerPanelButtons);
			foreach (RailroadSwitch _switch in _gameRailroadSwitchData.RailroadSwitchListHardMode) 
			{
				GameObject _newButtonSwitch;
				GameObject _newRailroadSwitch;
				_newRailroadSwitch = (GameObject)GameObject.Instantiate (_switch.Prefab, _switch.Position, Quaternion.Euler(_switch.RotationEuler));
				_newRailroadSwitch.name = _switch.Name;
				_newRailroadSwitch.transform.parent = _raildroadSwitchContainer.transform;
				_newRailroadSwitch.GetComponent<RailroadSwitchController> ().directions = _switch.Directions;
				
				_newButtonSwitch = (GameObject) GameObject.Instantiate (_gameRailroadSwitchData.PlayerButton, new Vector3(0,0,0), Quaternion.Euler(new Vector3(0,0,0)));
				_newButtonSwitch.transform.parent = _playerPanelButtons.transform;
				_newButtonSwitch.GetComponent<BoundingBoxButtonController>().AttachedRailroadSwitch = _newRailroadSwitch;
				_newButtonSwitch.transform.localPosition = new Vector3(_localHorizontalSeparation, 0, 0);
				_newButtonSwitch.transform.localRotation = Quaternion.Euler(new Vector3(0,0,0));
				_newButtonSwitch.transform.GetChild(0).GetComponent<SpringJoint>().autoConfigureConnectedAnchor = true;
				_localHorizontalSeparation += 2f;
			}
			
			foreach (Track _track in _gameRailroadSwitchData.TrackListMediumMode) 
			{
				GameObject _newTrack;
				_newTrack = (GameObject)GameObject.Instantiate(_track.Prefab, _track.Position, Quaternion.Euler(_track.EulerRotation));
				_newTrack.transform.localScale = new Vector3(_newTrack.transform.localScale.x, _newTrack.transform.localScale.y, _track.Scale.z);
				_newTrack.transform.parent = _trainTracksContainer.transform;
			}

			foreach (Track _track in _gameRailroadSwitchData.TrackListHardMode) 
			{
				GameObject _newTrack;
				_newTrack = (GameObject)GameObject.Instantiate(_track.Prefab, _track.Position, Quaternion.Euler(_track.EulerRotation));
				_newTrack.transform.localScale = new Vector3(_newTrack.transform.localScale.x, _newTrack.transform.localScale.y, _track.Scale.z);
				_newTrack.transform.parent = _trainTracksContainer.transform;
			}
			
		}
		#endregion
		
		#region Private methods
		private List<ColorStation> fillAuxList(List<ColorStation> colorStationDataList)
		{
			List<ColorStation> _auxList = new List<ColorStation> ();
			foreach (ColorStation _station in colorStationDataList) 
			{
				_auxList.Add (_station);
			}
			return _auxList;
		}
		
		private void selectColorStations(List<ColorStation> auxList)
		{
			for (int _index = 0; _index < this.totalStations; _index++) 
			{
				int _randomIndex = Random.Range (0, auxList.Count);
				ColorStation _selectedSation = auxList.GetRange(_randomIndex, 1)[0];
				this.selectedRandomColorStationsList.Add (_selectedSation);
				auxList.Remove (_selectedSation);
			}
		}

		private void adjustPlayerPanel(GameObject playerPanel)
		{
			Transform center = playerPanel.transform.FindChild("CenterPanel");
			Transform right = playerPanel.transform.FindChild("RightPanel");
			
			center.localScale = new Vector3 (14, center.localScale.y, center.localScale.z);
			center.localPosition = new Vector3 (6, center.localPosition.y, center.localPosition.z);
			right.localPosition = new Vector3 (13.85f, right.localPosition.y, right.localPosition.z);
		}
		#endregion
	}
}

