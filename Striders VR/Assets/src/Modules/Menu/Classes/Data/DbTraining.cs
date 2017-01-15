using System;
using System.Collections.Generic;
using StridersVR.Domain;
using UnityEngine;

namespace StridersVR.Modules.Menu.Data
{
	public class DbTraining : DbAccess
	{

		public DbTraining ()
		{
		}

		public List<Training> getTrainingList()
		{
			List<Training> _trainingList = new List<Training>();

			this.openConnection ();
			using (this.dbCommand = this.dbConnection.CreateCommand()) 
			{
				this.sqlQuery = "SELECT * FROM Training ORDER BY tr_name";
				this.dbCommand.CommandText = this.sqlQuery;
				using(this.dbCmdReader = this.dbCommand.ExecuteReader())
				{
					while(this.dbCmdReader.Read())
					{
						int id = this.dbCmdReader.GetInt32(0);
						string name = this.dbCmdReader.GetString(1);
						Training _newTraining = new Training(id, name);
						_newTraining.Image = this.dbCmdReader.GetString(3);
						_trainingList.Add(_newTraining);
					}
					return _trainingList;
				}
			}
		}

	}
}

