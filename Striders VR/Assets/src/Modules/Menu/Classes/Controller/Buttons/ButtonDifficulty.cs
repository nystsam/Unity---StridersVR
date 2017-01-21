using UnityEngine;
using System.Collections.Generic;
using StridersVR.Domain.Menu;

namespace StridersVR.Buttons
{
	public class ButtonDifficulty : VirtualButtonController
	{
		[SerializeField] private MeshRenderer image;
		[SerializeField] private string difficulty;

		private DifficultySelection selection;


		protected override void ButtonAction()
		{
			DifficultySelectionController.Current.selectDifficulty(this.selection);
		}

		#region Script
		void Start()
		{
			this.selection = new DifficultySelection(this.image, this.difficulty);
			this.selection.SetMaterial(this.image.material);
		}
		#endregion
	}
}

