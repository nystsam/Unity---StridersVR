using System;
using System.Collections.Generic;
using StridersVR.Modules.Menu.Data;

namespace StridersVR.Domain
{
	public class Statistic
	{
		private int id;
		public int Id { set { id = value; } get { return id; } }

		private int userId;
		public int UserId { get { return userId; } }

		private int trainingId;
		public int TrainingId { get { return trainingId; } }

		private int hits;
		public int Hits { get { return hits; } }

		private int errors;
		public int Errors { get { return errors; } }
		
		private string difficulty;
		public string Difficulty { get { return difficulty; } }

		private string currentDate;

		DbStatistics dbStatistics;


		public Statistic (int userId, int trainingId)
		{
			this.userId = userId;
			this.trainingId = trainingId;
			this.dbStatistics = new DbStatistics();
		}

		public void SetValues(int hits, int errors, string difficulty)
		{
			this.hits = hits;
			this.errors = errors;
			this.difficulty = difficulty;
		}

		public List<string> GetTrainingActivities(int trainingId)
		{
			return this.dbStatistics.getActivityList(trainingId);
		}

		public void SaveStatistics()
		{
			this.id = this.dbStatistics.insertStatistics(this);
		}

		public void SaveLevel(float level, string description)
		{
			if(this.id != 0)
				this.dbStatistics.insertCriterionLevel(this.id, level, description);
		}

		public void SaveReaction(float reaction, string description)
		{
			if(this.id != 0)
				this.dbStatistics.insertCriterionReaction(this.id, reaction, description);
		}

		public void SaveAttempts(float attempts, string description)
		{
			if(this.id != 0)
				this.dbStatistics.insertCriterionAttempts(this.id, attempts, description);
		}

		public void SaveScore(float score, string description)
		{
			if(this.id != 0)
				this.dbStatistics.insertCriterionScore(this.id, score, description);
		}
	}
}

