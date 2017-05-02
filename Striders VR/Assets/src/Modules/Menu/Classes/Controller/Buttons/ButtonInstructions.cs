using UnityEngine;
using System;

namespace StridersVR.Buttons
{
	public class ButtonInstructions : MenuButton
	{
		private Vector3 targetPosition;

		protected override void MenuButtonAction ()
		{
			this.CurrentMenu.SetActive(false);
			this.CurrentMenu.transform.localPosition = this.targetPosition;
			this.TargetMenu.transform.localPosition = Vector3.zero;
			this.TargetMenu.SetActive(true);
			string _name = ButtonTrainingList.Current.CurrentTrain.Name;

			MenuInstructionsController.Current.SetTrainingName(_name);
			if(_name.Equals("Focus Route"))
			{
				MenuInstructionsController.Current.focusRouteInstructions();
			}
			else if(_name.Equals("Velocity Pack"))
			{
				MenuInstructionsController.Current.velocityPackInstructions();
			}
			else
			{
				MenuInstructionsController.Current.dotToDotInstructions();
			}
		}
		
		void Start()
		{
			this.targetPosition = this.TargetMenu.transform.localPosition;
			this.TargetMenu.SetActive(false);
		}
	}
}

