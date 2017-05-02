using UnityEngine;
using StridersVR.Domain;


public class StaticUserController : MonoBehaviour {

	private User user;

	private Training training;


	public void setTraining(Training newTraining)
	{
		this.training = newTraining;
	}

	public void setDifficulty(string newDifficulty)
	{
		this.training.Difficulty = newDifficulty;
	}

	public void gameSelected()
	{
		Debug.Log ("Game: " + this.training.Name);
		Debug.Log ("Difficulty: " + this.training.Difficulty);
	}

	public bool isValidGame()
	{
		if(this.training != null)
			if(!this.training.Difficulty.Equals(""))
				return true;

		return false;
	}

	#region Script
	void Awake () 
	{
		this.training = null;
		//FIXME Descomentar para hacer las pruebas en los mapas o juegos
		//this.user = new User(12, "DSAM");
		//this.training = new Training (1,Application.loadedLevelName);
		//this.training.Difficulty = "Medium";
		GameObject.DontDestroyOnLoad (this);
	}
	#endregion

	#region Properties
	public User User
	{
		get { return this.user; }
		set { this.user = value; }
	}

	public Training Training
	{
		get { return this.training; }
		set { this.training = value; }
	}
	#endregion

}
