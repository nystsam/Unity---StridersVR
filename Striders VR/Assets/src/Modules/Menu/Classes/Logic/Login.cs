using System;
using StridersVR.Domain;
using StridersVR.Modules.Menu.Data;

namespace StridersVR.Modules.Menu.Logic
{
	public class Login
	{
		private DbUserLogin dbUser;

		public Login ()
		{
			this.dbUser = new DbUserLogin();
		}


		public Boolean LoginRequest(string username)
		{
			User _newUser;
			_newUser = this.dbUser.LoginRequest(username);
			if(_newUser != null)
			{
				SessionController.Current.SetUser(_newUser);
				return true;
			}
			return false;
		}
	}
}

