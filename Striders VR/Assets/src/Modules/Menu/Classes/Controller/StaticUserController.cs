using UnityEngine;
using UnityEngine.UI;
using StridersVR.Domain;


public class StaticUserController : MonoBehaviour {

	public Toggle toggleEasy;
	public Toggle toggleMedium;
	public Toggle toggleHard;

	private string userName;
	private Training training;

	public void selectedToggle()
	{
		if (toggleEasy.isOn) 
		{
			this.training.Difficulty = "Easy";
		} 
		else if (toggleMedium.isOn) 
		{
			this.training.Difficulty = "Medium";
		} 
		else if (toggleHard.isOn) 
		{
			this.training.Difficulty = "Hard";
		}
	}

	#region Script
	void Awake () 
	{
		this.training = new Training (Application.loadedLevelName);
		this.training.Difficulty = "Easy";
		GameObject.DontDestroyOnLoad (this);
	}
	#endregion

	#region Properties
	public string UserName
	{
		get { return this.userName; }
		set { this.userName = value; }
	}

	public Training Training
	{
		get { return this.training; }
		set { this.training = value; }
	}
	#endregion

}
