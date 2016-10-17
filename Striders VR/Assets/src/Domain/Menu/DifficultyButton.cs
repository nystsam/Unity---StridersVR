using System;
using UnityEngine;
using UnityEngine.UI;

namespace StridersVR.Domain.Menu
{
	public class DifficultyButton
	{
		private string difficulty;

		private GameObject button;

		private Color unpressedButtonColor;

		public DifficultyButton(GameObject button, string difficulty)
		{
			this.button = button;
			this.difficulty = difficulty;

			this.unpressedButtonColor = this.button.GetComponent<DifficultyButtonController> ().imageButton.GetComponent<Image>().color;
		}


		public void toogleOn()
		{
			Color _newColor = this.unpressedButtonColor;

			_newColor.a = 1f;
			this.button.GetComponent<DifficultyButtonController> ().imageButton.GetComponent<Image> ().color = _newColor;
		}

		public void toogleOff()
		{
			this.button.GetComponent<DifficultyButtonController> ().imageButton.GetComponent<Image> ().color = this.unpressedButtonColor;
		}
	}
}

