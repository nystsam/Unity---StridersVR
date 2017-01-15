using System;

namespace StridersVR.Domain
{
	public class Training
	{
		private int id;

		private string name = "";
		private string difficulty = "";
		private string tutorial;
		private string image;

		public Training (int id, String name)
		{
			this.id = id;
			this.name = name;
		}


		#region Properties
		public int Id
		{
			get { return this.id; }
		}

		public string Name
		{
			get { return this.name; }
			set { this.name = value; }
		}

		public string Difficulty
		{
			get { return this.difficulty; }
			set { this.difficulty = value; }
		}

		public string Tutorial
		{
			get { return this.tutorial; }
			set { this.tutorial = value; }
		}

		public string Image
		{
			get { return this.image; }
			set { this.image = value; }
		}
		#endregion
	}
}

