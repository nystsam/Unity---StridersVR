using System;
using UnityEngine;

namespace StridersVR.Domain.Menu
{
	public class MenuButtonDifficulty : MenuButton
	{	
		public MenuButtonDifficulty ()
		{
			this.menuContainer = GameObject.FindGameObjectWithTag("GameController");

			this.animName = "AnimDifficultyMenu";
			this.animVariable = "IsDifficulty";
		}

		public override void buttonAction()
		{
			this.menuContainer.GetComponent<MenuContainerController> ().changeMenu (this.animName, this.animVariable, true);
		}
	}
}

