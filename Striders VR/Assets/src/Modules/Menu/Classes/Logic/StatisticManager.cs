using UnityEngine;
using System.Collections.Generic;
using StridersVR.Modules.Menu.Data;
using StridersVR.Domain;
using StridersVR.Buttons;

namespace StridersVR.Modules.Menu.Logic
{
	public class StatisticManager
	{
		private List<Training> trainingList;
		private List<Statistic> statisticsList;

		private DbTraining db;
		private DbStatistics dbStats;


		public StatisticManager ()
		{
			this.db = new DbTraining();
			this.dbStats = new DbStatistics();

			this.trainingList = new List<Training>();
			this.statisticsList = new List<Statistic>();

			this.trainingList = this.db.getTrainingList();
		}

		public void GetLastPlays(int userId, int trainingId)
		{
			this.statisticsList = this.dbStats.GetLastPlays(userId, trainingId);
		}

		public void GetTodayPlays(int userId, int trainingId)
		{
			this.statisticsList = this.dbStats.GetTodayPlays(userId, trainingId);
		}

		public void GetYesterdayPlays(int userId, int trainingId)
		{
			this.statisticsList = this.dbStats.GetYesterdayPlays(userId, trainingId);
		}

		public void RemovePanelInfo(GameObject panelContainer)
		{
			foreach(Transform child in panelContainer.transform)
			{
				GameObject.Destroy(child.gameObject);
			}
		}

		public void InstantiateButtons(GameObject panelContainer)
		{
			GameObject _buttonPrefab = Resources.Load ("Prefabs/Menu/InfoButton", typeof(GameObject)) as GameObject;
			float _posY = 2.8f;

			if(this.statisticsList.Count > 0)
			{
				foreach(Statistic s in this.statisticsList)
				{
					GameObject _clone;
					
					_clone = (GameObject)GameObject.Instantiate(_buttonPrefab);
					_clone.transform.parent = panelContainer.transform;
					_clone.transform.localPosition = new Vector3(1.85f, _posY, -0.25f);
					
					_clone.GetComponentInChildren<ButtonInfo>().SetStatistic(s);
					_posY -= 0.6f;
				}
			}
			else
			{
				GameObject _textPrefab = Resources.Load("Prefabs/Menu/NoResult", typeof(GameObject)) as GameObject;
				GameObject _clone;

				_clone = (GameObject)GameObject.Instantiate(_textPrefab);
				_clone.transform.parent = panelContainer.transform;
				_clone.transform.localPosition = new Vector3(-2.1f, _posY, -0.25f);

			}
		}

		public void InstantiateButtons(GameObject container, GameObject current, GameObject target)
		{
			GameObject _buttonPrefab = Resources.Load ("Prefabs/Menu/UIButtonSt", typeof(GameObject)) as GameObject;
			float _posX = -1.5f;
			float _posY = 1.1f;

			foreach(Training t in this.trainingList)
			{
				GameObject _clone;

				_clone = (GameObject)GameObject.Instantiate(_buttonPrefab);
				_clone.transform.parent = container.transform;
				_clone.transform.localPosition = new Vector3(_posX, _posY, -0.7f);
				_clone.transform.localRotation = Quaternion.Euler(Vector3.zero);

				_clone.GetComponentInChildren<ButtonStatisticsTraining>().SetTraining(t);
				_clone.GetComponentInChildren<ButtonStatisticsTraining>().SetContainerTransition(current, target);
				_posY -= 1.2f;
			}
			container.transform.parent = current.transform.GetChild(0);
			container.transform.localPosition = Vector3.zero;
		}
	}
}

