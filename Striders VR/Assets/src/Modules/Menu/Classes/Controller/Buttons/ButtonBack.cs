using UnityEngine;
using System;

namespace StridersVR.Buttons
{
	public class ButtonBack : MenuButton
	{
		private Vector3 startingPosition;

		protected override void MenuButtonAction ()
		{
			this.CurrentMenu.SetActive(false);
			this.CurrentMenu.transform.localPosition = this.startingPosition;
			this.TargetMenu.transform.localPosition = Vector3.zero;
			this.TargetMenu.SetActive(true);
			
		}

		void Start()
		{
			this.startingPosition = this.CurrentMenu.transform.localPosition;
		}
	}
}

