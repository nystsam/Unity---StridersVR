using UnityEngine;
using System;

namespace StridersVR.Buttons
{
	public abstract class StatisticsPanelButton : VirtualButtonController
	{
		[SerializeField]private MeshRenderer shape;
		[SerializeField]private TextMesh text;

		protected Material originalMat;
		protected Material pressedMat;

		protected Color originalColor;
		protected Color pressedColor;

		protected bool isSelected;
		public bool IsSelected 
		{
			get { return IsSelected; }
		}

		protected abstract void PanelAction();

		protected override void ButtonAction ()
		{
			this.PanelAction();
		}

		protected void SetValues()
		{
			this.originalMat = Resources.Load ("Images/Materials/StatsPanelBg", typeof(Material)) as Material;
			this.pressedMat = Resources.Load ("Images/Materials/StatsPanelPressedBg", typeof(Material)) as Material;
			
			Color.TryParseHexString("18CAE6FF", out this.originalColor);
			Color.TryParseHexString("012C29FF", out this.pressedColor);
		}

		public void ButtonActivation(bool isActive)
		{
			this.isSelected = isActive;

			if(isActive)
			{
				this.shape.material = this.pressedMat;
				this.text.color = this.pressedColor;
			}
			else
			{
				this.transform.localPosition = new Vector3(0,0,0);
				this.shape.material = this.originalMat;
				this.text.color = this.originalColor;
			}

		}
	}
}

