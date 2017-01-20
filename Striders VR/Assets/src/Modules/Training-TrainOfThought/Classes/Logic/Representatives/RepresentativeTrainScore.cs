using System;
using UnityEngine;
using UnityEngine.UI;
using StridersVR.Domain.TrainOfThought;

namespace StridersVR.Modules.TrainOfThought.Logic.Representatives
{
	public class RepresentativeTrainScore
	{
		private Text currentHit = null;
		private Text totalHits = null;
		private GameObject colorTrainContainer;
		private bool isSucceeded = false;
		public bool IsSucceeded {
			get { return isSucceeded; }
		}

		public RepresentativeTrainScore (ref Text currentHit, ref Text totalHits, GameObject platform)
		{
			this.currentHit = currentHit;
			this.totalHits = totalHits;
			this.colorTrainContainer = platform.transform.FindChild ("ColorTrainContainer").gameObject;
		}


		public void trainArrival(Collider inGameStationColor, GameObject inGameTrainColor, ColorTrain currentColorTrain)
		{
			int _score = 0;

			if (inGameStationColor.GetComponent<StationController> ().ColorStation.StationName == currentColorTrain.TrainDestination.StationName) 
			{
				this.isSucceeded = true;
				_score++;
			}

			this.setNewScoreAndTotal (_score);
			GameObject.Destroy (inGameTrainColor);
		}

		public void destroyTrain(GameObject inGameTrainColor, ActivityFocusRoute currentActivity)
		{
			currentActivity.IsTrainSucceeded = this.isSucceeded;
			StatisticsFocusRouteController.Current.addNewResult(currentActivity);
			GameObject.Destroy (inGameTrainColor);

			this.isSucceeded = false;
		}

		public bool remainingTrains()
		{
			if (this.colorTrainContainer.transform.childCount <= 0) 
			{
				return true;
			}
			return false;
		}

		#region Private Methods
		private void setNewScoreAndTotal(int newScore)
		{
			int _current = int.Parse (this.currentHit.text);
			int _total = int.Parse (this.totalHits.text);

			_current += newScore;
			_total++;

			this.currentHit.text = _current.ToString();
			this.totalHits.text = _total.ToString();

		}
		#endregion
	}
}

