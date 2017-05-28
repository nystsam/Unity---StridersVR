using UnityEngine;
using System.Collections.Generic;
using StridersVR.Modules.Menu.Data;

namespace StridersVR.Domain.SpeedPack
{
	public class StatisticsVelocityPack
	{
		private float agilityValue = 0;
		private float perceptionValue = 0;
		private float averageTimeValue = 0;

		private int errors = 0;
		private int totalPackage = 0;
		private int score = 0;

		private float minTimeComplete = 2;		
		private float maxTimeComplete = 15;

		private List<string> activities;
		public List<string> Activities 
		{
			get { return activities; }
		}

		private Statistic trainingStatistics;

		public float GetAgility()
		{
			return this.agilityValue;
		}
		
		public float GetPerception()
		{
			return this.perceptionValue;
		}
		
		public float GetAverageTime()
		{
			return this.averageTimeValue;
		}

		public int GetScore()
		{
			return this.score;
		}

		public StatisticsVelocityPack ()
		{
			this.activities = new List<string>();
			
			int _idUser = GameObject.FindGameObjectWithTag ("StaticUser").GetComponent<StaticUserController> ().User.Id;
			int _idTraining = GameObject.FindGameObjectWithTag ("StaticUser").GetComponent<StaticUserController> ().Training.Id;
			
			this.trainingStatistics = new Statistic(_idUser, _idTraining);
			this.activities.Add("Agilidad mental");
			this.activities.Add("Percepción");
		}

		public void calculateResults(int success, int total)
		{
			string _difficulty = GameObject.FindGameObjectWithTag ("StaticUser").GetComponent<StaticUserController> ().Training.Difficulty;

			float _customValue = 0;
			if(this.errors == 0)
			{
				if((this.averageTimeValue/this.totalPackage) < 5)
					_customValue = Random.Range(84,96);
				else if((this.averageTimeValue/this.totalPackage) > 10)
					_customValue = Random.Range(35,43);
				else
					_customValue = Random.Range(55,71);
			}
			else if(this.errors > 0 && this.errors <= 2)
			{
				if((this.averageTimeValue/this.totalPackage) < (this.maxTimeComplete + this.minTimeComplete)/2)
					_customValue = Random.Range(65,76);
				else
					_customValue = Random.Range(51,66);
			}
			else if(this.errors > 2 && this.errors <= 4)
			{
				_customValue = Random.Range(45,61);
			}
			else
			{
				_customValue = Random.Range(25,36);
			}
			
			this.perceptionValue = _customValue;

			if(this.score != 0)
			{
				this.averageTimeValue = this.averageTimeValue/this.totalPackage;
				this.agilityValue = (Mathf.Abs(this.averageTimeValue - maxTimeComplete))/(Mathf.Abs(minTimeComplete - maxTimeComplete)) * 100;
			}

			this.trainingStatistics.SetValues(success, total - success, _difficulty);
		}

		public void SetDataPerModel(ActivityVelocityPack currentActivity)
		{
			this.averageTimeValue += currentActivity.TimeComplete;
			this.totalPackage ++;
			this.score += currentActivity.Score;
			if(currentActivity.IsCorrect){
				this.averageTimeValue += currentActivity.TimeComplete;
			}
			else{
				this.errors ++;
				this.averageTimeValue += this.minTimeComplete;
			}
		}

		public void saveStatistics()
		{
			string _timeDescription = "";
			
			this.trainingStatistics.SaveStatistics();
			this.trainingStatistics.SaveLevel(this.agilityValue, this.activities[0]);
			this.trainingStatistics.SaveLevel(this.perceptionValue, this.activities[1]);
			this.trainingStatistics.SaveScore(this.score, "Puntuación");
			
			if(this.averageTimeValue <= this.minTimeComplete + 0.1f)
				_timeDescription = "Óptimo tiempo de modelación.";
			else if(this.averageTimeValue > this.minTimeComplete + 0.1f && this.averageTimeValue <= this.minTimeComplete + 1.3f)
				_timeDescription = "Buena reacción con respecto a cada modelo, mejorable.";
			else
				_timeDescription = "Se recomienda seguir practicando.";
			
			this.trainingStatistics.SaveReaction(this.averageTimeValue, _timeDescription);
			
		}

	}
}

