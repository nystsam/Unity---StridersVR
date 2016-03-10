using UnityEngine;
using StridersVR.Domain;

public class StaticUserController : MonoBehaviour {

	private string userName;
	private Training training;


	#region Script
	void Awake () 
	{
		this.training = new Training ("Train of Throught");
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
