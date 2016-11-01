using UnityEngine;
using System.Collections;
using StridersVR.Domain.Menu;

public class DifficultySelectionController : MonoBehaviour {

	private DifficultyButton currentButton;


	public void selectDifficulty(DifficultyButton newDifficulty)
	{
		if(this.currentButton != newDifficulty)
		{
			if(this.currentButton != null)
				this.currentButton.toogleOff ();

			this.currentButton = newDifficulty;
			this.currentButton.toogleOn ();
			this.selectDifficulty();
		}
	}

	/* Setting Difficulty to Static User */
	private void selectDifficulty()
	{
		GameObject.FindGameObjectWithTag ("StaticUser").GetComponent<StaticUserController> ().setDifficulty (currentButton.Difficulty);
	}
	/* ********************* */

	#region script
	void Awake () 
	{
		this.currentButton = null;
	}
	#endregion
}
