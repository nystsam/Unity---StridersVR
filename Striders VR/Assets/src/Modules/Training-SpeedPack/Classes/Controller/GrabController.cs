using UnityEngine;
using System.Collections;
using Leap;

public class GrabController : MonoBehaviour {

//	public float grabMinDistance;
//	public float grabMaxDistance;
	public float grabStart;
	public float grabContinue;

	private bool isGrabbing;

	private HandModel handModel;

	private GameObject playerItem;

	private Vector3 itemStartingPosition;
	private Quaternion itemStartingRotation;

	#region Sript
	void Start () 
	{
		this.playerItem = GameObject.FindGameObjectWithTag("DraggableItem");
		this.itemStartingPosition = new Vector3(this.playerItem.transform.localPosition.x, 
		                                        this.playerItem.transform.localPosition.y, 
		                                        this.playerItem.transform.localPosition.z);
		this.itemStartingRotation = new Quaternion (this.playerItem.transform.localRotation.x, 
		                                            this.playerItem.transform.localRotation.y, 
		                                            this.playerItem.transform.localRotation.z, 
		                                            this.playerItem.transform.localRotation.w);

		this.isGrabbing = false;
		this.handModel = this.transform.GetComponent<HandModel> ();
	}

	void Update () 
	{
		Vector3 indexPosition = this.handModel.fingers [1].GetBoneCenter (3);
		Vector3 thumbPosition = this.handModel.fingers [0].GetBoneCenter (3);
//		float distance = (indexPosition - thumbPosition).magnitude;
//		float normalizedDistance = (distance - this.grabMinDistance) / (this.grabMaxDistance - this.grabMinDistance);
//		float pinch = 1.0f - Mathf.Clamp01 (normalizedDistance);
		float grab = this.handModel.GetLeapHand ().PinchStrength;
		
		if (!this.isGrabbing && grab > this.grabStart) 
		{
			this.isGrabbing = true;
		} 
		else if (this.isGrabbing && grab < this.grabContinue) 
		{
			this.isGrabbing = false;
			this.playerItem.transform.localPosition = this.itemStartingPosition;
			this.playerItem.transform.localRotation = this.itemStartingRotation;
		}

		if (this.isGrabbing) 
		{
			this.playerItem.transform.localPosition = (indexPosition + thumbPosition)/2;
			this.playerItem.transform.localRotation = Quaternion.Euler(new Vector3(0,0,0));
		}
	}
	#endregion

	#region Properties
	public bool IsGrabbing
	{
		get { return this.isGrabbing; }
	}
	#endregion
}
