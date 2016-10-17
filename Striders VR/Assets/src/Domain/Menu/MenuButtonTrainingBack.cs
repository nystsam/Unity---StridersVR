using System;
using UnityEngine;

namespace StridersVR.Domain.Menu
{
	public class MenuButtonTrainingBack : MenuButton
	{
		public MenuButtonTrainingBack ()
		{
			this.menuContainer = GameObject.FindGameObjectWithTag("GameController");

			this.animName = "AnimMainMenu";
			this.animVariable = "IsTraining";
		}

		public override void buttonAction()
		{
			this.menuContainer.GetComponent<MenuContainerController> ().changeMenu (this.animName, this.animVariable, false);	
		}
	}
}

