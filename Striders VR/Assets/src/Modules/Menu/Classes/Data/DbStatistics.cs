using System;
using System.Collections.Generic;
using StridersVR.Domain;

namespace StridersVR.Modules.Menu.Data
{
	public class DbStatistics : DbAccess
	{
		public DbStatistics ()
		{
		}

		public List<string> getActivityList(int trainingId)
		{
			List<string> _activityList = new List<string>();
			
			this.openConnection ();
			using (this.dbCommand = this.dbConnection.CreateCommand()) 
			{
				this.sqlQuery = "SELECT ac_name FROM Activity WHERE fk_training="+trainingId.ToString()+" ORDER BY ac_name";
				this.dbCommand.CommandText = this.sqlQuery;
				using(this.dbCmdReader = this.dbCommand.ExecuteReader())
				{
					while(this.dbCmdReader.Read())
					{
						string name = this.dbCmdReader.GetString(0);
						_activityList.Add(name);
					}
					this.closeConnection();
					return _activityList;
				}
			}
		}

		#region Statistics
		public int insertStatistics(Statistic currentStats)
		{
			Int64 _newStatisticId;

			this.openConnection ();
			using (this.dbCommand = this.dbConnection.CreateCommand()) 
			{
				this.sqlQuery = "INSERT INTO Statistic(st_date, st_difficulty, st_correct, st_incorrect, fk_training, fk_user) " +
					"VALUES (datetime('now', 'localtime'),'"+ currentStats.Difficulty +"', "+ currentStats.Hits +", "+ currentStats.Errors +", " + 
						currentStats.TrainingId +", "+ currentStats.UserId +");";
				this.dbCommand.CommandText = this.sqlQuery;
				this.dbCommand.ExecuteNonQuery();

				this.sqlQuery = "SELECT last_insert_rowid()";
				this.dbCommand.CommandText = this.sqlQuery;

				_newStatisticId = (Int64) this.dbCommand.ExecuteScalar();
			}
			this.closeConnection();

			return Convert.ToInt32(_newStatisticId);
		}
		#endregion

		#region Criterion
		public void insertCriterionLevel(int statsId, float level, string description)
		{
			this.openConnection ();
			using (this.dbCommand = this.dbConnection.CreateCommand()) 
			{
				this.sqlQuery = "INSERT INTO Criterion(cr_level, cr_reaction, cr_attempts, cr_score, cr_description, fk_statistic)" +
						"VALUES ("+ level +", null, null, null, '"+ description +"', "+ statsId +");";
				
				this.dbCommand.CommandText = this.sqlQuery;
				this.dbCommand.ExecuteNonQuery();
			}
			this.closeConnection();
		}
		
		public void insertCriterionReaction(int statsId, float reaction, string description)
		{
			this.openConnection ();
			using (this.dbCommand = this.dbConnection.CreateCommand()) 
			{
				this.sqlQuery = "INSERT INTO Criterion(cr_level, cr_reaction, cr_attempts, cr_score, cr_description, fk_statistic)" +
						"VALUES (null, "+ reaction +", null, null, '"+ description +"', "+ statsId +");";

				this.dbCommand.CommandText = this.sqlQuery;
				this.dbCommand.ExecuteNonQuery();
			}
			this.closeConnection();
		}

		public void insertCriterionAttempts(int statsId, float attempts, string description)
		{
			this.openConnection ();
			using (this.dbCommand = this.dbConnection.CreateCommand()) 
			{
				this.sqlQuery = "INSERT INTO Criterion(cr_level, cr_reaction, cr_attempts, cr_score, cr_description, fk_statistic)" +
						"VALUES (null, null, "+ attempts +", null, '"+ description +"', "+ statsId +");";
				
				this.dbCommand.CommandText = this.sqlQuery;
				this.dbCommand.ExecuteNonQuery();
			}
			this.closeConnection();
		}

		public void insertCriterionScore(int statsId, float score, string description)
		{
			this.openConnection ();
			using (this.dbCommand = this.dbConnection.CreateCommand()) 
			{
				this.sqlQuery = "INSERT INTO Criterion(cr_level, cr_reaction, cr_attempts, cr_score, cr_description, fk_statistic)" +
					"VALUES (null, null, null, "+ score +", '"+ description +"', "+ statsId +");";
				
				this.dbCommand.CommandText = this.sqlQuery;
				this.dbCommand.ExecuteNonQuery();
			}
			this.closeConnection();
		}
		#endregion
	}
}

