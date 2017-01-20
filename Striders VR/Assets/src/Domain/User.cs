using System;

namespace StridersVR.Domain
{
	public class User
	{
		private int id;
		public int Id 
		{
			get { return id; }
			set { id = value; }
		}

		private string name;
		public string Name 
		{
			get { return name; }
			set { name = value; }
		}

		public User (int id, string name)
		{
			this.id = id;
			this.name = name;
		}
	}
}

