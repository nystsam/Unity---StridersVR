using System;
using System.Collections.Generic;
using StridersVR.Domain;
using UnityEngine;

namespace StridersVR.Modules.TrainOfThought.Data
{
	public class DbFocusRoute : DbAccess
	{

		public DbFocusRoute()
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
	}
}

