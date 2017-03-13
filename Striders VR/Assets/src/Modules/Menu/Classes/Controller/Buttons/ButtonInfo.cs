using UnityEngine;
using System;
using StridersVR.Domain;

namespace StridersVR.Buttons
{
	public class ButtonInfo : VirtualButtonController
	{
		[SerializeField] private TextMesh textDate;
		[SerializeField] private TextMesh textDifficulty;
		[SerializeField] private MeshRenderer background;

		private Material originalMat;
		private Material hoverMat;

		private Statistic currentStatistic;

		public void EnableButton()
		{
			this.GetComponent<Rigidbody>().isKinematic = false;
			this.background.material = this.hoverMat;
		}

		public void DisableButton()
		{
			this.GetComponent<Rigidbody>().isKinematic = true;
			this.background.material = originalMat;
		}

		public void SetStatistic(Statistic newStatistic)
		{
			string _difficulty;
			this.currentStatistic = newStatistic;

			if(this.currentStatistic.Difficulty.Equals("Easy"))
				_difficulty = "Fácil";
			else if(this.currentStatistic.Difficulty.Equals("Medium"))
				_difficulty = "Normal";
			else
				_difficulty = "Avanzado";

			this.textDate.text = this.currentStatistic.CurrentDate + ", ";
			this.textDifficulty.text = "Dificultad: " + _difficulty;
		}

		protected override void ButtonAction ()
		{
			MenuStatisticsController.Current.ShowDetails(this.currentStatistic);
		}

		private void SetValues()
		{
			this.originalMat = Resources.Load ("Materials/Menu/MatBgTransparent", typeof(Material)) as Material;
			this.hoverMat = Resources.Load ("Materials/Menu/MatBg4", typeof(Material)) as Material;
		}

		#region Script
		void Start()
		{
			this.SetValues();
		}
		#endregion
	}
}

