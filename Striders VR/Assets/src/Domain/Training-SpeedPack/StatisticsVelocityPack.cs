using UnityEngine;
using System.Collections.Generic;
using StridersVR.Modules.Menu.Data;

namespace StridersVR.Domain.SpeedPack
{
	public class StatisticsVelocityPack
	{

		private List<string> activities;
		public List<string> Activities 
		{
			get { return activities; }
		}

		private Statistic trainingStatistics;

		public StatisticsVelocityPack ()
		{
			this.activities = new List<string>();
			
			int _idUser = GameObject.FindGameObjectWithTag ("StaticUser").GetComponent<StaticUserController> ().User.Id;
			int _idTraining = GameObject.FindGameObjectWithTag ("StaticUser").GetComponent<StaticUserController> ().Training.Id;
			
			this.trainingStatistics = new Statistic(_idUser, _idTraining);
			this.activities.Add("Agilidad mental");
			this.activities.Add("Percepción");
		}
	}
}

