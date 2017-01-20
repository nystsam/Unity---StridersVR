using UnityEngine;
using System.Collections.Generic;
using StridersVR.Modules.Menu.Data;

namespace StridersVR.Domain.TrainOfThought
{
	public class StatisticsFocusRoute
	{
		private float attentionValue;
		private float concentrationValue;
		private float averageRactionTimeValue;

		private float acumConcentration = 0;
		private float acumAverageTime = 0;

		private float minTime = 0;		
		private float maxTime = 3.0f;
		
		private int totalTrains = 0;

		private List<string> activities;
		public List<string> Activities 
		{
			get { return activities; }
		}

		private Statistic trainingStatistics;


		public StatisticsFocusRoute ()
		{
			this.activities = new List<string>();

			int _idUser = GameObject.FindGameObjectWithTag ("StaticUser").GetComponent<StaticUserController> ().User.Id;
			int _idTraining = GameObject.FindGameObjectWithTag ("StaticUser").GetComponent<StaticUserController> ().Training.Id;
			
			this.trainingStatistics = new Statistic(_idUser, _idTraining);
			this.activities = this.trainingStatistics.GetTrainingActivities(_idTraining);
		}

		public float GetAverageReactionTime()
		{
			return this.averageRactionTimeValue;
		}

		public float GetAttention()
		{
			return this.attentionValue;
		}
		
		public float GetConcentration()
		{
			return this.concentrationValue;
		}

		public void calculateResults(int success, int total)
		{
			float _fa = acumConcentration/totalTrains;
			string _difficulty = GameObject.FindGameObjectWithTag ("StaticUser").GetComponent<StaticUserController> ().Training.Difficulty;

			this.concentrationValue = (Mathf.Abs(_fa - this.maxTime))/(Mathf.Abs(this.minTime - this.maxTime)) * 100;
			this.averageRactionTimeValue = acumAverageTime/totalTrains;
			this.attentionValue = (success * 100) / total;

			this.trainingStatistics.SetValues(success, total - success, _difficulty);
		}

		public void SetDataPerTrain (ActivityFocusRoute currentActivity)
		{
			float _localFocusedAttetion = 0f;
			float _averageTimePerTrain = 0f;
			float _totalActibityPerTrain = currentActivity.TotalActivity;

			totalTrains ++;
			if(currentActivity.IsTrainSucceeded)
			{
				for(int i = 0; i < _totalActibityPerTrain; i++)
				{
					float _timing = currentActivity.TimeList[i];
					int _tCount = currentActivity.TrainCountList[i];
					int _bCount = currentActivity.ButtonCountList[i];
					
					if(_timing < maxTime)
						_localFocusedAttetion += _timing / _tCount;
					
					if(_timing < maxTime && _bCount == 1)
						_averageTimePerTrain += _timing;
					else if(_timing < maxTime && _bCount > 1)
						_averageTimePerTrain += _timing + (_bCount/_timing);	
				}
			}
			else
			{
				_localFocusedAttetion += 2 * (_totalActibityPerTrain + maxTime);
				_averageTimePerTrain += maxTime * (_totalActibityPerTrain + maxTime);
			}
			
			_localFocusedAttetion = _localFocusedAttetion / _totalActibityPerTrain;
			_averageTimePerTrain = _averageTimePerTrain/_totalActibityPerTrain;
			
			acumAverageTime += _averageTimePerTrain;
			acumConcentration += _localFocusedAttetion;
		}

		public void saveStatistics()
		{
			string _timeDescription = "";

			this.trainingStatistics.SaveStatistics();
			this.trainingStatistics.SaveLevel(this.attentionValue, this.activities[0]);
			this.trainingStatistics.SaveLevel(this.concentrationValue, this.activities[1]);

			if(this.averageRactionTimeValue <= this.minTime + 0.1f)
				_timeDescription = "Óptimo tiempo de reacción para cada tren.";
			else if(this.averageRactionTimeValue > this.minTime + 0.1f && this.averageRactionTimeValue <= this.minTime + 1.3f)
				_timeDescription = "Buena reacción con respecto a cada tren, mejorable.";
			else
				_timeDescription = "Se recomienda seguir practicando.";

			this.trainingStatistics.SaveReaction(this.averageRactionTimeValue, _timeDescription);

		}
	}
}

