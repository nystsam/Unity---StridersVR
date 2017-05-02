using System;
using UnityEngine;
using StridersVR.Domain;

namespace StridersVR.Modules.Menu.Data
{
	public class DbUserLogin : DbAccess
	{
		public DbUserLogin ()
		{
		}


		public User LoginRequest(string username)
		{
			User _newUser;
			this.openConnection ();
			_newUser = this.login(username);
			if(_newUser == null)
			{
				this.createUser(username);
				_newUser = this.login(username);
			}
			this.closeConnection();
			return _newUser;
		}

		private User login(string username)
		{

			using (this.dbCommand = this.dbConnection.CreateCommand()) 
			{
				this.sqlQuery = String.Format("SELECT * FROM User WHERE us_name=\'{0}\';",username.ToUpper());
				this.dbCommand.CommandText = this.sqlQuery;
				using(this.dbCmdReader = this.dbCommand.ExecuteReader())
				{
					if(this.dbCmdReader.Read())
					{
						int _userId = this.dbCmdReader.GetInt32(0);
						string _userName = this.dbCmdReader.GetString(1);
						User _currentUser = new User(_userId, _userName);
						return _currentUser;
					}
				}
			}
			return null;
		}

		private void createUser(string username)
		{
			try
			{
				using (this.dbCommand = this.dbConnection.CreateCommand()) 
				{
					this.sqlQuery = String.Format("INSERT INTO User(us_name) VALUES (\'{0}\');",username.ToUpper());
					this.dbCommand.CommandText = this.sqlQuery;
					this.dbCommand.ExecuteScalar();
				}
			}
			catch (Exception e)
			{
				Debug.LogException(e);
			}
		}
	}
}

