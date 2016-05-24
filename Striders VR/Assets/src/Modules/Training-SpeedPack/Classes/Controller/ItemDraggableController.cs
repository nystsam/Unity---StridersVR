using UnityEngine;
using System.Collections;

public class ItemDraggableController : MonoBehaviour {

	private GameObject hand;

	private bool isPlaced = false;

	private Vector3 itemStartingPosition;
	private Quaternion itemStartingRotation;

	private bool isHandController(Collider other)
	{
		if (other.GetComponentInParent<HandController> ())
			return true;
		return false;
	}

	public void stopDragging()
	{
		this.isPlaced = true;
		this.hand.GetComponentInParent<GrabController> ().IsTouching = false;
	}

	#region Script
	void Start()
	{
		this.itemStartingPosition = new Vector3(this.transform.localPosition.x, 
		                                        this.transform.localPosition.y, 
		                                        this.transform.localPosition.z);
		this.itemStartingRotation = new Quaternion (this.transform.localRotation.x, 
		                                            this.transform.localRotation.y, 
		                                            this.transform.localRotation.z, 
		                                            this.transform.localRotation.w);
	}

	void OnTriggerEnter(Collider other)
	{
		if (this.isHandController (other) && !this.IsPlaced) 
		{
			other.GetComponentInParent<GrabController>().IsTouching = true;
			this.hand = other.gameObject;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (this.isHandController (other)) 
		{
			if(!other.GetComponentInParent<GrabController>().IsGrabbing)
			{
				other.GetComponentInParent<GrabController>().IsTouching = false;
				this.transform.localRotation = this.itemStartingRotation;
				if(!this.isPlaced)
				{
					this.transform.localPosition = this.itemStartingPosition;
				}
			}

		}
	}
	#endregion

	#region Properties
	public bool IsPlaced
	{
		get { return this.isPlaced; }
	}
	#endregion
}

