using UnityEngine;
using System.Collections.Generic;
using StridersVR.Domain.Menu;

namespace StridersVR.ScriptableObjects.Menu
{
	public class ScriptableObjectMenuButtons : ScriptableObject
	{
		[SerializeField] private string trainingName;
		[SerializeField] private string trainingBackName;
		[SerializeField] private string statisticsName;
		[SerializeField] private string statisticsBackName;
		[SerializeField] private string difficultyBackName;

		private int trainingNameHash;
		private int trainingBackNameHash;
		private int statisticsNameHash;
		private int statisticsBackNameHash;
		private int difficiltyBackHash;

		private void convertToHash()
		{
			this.trainingNameHash = this.trainingName.GetHashCode ();
			this.trainingBackNameHash = this.trainingBackName.GetHashCode ();
			this.statisticsNameHash = this.statisticsName.GetHashCode ();
			this.statisticsBackNameHash = this.statisticsBackName.GetHashCode ();
			this.difficiltyBackHash = this.difficultyBackName.GetHashCode ();
		}

		public MenuButton getButton(string currentName)
		{
			int _currentNameHash = currentName.GetHashCode ();
			MenuButton _menuButton = null;

			this.convertToHash ();

			if (_currentNameHash == this.trainingNameHash)
				_menuButton = new MenuButtonTraining ();
			else if (_currentNameHash == this.trainingBackNameHash)
				_menuButton = new MenuButtonTrainingBack ();
			else if (_currentNameHash == this.statisticsNameHash)
				_menuButton = new MenuButtonStatistic ();
			else if (_currentNameHash == this.statisticsBackNameHash)
				_menuButton = new MenuButtonStatisticBack ();
			else if (_currentNameHash == this.difficiltyBackHash)
				_menuButton = new MenuButtonDifficultyBack ();

			return _menuButton;
		}


	}
}

