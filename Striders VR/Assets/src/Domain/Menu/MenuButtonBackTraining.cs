using System;
using UnityEngine;

namespace StridersVR.Domain.Menu
{
	public class MenuButtonBackTraining : MenuButton
	{
		public MenuButtonBackTraining ()
		{
			this.menuContainer = GameObject.FindGameObjectWithTag("GameController");
		}

		public override void buttonAction()
		{
			this.menuContainer.GetComponent<MenuContainerController> ().changeMenu ("", "");
		}
	}
}

