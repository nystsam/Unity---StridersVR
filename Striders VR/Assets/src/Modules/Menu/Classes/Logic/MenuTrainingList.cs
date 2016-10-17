using System;
using System.Collections.Generic;
using StridersVR.Modules.Menu.Data;
using StridersVR.Domain;

namespace StridersVR.Modules.Menu.Logic
{
	public class MenuTrainingList
	{
		private List<Training> trainingList;

		private int currentPosition;
		private int maxListPosition;

		public MenuTrainingList ()
		{
			DbTraining _training;

			_training = new DbTraining();
			this.trainingList = _training.getTrainingList ();

			this.maxListPosition = this.trainingList.Count - 1;
			this.currentPosition = 0;
		}

		public Training getCurrentTraining()
		{
			return this.trainingList[this.currentPosition];
		}

		public Training getNextTraining()
		{
			if (this.currentPosition < this.maxListPosition) 
			{
				this.currentPosition++;
			}
			else
			{
				this.currentPosition = 0;
			}

			return this.trainingList [this.currentPosition];
		}

		public Training getPreviousTraining()
		{
			if (this.currentPosition > 0) 
			{
				this.currentPosition--;
			}
			else
			{
				this.currentPosition = this.maxListPosition;
			}
			
			return this.trainingList [this.currentPosition];			
		}
	}
}

