using UnityEngine;
using System;

namespace StridersVR.Buttons
{
	public class ButtonShift : KeyboardButton
	{
		private bool isActive = false;

		protected override void KeyPressed()
		{
			this.isActive = !this.isActive;
			this.virtualKeyboard.changeCase(this.isActive);
			this.virtualKeyboard.SetFirstLetterCase();
		}
	}
}

