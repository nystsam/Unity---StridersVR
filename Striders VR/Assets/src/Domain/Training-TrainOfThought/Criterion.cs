using System;
using System.Collections.Generic;
using StridersVR.Modules.Menu.Data;

namespace StridersVR.Domain
{
	public class Criterion
	{
		private bool isLevel;
		public bool IsLevel 
		{
			get { return isLevel; } 
			set { isLevel = value; }
		}

		private bool isScore;
		public bool IsScore 
		{
			get { return isScore; }
			set { isScore = value; }
		}

		private bool isAttempt;
		public bool IsAttempt 
		{
			get { return isAttempt; }
			set { isAttempt = value; }
		}

		private float criterionValue;
		public float CriterionValue 
		{
			get { return criterionValue; }
		}

		private string description;
		public string Description 
		{
			get { return description; }
			set { description = value; }
		}

		public Criterion (float value)
		{
			this.criterionValue = value;

			this.isLevel = false;
			this.isAttempt = false;
			this.isScore = false;
		}

	}
}

