using UnityEngine;
using System;
using StridersVR.Domain;

namespace StridersVR.Buttons
{
	public class ButtonStatisticsTraining : MenuButton
	{
		[SerializeField] private TextMesh text;

		private Vector3 targetPosition;

		private Training currentTraining;


		public void SetTraining(Training newTraining)
		{
			this.currentTraining = newTraining;
			this.text.text = this.currentTraining.Name;
		}

		public void SetContainerTransition(GameObject current, GameObject target)
		{
			this.CurrentMenu = current;
			this.TargetMenu = target;
		}

		protected override void MenuButtonAction ()
		{
			this.CurrentMenu.SetActive(false);
			this.CurrentMenu.transform.localPosition = this.targetPosition;
			this.TargetMenu.transform.localPosition = Vector3.zero;
			this.TargetMenu.SetActive(true);
			MenuStatisticsController.Current.SelectTraining(this.currentTraining);
		}

		void Start()
		{
			this.targetPosition = this.TargetMenu.transform.localPosition;
		}
	}
}

