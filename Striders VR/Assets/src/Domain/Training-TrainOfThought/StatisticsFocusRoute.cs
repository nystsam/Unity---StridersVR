using UnityEngine;
using System.Collections.Generic;
using StridersVR.Modules.TrainOfThought.Data;

namespace StridersVR.Domain.TrainOfThought
{
	public class StatisticsFocusRoute
	{
		private float focusedAttention = 0;
		private float averageReactionTime = 0;

		private float minTime = 0;		
		private float maxTime = 3.0f;
		
		private int totalTrains = 0;

		private List<string> activities;
		public List<string> Activities 
		{
			get { return activities; }
		}

		public StatisticsFocusRoute ()
		{
			this.activities = new List<string>();
			this.trainActivities();
		}

		public float GetAverageReactionTime()
		{
			return averageReactionTime/totalTrains;
		}

		public float GetAttention(int success, int total)
		{
			return (success * 100) / total;
		}
		
		public float GetConcentration()
		{
			float _fa = focusedAttention/totalTrains;
			
			return (Mathf.Abs(_fa - this.maxTime))/(Mathf.Abs(this.minTime - this.maxTime)) * 100;
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
				_localFocusedAttetion += maxTime * _totalActibityPerTrain;
				_averageTimePerTrain += maxTime * _totalActibityPerTrain;
			}
			
			_localFocusedAttetion = _localFocusedAttetion / _totalActibityPerTrain;
			_averageTimePerTrain = _averageTimePerTrain/_totalActibityPerTrain;
			
			averageReactionTime += _averageTimePerTrain;
			focusedAttention += _localFocusedAttetion;
		}

		private void trainActivities()
		{
			DbFocusRoute _dbFocusRoute = new DbFocusRoute();
			int _id = GameObject.FindGameObjectWithTag ("StaticUser").GetComponent<StaticUserController> ().Training.Id;

			this.activities = _dbFocusRoute.getActivityList(_id);
		}
		
	}
}

