using UnityEngine;
using System;

namespace StridersVR.Buttons
{
	public class ButtonLetter : KeyboardButton
	{
		[SerializeField] private char letter;

		private TextMesh keyText;

		protected override void KeyPressed()
		{
			this.virtualKeyboard.typeLetter(this.letter);
		}

		public override void ChangeCase(bool newCase)
		{
			if(!newCase)
			{
				this.letter = Char.ToLower(this.letter);
			}
			else
			{
				this.letter = Char.ToUpper(this.letter);

			}
			this.keyText.text = this.letter.ToString();
		}

		#region Script
		void Awake()
		{
			this.keyText = this.transform.GetChild(1).GetComponent<TextMesh>();
			this.GetComponent<Rigidbody>().isKinematic = true;
		}


		#endregion
	}
}

