using System;
using UnityEngine;

namespace StridersVR.Domain.Menu
{
	public class MenuButtonDifficultyBack : MenuButton
	{	
		public MenuButtonDifficultyBack ()
		{
			this.menuContainer = GameObject.FindGameObjectWithTag("GameController");
			
			this.animName = "AnimTrainingMenu";
			this.animVariable = "IsDifficulty";
		}
		
		public override void buttonAction()
		{
			this.menuContainer.GetComponent<MenuContainerController> ().changeMenu (this.animName, this.animVariable, false);
		}
	}
}

