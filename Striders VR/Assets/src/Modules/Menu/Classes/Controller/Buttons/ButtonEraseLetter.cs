using UnityEngine;
using System;

namespace StridersVR.Buttons
{
	public class ButtonEraseLetter : KeyboardButton
	{
		protected override void KeyPressed()
		{
			this.virtualKeyboard.eraseLetter();
		}
	}
}

