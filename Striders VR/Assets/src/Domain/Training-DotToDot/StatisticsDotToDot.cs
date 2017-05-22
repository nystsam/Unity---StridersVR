using UnityEngine;
using System.Collections.Generic;
using StridersVR.Modules.Menu.Data;

namespace StridersVR.Domain.DotToDot
{
	public class StatisticsDotToDot
	{
		private float memoryValue;
		private float abstrationValue;
		private float averageModelationTimeValue;
		private float averageRevelations;

		private float acumMemory = 0;
		private float acumAbstraction = 0;
		private float acumAverageTime = 0;

		private float minTimeComplete = 10;		
		private float maxTimeComplete = 30;
		private float minTimeShowing = 2;
		private float maxTimeShowing = 10;

		private int totalModels = 0;
		private int totalRevelation = 0;



		private List<string> activities;
		public List<string> Activities 
		{
			get { return activities; }
		}

		private Statistic trainingStatistics;

		public StatisticsDotToDot ()
		{
			this.activities = new List<string>();
			
			int _idUser = GameObject.FindGameObjectWithTag ("StaticUser").GetComponent<StaticUserController> ().User.Id;
			int _idTraining = GameObject.FindGameObjectWithTag ("StaticUser").GetComponent<StaticUserController> ().Training.Id;
			
			this.trainingStatistics = new Statistic(_idUser, _idTraining);
			this.activities.Add("Memoria");
			this.activities.Add("Abstración");
		}

		public float GetAverageModelationTime()
		{
			return this.averageModelationTimeValue;
		}
		
		public float GetMemory()
		{
			return this.memoryValue;
		}
		
		public float GetAbstraction()
		{
			return this.abstrationValue;
		}

		public float GetRevelationsPerModel()
		{
			return this.averageRevelations;
		}

		public void calculateResults(int success, int total)
		{
			string _difficulty = GameObject.FindGameObjectWithTag ("StaticUser").GetComponent<StaticUserController> ().Training.Difficulty;

			this.acumMemory = this.acumMemory/this.totalModels;
			this.acumAbstraction = this.acumAbstraction/this.totalModels;

			if(this.acumMemory < this.minTimeShowing)
				this.acumMemory = this.minTimeShowing;
			else if(this.acumMemory > this.maxTimeShowing)
				this.acumMemory = this.maxTimeShowing;

			if(this.acumAbstraction < this.minTimeComplete)
				this.acumAbstraction = this.minTimeComplete;
			else if(this.acumAbstraction > this.maxTimeComplete)
				this.acumAbstraction = this.maxTimeComplete;

			this.averageRevelations = this.totalRevelation/this.totalModels;
			this.memoryValue = (Mathf.Abs(this.acumMemory - maxTimeShowing))/(Mathf.Abs(minTimeShowing - maxTimeShowing)) * 100;
			this.averageModelationTimeValue = acumAverageTime/totalModels;
			this.abstrationValue = (Mathf.Abs(this.acumAbstraction - maxTimeComplete))/(Mathf.Abs(minTimeComplete - maxTimeComplete)) * 100;
			this.trainingStatistics.SetValues(success, total - success, _difficulty);
		}

		public void SetDataPerModel(ActivityDotToDot currentActivity)
		{
			float _acumTimeShowing = 0;
			float _timeAlpha = 0;

			this.totalModels ++;
			if(currentActivity.IsCorrect)
			{
				foreach(float time in currentActivity.TimeRevelationList)
				{
					_acumTimeShowing += time;
				}

				this.acumMemory += _acumTimeShowing/currentActivity.TimeRevelationList.Count;
				this.acumAverageTime += currentActivity.TimeComplete;

				_timeAlpha = (currentActivity.TimeComplete/2)/10;
				this.acumAbstraction += (currentActivity.TimeComplete + (_timeAlpha/Mathf.Pow(_timeAlpha,currentActivity.Revelations)));
				this.totalRevelation += currentActivity.Revelations;
			}
			else
			{
				this.acumMemory += this.maxTimeShowing;
				this.acumAverageTime += this.maxTimeComplete;
				_timeAlpha = (maxTimeComplete/2)/10;
				this.acumAbstraction += (maxTimeComplete + (_timeAlpha/Mathf.Pow(_timeAlpha,2)));
			}
		}

		public void saveStatistics()
		{
			string _timeDescription = "";
			
			this.trainingStatistics.SaveStatistics();
			this.trainingStatistics.SaveLevel(this.memoryValue, this.activities[0]);
			this.trainingStatistics.SaveLevel(this.abstrationValue, this.activities[1]);
			this.trainingStatistics.SaveAttempts(this.averageRevelations, "Revelaciones por modelo");
			
			if(this.averageModelationTimeValue <= this.minTimeComplete + 0.1f)
				_timeDescription = "Óptimo tiempo de modelación.";
			else if(this.averageModelationTimeValue > this.minTimeComplete + 0.1f && this.averageModelationTimeValue <= this.minTimeComplete + 1.3f)
				_timeDescription = "Buena reacción con respecto a cada modelo, mejorable.";
			else
				_timeDescription = "Se recomienda seguir practicando.";
			
			this.trainingStatistics.SaveReaction(this.averageModelationTimeValue, _timeDescription);
			
		}

	}
}

