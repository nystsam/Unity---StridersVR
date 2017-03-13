using UnityEngine;
using System;

namespace StridersVR.Buttons
{
	public abstract class KeyboardButton : VirtualButtonController
	{
		[SerializeField] protected KeyboardController virtualKeyboard;

		private Material originalMat;
		private Material hoverMat;

		private Color originalColor = new Color(0.094f,0.792f,0.902f,1);
		private Color hoverColor = new Color(0.003f, 0.243f, 0.203f, 1);

		private MeshRenderer icon;
		private MeshRenderer background;
	
		private AudioSource buttonSound;

		public void EnableButton()
		{
			this.GetComponent<Rigidbody>().isKinematic = false;
			this.background.material = this.hoverMat;
			this.icon.material.color = this.hoverColor;
		}
		
		public void DisableButton()
		{
			this.GetComponent<Rigidbody>().isKinematic = true;
			this.background.material = this.originalMat;
			this.icon.material.color = this.originalColor;
			this.CancelForce();
		}

		public virtual void ChangeCase(bool newCase)
		{
			Debug.Log ("Fine");
		}

		protected override void ButtonAction()
		{
			if(this.buttonSound != null)
				this.buttonSound.Play();

			this.KeyPressed();
		}

		protected abstract void KeyPressed();

		#region Script
		void Awake()
		{
			this.GetComponent<Rigidbody>().isKinematic = true;
		}

		void Start()
		{
			this.triggerDistance = 0.15f;
			this.min = 0f;
			this.max = 0.2f;
			this.spring = 200;
			this.ResetVirtualButton();
			this.icon = this.transform.GetChild(1).GetComponent<MeshRenderer>();
			this.background = this.transform.GetChild(0).GetComponent<MeshRenderer>();

			this.originalMat = Resources.Load ("Materials/MatUIMenuColor2", typeof(Material)) as Material;
			this.hoverMat = Resources.Load ("Materials/Menu/MatBg5", typeof(Material)) as Material;
			this.buttonSound = this.GetComponent<AudioSource>();
		}
		#endregion
	}
}

