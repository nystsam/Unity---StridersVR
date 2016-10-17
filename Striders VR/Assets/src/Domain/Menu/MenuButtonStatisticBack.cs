using System;
using UnityEngine;

namespace StridersVR.Domain.Menu
{
	public class MenuButtonStatisticBack : MenuButton
	{
		public MenuButtonStatisticBack ()
		{
			this.menuContainer = GameObject.FindGameObjectWithTag("GameController");
			
			this.animName = "AnimMainMenu";
			this.animVariable = "IsStatistics";
		}

		public override void buttonAction()
		{
			this.menuContainer.GetComponent<MenuContainerController> ().changeMenu (this.animName, this.animVariable, false);	
		}
	}
}

