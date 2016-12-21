using UnityEngine;
using System.Collections;
using Leap;

public class CameraLookHand : MonoBehaviour {

	public GameObject lookDiretion;

	private Camera myCamera;

	private bool isLeftHand = false;

	void Start () 
	{
		if (this.GetComponent<HandModel> ().GetLeapHand ().IsLeft) 
		{
			this.isLeftHand = true;
		}

		if(this.isLeftHand)
		{
			this.myCamera = Camera.main;
		}
	}

	void Update () 
	{
		if (this.isLeftHand) 
		{
			Quaternion _targetRotation = Quaternion.LookRotation((this.lookDiretion.transform.position - this.myCamera.transform.position).normalized);
			
			// Smoothly rotate towards the target point.
			this.myCamera.transform.rotation = Quaternion.Slerp(this.myCamera.transform.rotation, _targetRotation, 50 * Time.deltaTime);
		}
	}
}
