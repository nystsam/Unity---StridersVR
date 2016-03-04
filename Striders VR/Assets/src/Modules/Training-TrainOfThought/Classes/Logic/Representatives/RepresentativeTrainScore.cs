using System;
using UnityEngine;
using UnityEngine.UI;
using StridersVR.Domain.TrainOfThought;

namespace StridersVR.Modules.TrainOfThought.Logic.Representatives
{
	public class RepresentativeTrainScore
	{
		private Text currentHit = null;
		private Text totalHits= null;

		public RepresentativeTrainScore (ref Text currentHit, ref Text totalHits)
		{
			this.currentHit = currentHit;
			this.totalHits = totalHits;
		}


		public void trainArrival(Collider inGameStationColor, GameObject inGameTrainColor, ColorTrain currentColorTrain)
		{
			int _score = 0;

			if (inGameStationColor.GetComponent<StationController> ().ColorStation.StationName == currentColorTrain.TrainDestination.StationName) 
			{
				_score++;
			}

			this.setNewScoreAndTotal (_score);
			GameObject.Destroy (inGameTrainColor);
		}

		private void setNewScoreAndTotal(int newScore)
		{
			int _current = int.Parse (this.currentHit.text);
			int _total = int.Parse (this.totalHits.text);

			_current += newScore;
			_total++;

			this.currentHit.text = _current.ToString();
			this.totalHits.text = _total.ToString();

		}
	}
}

