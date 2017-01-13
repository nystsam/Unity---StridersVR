using UnityEngine;
using System.Collections;

public class ItemDraggableController : MonoBehaviour {

//	private GameObject hand;

//	private bool isPlaced = false;
	private bool isDraggable = true;
	public bool IsDraggable 
	{
		get { return isDraggable; }
		set { isDraggable = value; }
	}

	private bool watchObjectPosition = false;

	private Vector3 itemStartingPosition;
//	private Quaternion itemStartingRotation;

//	private bool isHandController(Collider other)
//	{
//		if (other.GetComponentInParent<HandController> ())
//			return true;
//		return false;
//	}

//	public void stopDragging()
//	{
//		this.isPlaced = true;
//		this.hand.GetComponentInParent<GrabController> ().IsTouching = false;
//	}

	private void resetPosition()
	{
		this.GetComponent<Rigidbody>().velocity = Vector3.zero;
		this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		this.transform.position = this.itemStartingPosition;
		this.transform.rotation = Quaternion.Euler(Vector3.zero);
		StartCoroutine(this.freezeConstraints());
	}

	private IEnumerator freezeConstraints()
	{
		yield return new WaitForSeconds(0.25f);
		this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
		this.watchObjectPosition = true;
	}

	#region Script
	void Start()
	{
		this.itemStartingPosition = this.transform.position;
		StartCoroutine(this.freezeConstraints());
	}

	void Update()
	{
		if(this.watchObjectPosition)
		{
			if(Mathf.Abs(this.transform.position.x) > Mathf.Abs(this.itemStartingPosition.x * 3) ||
			   Mathf.Abs(this.transform.position.y) > Mathf.Abs(this.itemStartingPosition.y * 3) ||
			   Mathf.Abs(this.transform.position.z) > Mathf.Abs(this.itemStartingPosition.z * 3))
			{
				this.watchObjectPosition = false;
				this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
				this.resetPosition();
			}
		}
	}

	void OnCollisionEnter(Collision other)
	{
		// Si toca el piso...
		if(other.collider.tag.Equals("EditorOnly"))
		{
			this.resetPosition();
		}
	}

//	void OnTriggerEnter(Collider other)
//	{
//		if (this.isHandController (other) && !this.IsPlaced) 
//		{
//			other.GetComponentInParent<GrabController>().IsTouching = true;
//			this.hand = other.gameObject;
//		}
//	}
//
//	void OnTriggerExit(Collider other)
//	{
//		if (this.isHandController (other)) 
//		{
//			if(!other.GetComponentInParent<GrabController>().IsGrabbing)
//			{
//				other.GetComponentInParent<GrabController>().IsTouching = false;
//				this.transform.localRotation = this.itemStartingRotation;
//				if(!this.isPlaced)
//				{
//					this.transform.localPosition = this.itemStartingPosition;
//				}
//			}
//
//		}
//	}
	#endregion

//	#region Properties
//	public bool IsPlaced
//	{
//		get { return this.isPlaced; }
//	}
//	#endregion
}

