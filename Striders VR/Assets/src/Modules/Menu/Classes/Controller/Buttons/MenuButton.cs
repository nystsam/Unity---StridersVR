using UnityEngine;
using System;

namespace StridersVR.Buttons
{
	public abstract class MenuButton : VirtualButtonController
	{
		[SerializeField] protected GameObject CurrentMenu;
		[SerializeField] protected GameObject TargetMenu;

		protected override void ButtonAction()
		{
			this.MenuButtonAction();
		}

		protected abstract void MenuButtonAction();
	}
}

