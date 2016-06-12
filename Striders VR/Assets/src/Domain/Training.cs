using System;

namespace StridersVR.Domain
{
	public class Training
	{
		private string name = "";
		private string difficulty = "";

		public Training (String name)
		{
			this.name = name;
		}


		#region Properties
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
		#endregion
	}
}

