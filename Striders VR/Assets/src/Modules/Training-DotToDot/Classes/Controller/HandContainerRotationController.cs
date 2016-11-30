using UnityEngine;
using System.Collections;
using Leap;

public class HandContainerRotationController : MonoBehaviour {

	private GameObject platform;

	private bool isRightHand = false;
	private bool isGrabbing = false;


	#region Script
	void Start()
	{
		this.platform = GameObject.FindGameObjectWithTag("DotContainer");
		if (this.GetComponent<HandModel> ().GetLeapHand ().IsRight) 
		{
			this.isRightHand = true;
		}
	}

	void Update()
	{
		if(this.isRightHand)
		{
			float grabValue = this.GetComponent<HandModel>().GetLeapHand().GrabStrength;
			if(grabValue > 0.8f && !this.isGrabbing)
			{
				this.isGrabbing = true;
				Debug.Log ("Apretando");
			}
			else if(grabValue < 0.8f && this.isGrabbing)
			{
				this.isGrabbing = false;
				Debug.Log ("Dejo de apretar");
			}
		}

//		if(this.isGrabbing)
//		{
//			this.platform.transform.rotation = Quaternion.Lerp(this.platform.transform.rotation, 
//			                                                    this.GetComponent<HandModel>().GetPalmRotation(), 
//			                                                    Time.deltaTime * 1);		}
	}
	#endregion
}