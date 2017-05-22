using UnityEngine;
using System.Collections;
using Leap;

public class GrabController : MonoBehaviour {

	public float grabStart;
	public float grabContinue;
	//public float grabbingTime;

	private bool isGrabbing;
	private bool enoughStrength;
	//[SerializeField]private bool isTouching;

	private HandModel handModel;

	private ItemDraggableController playerItem;

	private Vector3 itemStartingPosition;

	private Quaternion itemStartingRotation;

	private RaycastHit hit;

	[SerializeField] private float hitRange;


	public bool IsGrabbingObject()
	{
		if(this.isGrabbing && this.playerItem != null)
			return true;
		return false;
	}

	public void placingObject()
	{
		this.isGrabbing = false;
		this.playerItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
	}

	public GameObject GetDraggableItem()
	{
		if(this.playerItem != null)
			return this.playerItem.gameObject;
		return null;
	}

//	private bool smoothTime()
//	{
//		this.grabbingTime += Time.deltaTime;
//		if(this.grabbingTime > 0.35f)
//			return true;
//
//		return false;
//	}

	private void casthit()
	{
		Debug.DrawRay(this.handModel.palm.position, -this.handModel.palm.up * this.hitRange);
		Ray ray = new Ray(this.handModel.palm.position, -this.handModel.palm.up * this.hitRange);
		if(Physics.Raycast(ray, out hit, this.hitRange))
		{
			if(hit.collider.tag.Equals("DraggableItem") && this.enoughStrength)
			{
				this.playerItem = this.hit.collider.GetComponent<ItemDraggableController>();
				this.isGrabbing = true;
			}
		}
	}

	private Vector3 middlePostionBetweenFingersPalm()
	{
		Vector3 thumb = this.handModel.fingers[0].bones[3].transform.position;
		Vector3 middle = this.handModel.fingers[3].bones[3].transform.position;

		return (((thumb + middle)/2) + this.handModel.palm.position)/2;
	}

	private void cancelObject()
	{
		this.playerItem = null;
		this.isGrabbing = false;
	}

	#region Sript
	void Start () 
	{
//		this.grabbingTime = 0f;
//		this.playerItem = GameObject.FindGameObjectWithTag("DraggableItem");

		this.isGrabbing = false;
		this.enoughStrength = false;
		this.handModel = this.transform.GetComponent<HandModel> ();
	}

	void Update () 
	{
		if(!this.isGrabbing)
		{
			this.casthit();
		}
		else if(this.isGrabbing && this.playerItem != null)
		{
			if(this.playerItem.IsDraggable && this.enoughStrength)
			{
				this.playerItem.transform.position = this.middlePostionBetweenFingersPalm() + new Vector3(-0.1f,0,-0.1f);
				this.playerItem.transform.rotation = this.handModel.palm.rotation;
			}
			else
			{
				if(this.playerItem.IsDraggable)
					this.playerItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
				this.cancelObject();
			}
		}

		if(this.isGrabbing && UIMenuOptions.Current.DisableHandActions)
		{
			this.playerItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			this.cancelObject();
		}
//		Vector3 indexPosition = this.handModel.fingers [1].GetBoneCenter (3);
//		Vector3 thumbPosition = this.handModel.fingers [0].GetBoneCenter (3);
		float grab = this.handModel.GetLeapHand ().PinchStrength;

		if (!this.enoughStrength && grab > this.grabStart) 
		{
			this.enoughStrength = true;
		} 
		else if (this.enoughStrength && grab < this.grabContinue) 
		{
			this.enoughStrength = false;
		}
//
//		if (this.isGrabbing && this.isTouching) 
//		{
//			this.playerItem.transform.localPosition = (indexPosition + thumbPosition) / 2;
//			this.playerItem.transform.localRotation = Quaternion.Euler(new Vector3(315,340,300));
//		}
//
//		if (this.playerItem == null) 
//		{
//			this.playerItem = GameObject.FindGameObjectWithTag("DraggableItem");
//		}
	}

	void OnDestroy()
	{
		if(this.isGrabbing && this.playerItem != null)
		{
			this.playerItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			this.cancelObject();
		}
	}
	#endregion

//	#region Properties
//	public bool IsGrabbing
//	{
//		get { return this.isGrabbing; }
//	}
//	public bool IsTouching
//	{
//		get { return this.isTouching; }
//		set { this.isTouching = value; }
//	}
//	#endregion
}
