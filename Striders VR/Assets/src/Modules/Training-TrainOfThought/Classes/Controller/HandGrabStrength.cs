using UnityEngine;
using System.Collections;
using Leap;

public class HandGrabStrength : MonoBehaviour {

	[SerializeField] private float minDistance;
	[SerializeField] private float maxDistance;
	[SerializeField] private float startGrab;
	[SerializeField] private float grabToContinue;

	private bool isGrabbing = false;
	private bool isRightHand = false;

	private Vector3 distanceBetweenIndexThumb;

	private HandModel rightHand = null;


	public bool IsGrabbing()
	{
		return this.isGrabbing;
	}

	public bool IsRightHand()
	{
		return this.isRightHand;
	}

	#region Script
	void Start () 
	{
		if(this.GetComponent<HandModel>().GetLeapHand().IsRight)
		{
			this.rightHand = this.GetComponent<HandModel>();
			this.isRightHand = true;
		}
	}

	void Update () 
	{
		if(this.rightHand != null)
		{
			Vector3 indexPosition = this.rightHand.fingers[1].GetBoneCenter(3);
			Vector3 thumbPosition = this.rightHand.fingers[0].GetBoneCenter(3);

			float distance = (indexPosition - thumbPosition).magnitude;

			float normalizedDistance = (distance - this.minDistance)/(this.maxDistance - this.minDistance);
			float grab = 1.0f - Mathf.Clamp01(normalizedDistance);

			if(!this.isGrabbing && grab > this.startGrab)
			{
				this.isGrabbing = true;
				Debug.Log ("True");
			}
			else if(this.isGrabbing && grab < this.grabToContinue)
			{
				this.isGrabbing = false;
				Debug.Log ("False");
			}
		}
	}
	#endregion
}
