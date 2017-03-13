using UnityEngine;
using System;

namespace StridersVR.Buttons
{
	public class ButtonSpaceBar : KeyboardButton
	{
		protected override void KeyPressed()
		{
			this.virtualKeyboard.typeLetter(' ');
		}
	}
}

