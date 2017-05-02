using UnityEngine;
using System;

namespace StridersVR.Buttons
{
	public class ButtonTraining : MenuButton
	{
		private Vector3 targetPosition;

		protected override void MenuButtonAction ()
		{
			this.CurrentMenu.SetActive(false);
			this.CurrentMenu.transform.localPosition = this.targetPosition;
			this.TargetMenu.transform.localPosition = Vector3.zero;
			this.TargetMenu.SetActive(true);
		}

		void Start()
		{
			this.targetPosition = this.TargetMenu.transform.localPosition;
			this.TargetMenu.SetActive(false);
		}
	}
}

