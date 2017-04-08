using UnityEngine;
using System;
using StridersVR.Modules.Menu.Logic;

namespace StridersVR.Buttons
{
	public class ButtonEnter : KeyboardButton
	{
		private Login loginManager;

		protected override void KeyPressed()
		{
			this.loginManager = new Login();
			if(!this.virtualKeyboard.GetText().Equals(""))
			{
				if(this.loginManager.LoginRequest(this.virtualKeyboard.GetText()))
				{
					this.virtualKeyboard.ClearText();
				}

			}
		}
	}
}

