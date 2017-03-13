using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StridersVR.Buttons;

public class KeyboardController : MonoBehaviour {

	[SerializeField] private TextMesh inputText;
	[SerializeField] private List<KeyboardButton> buttons;

	private bool isFirstLeter = true;

	public void typeLetter(char letter)
	{
		if(this.inputText.text.Length <= 20)
			this.inputText.text += letter.ToString();

		if(this.isFirstLeter)
		{
			this.isFirstLeter = false;
			this.changeCase(false);
		}
	}

	public void eraseLetter()
	{
		if(this.inputText.text.Length > 0)
			this.inputText.text = this.inputText.text.Remove(this.inputText.text.Length - 1);

		if(this.inputText.text.Length == 0 && !this.isFirstLeter)
		{
			this.isFirstLeter = true;
			this.changeCase(true);
		}
	}

	public void changeCase(bool newCase)
	{
		if(this.buttons.Count > 0)
		{
			foreach(KeyboardButton key in this.buttons)
			{
				key.ChangeCase(newCase);
			}
		}
	}

	public void SetFirstLetterCase()
	{
		if(this.isFirstLeter)
			this.isFirstLeter = false;
	}

	#region Script
	void Start () 
	{
		this.changeCase(this.isFirstLeter);
	}

	void Update () 
	{
	
	}
	#endregion
}
