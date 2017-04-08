using UnityEngine;
using Leap;
using System.Collections;

public class UILogOutController : MonoBehaviour {

	[SerializeField] private GameObject LogOutPrefab;

	private GameObject LogOutButton;

	private bool isButtonActive = false;
	private bool isInstantiated = false;
	
	private HandModel hand;
	private HandModel leftHand;
	
	private void setLogOutButton()
	{
		if (this.hand.GetLeapHand ().IsLeft) 
		{
			this.leftHand = this.transform.GetComponent<HandModel> ();
			this.LogOutButton = (GameObject)GameObject.Instantiate (this.LogOutPrefab);
			this.LogOutButton.transform.parent = this.hand.palm;
			this.LogOutButton.transform.localPosition = new Vector3 (-1f, -0.5f, -3f);
			this.LogOutButton.transform.localRotation = Quaternion.Euler(new Vector3(-90,90,0));
			this.LogOutButton.transform.localScale = new Vector3(0.6f, 0.6f, 0.001f);
			this.LogOutButton.SetActive (false);

			this.isInstantiated = true;
		}
	}
	
	private void enableLogOutButton()
	{
		if(this.isInstantiated)
		{
			
			if (this.leftHand.palm.up.y < -0.6f && 
			    !this.isButtonActive) 
			{
				if(GameObject.FindGameObjectWithTag ("StaticUser").GetComponent<StaticUserController> ().User != null)
				{
					this.LogOutButton.SetActive (true);
					this.isButtonActive = true;
				}
			} 
			else if(this.leftHand.palm.up.y > -0.6f &&
			        this.isButtonActive)
			{
				this.LogOutButton.SetActive (false);
				this.isButtonActive = false;
			}
		}
	}
	
	#region Script
	void Start () 
	{
		this.hand = this.transform.GetComponent<HandModel> ();
		this.setLogOutButton ();
	}
	
	void Update () 
	{
		this.enableLogOutButton ();
	}
	#endregion
}
