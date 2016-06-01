using UnityEngine;
using System.Collections;
using Leap;

public class GrabController : MonoBehaviour {

	public float grabStart;
	public float grabContinue;
	public float grabbingTime;

	private bool isGrabbing;
	private bool isTouching;

	private HandModel handModel;

	private GameObject playerItem;

	private Vector3 itemStartingPosition;

	private Quaternion itemStartingRotation;

	private bool smoothTime()
	{
		this.grabbingTime += Time.deltaTime;
		if(this.grabbingTime > 0.35f)
			return true;

		return false;
	}


	#region Sript
	void Start () 
	{
		this.grabbingTime = 0f;
		this.playerItem = GameObject.FindGameObjectWithTag("DraggableItem");

		this.isGrabbing = false;
		this.handModel = this.transform.GetComponent<HandModel> ();
	}

	void Update () 
	{
		Vector3 indexPosition = this.handModel.fingers [1].GetBoneCenter (3);
		Vector3 thumbPosition = this.handModel.fingers [0].GetBoneCenter (3);
		float grab = this.handModel.GetLeapHand ().PinchStrength;

		if (!this.isGrabbing && grab > this.grabStart) 
		{
			this.isGrabbing = true;
			this.grabbingTime = 0f;
		} 
		else if (this.isGrabbing && grab < this.grabContinue) 
		{
			if(this.smoothTime())
				this.isGrabbing = false;
//			this.playerItem.transform.localPosition = this.itemStartingPosition;
//			this.playerItem.transform.localRotation = this.itemStartingRotation;
		}

		if (this.isGrabbing && this.isTouching) 
		{
			this.playerItem.transform.localPosition = (indexPosition + thumbPosition) / 2;
			this.playerItem.transform.localRotation = Quaternion.Euler(new Vector3(315,340,300));
		}

		if (this.playerItem == null) 
		{
			this.playerItem = GameObject.FindGameObjectWithTag("DraggableItem");
		}
	}
	#endregion

	#region Properties
	public bool IsGrabbing
	{
		get { return this.isGrabbing; }
	}
	public bool IsTouching
	{
		get { return this.isTouching; }
		set { this.isTouching = value; }
	}
	#endregion
}
