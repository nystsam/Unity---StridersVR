using UnityEngine;
using System;
using System.Collections.Generic;
using StridersVR.Modules.Menu.Data;
using StridersVR.Domain;

namespace StridersVR.Modules.Menu.Logic
{
	public class StatisticManager
	{
		private List<Training> trainingList;

		private DbTraining db;


		public StatisticManager ()
		{
			this.db = new DbTraining();
			this.trainingList = new List<Training>();

			this.trainingList = this.db.getTrainingList();
		}
		
		public void InstantiateButtons(GameObject container)
		{
			GameObject _buttonPrefab = Resources.Load ("Prefabs/Menu/UIButtonSt", typeof(GameObject)) as GameObject;
			float _posX = -2.5f;

			foreach(Training t in this.trainingList)
			{
				GameObject _clone;

				_clone = (GameObject)GameObject.Instantiate(_buttonPrefab);
				_clone.transform.parent = container.transform;
				_clone.transform.localPosition = new Vector3(_posX, 0, -0.6f);
				_clone.transform.localRotation = Quaternion.Euler(Vector3.zero);

				//_clone.GetComponentInChildren<UiButtonSelectStatistic>().SetTraining(t);
				_posX += 2.5f;
			}
		}
	}
}

