using System;
using UnityEngine;
using UnityEngine.UI;

namespace StridersVR.Domain.Menu
{
	public class DifficultySelection
	{
		private MeshRenderer image;

		private string difficulty;

		private Color unpressedButtonColor;

		private Material original;
		private Material selected;

		public DifficultySelection(MeshRenderer image, string difficulty)
		{
			this.image = image;
			this.difficulty = difficulty;

			this.selected = Resources.Load("Images/Materials/CircleSelected", typeof(Material)) as Material;
		}

		public void SetMaterial(Material original)
		{
			this.original = original;
		}

		public void toogleOn()
		{
			this.image.material = selected;
		}

		public void toogleOff()
		{
			this.image.material = original;
		}


		#region Properties
		public string Difficulty
		{
			get{ return this.difficulty; }
		}
		#endregion
	}
}

