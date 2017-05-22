using UnityEngine;
using System.Collections;

public class HandFinishButtonController : MonoBehaviour {

	private HandModel rightHand;

	private bool isRightHand = false;
	private bool isRightPalmUp = false;

	private GameObject finishButtonPrefab;
	private GameObject finishButton;
	private GameObject gameController;

	private void buttonConfiguration()
	{
		if(this.gameController.GetComponent<PointManagerController>().getFinishButtonStatus())
		{
			this.gameController.GetComponent<PointManagerController>().setFinishButtonStatus(false);
		
			this.finishButton = (GameObject)GameObject.Instantiate(this.finishButtonPrefab);
			Vector3 _newPosition = this.rightHand.palm.position + new Vector3(0,0.85f,-1f);
			this.finishButton.GetComponent<FinishModelButtonController>().initButton(_newPosition);
			this.finishButton.GetComponent<FinishModelButtonController>().activeTimer();
		}
	}

	#region Script
	void Start () 
	{
		if(this.GetComponent<HandModel>().GetLeapHand().IsRight)
		{
			this.rightHand = this.GetComponent<HandModel>();
			this.isRightHand = true;
			this.finishButtonPrefab = Resources.Load("Prefabs/Training-DotToDot/DoneButton", typeof(GameObject)) as GameObject;
			this.gameController = GameObject.FindGameObjectWithTag("GameController");
		}
	}
	
	void Update () 
	{
		if(this.isRightHand)
		{
			if(this.rightHand.palm.up.y < -0.6f && !this.isRightPalmUp)
			{
				this.isRightPalmUp = true;
				this.buttonConfiguration();
			}
			else if(this.rightHand.palm.up.y > -0.6f && this.isRightPalmUp)
			{
				this.isRightPalmUp = false;
			}
		}
	}
	#endregion
}
