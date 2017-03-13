using System;
using System.Collections.Generic;
using StridersVR.Domain;

namespace StridersVR.Modules.Menu.Data
{
	public class DbCriterion : DbAccess
	{
		public DbCriterion ()
		{
		}

		public List<Criterion> GetCriterions(int statisticId)
		{
			List<Criterion> _list = new List<Criterion>();;

			this.openConnection ();
			using (this.dbCommand = this.dbConnection.CreateCommand()) 
			{
				this.sqlQuery = "SELECT cr_level, cr_reaction, cr_attempts, cr_score, cr_description FROM Criterion " +
						"WHERE fk_statistic="+statisticId.ToString();
				this.dbCommand.CommandText = this.sqlQuery;
				using(this.dbCmdReader = this.dbCommand.ExecuteReader())
				{
					while(this.dbCmdReader.Read())
					{
						Criterion _newCriterion = new Criterion(2f);

						float _level = -1;
						float _reaction = -1;
						float _attempts = -1;
						Int32 _score = -1;

						if(!this.dbCmdReader.IsDBNull(0))
							_level = this.dbCmdReader.GetFloat(0);
						if(!this.dbCmdReader.IsDBNull(1))
							_reaction = this.dbCmdReader.GetFloat(1);
						if(!this.dbCmdReader.IsDBNull(2))
							_attempts = this.dbCmdReader.GetFloat(2);
						if(!this.dbCmdReader.IsDBNull(3))
							_score = this.dbCmdReader.GetInt32(3);

						if(_level != -1)
						{
							_newCriterion = new Criterion(_level);
							_newCriterion.IsLevel = true;
						}
						else if(_attempts != -1)
						{
							_newCriterion = new Criterion(_attempts);
							_newCriterion.IsAttempt = true;
						}
						else if(_score != -1)
						{
							_newCriterion = new Criterion((float)_score);
							_newCriterion.IsScore = true;
						}
						else
						{
							_newCriterion = new Criterion(_reaction);
						}

						_newCriterion.Description = this.dbCmdReader.GetString(4);

						_list.Add(_newCriterion);

					}
					this.closeConnection();
					return _list;
				}
			}
		}
	}
}

