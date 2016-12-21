using UnityEngine;
using System.Collections;

public class StripeController : MonoBehaviour {

	public float distanceToChange;
	public float sizeSpeed;
	public float rotationSpeed;

	private bool isTrigger = false;
	private bool decreaseSize = false;
	private bool increaseSize = false;
	private bool isStripePlaced = false;
	private bool isDraggable = true;

	private GameObject playerIndex;
	private GameObject childObject;
	private GameObject parentObject;


	public void placeStripe(Vector3 endPosition)
	{

		Vector3 _resize = this.parentObject.transform.localScale;
		
		_resize.z = Vector3.Distance(endPosition, this.parentObject.transform.position)*25;
		this.parentObject.transform.localScale = _resize;
		
		Vector3 _direction = (endPosition - this.parentObject.transform.position).normalized;
		Quaternion _lookDirection = Quaternion.LookRotation(_direction);

		this.parentObject.transform.rotation = Quaternion.Slerp(this.parentObject.transform.rotation, _lookDirection, 1);
		this.isStripePlaced = true;
	}

	public void resetStripe()
	{
		this.isStripePlaced = false;
		this.isTrigger = false;
		this.playerIndex = null;
		this.parentObject.transform.localScale = Vector3.one;
		this.parentObject.transform.rotation = Quaternion.Euler(Vector3.zero);
	}

	public void setUndraggable()
	{
		this.isDraggable = false;
	}

	private void changeSize()
	{
		if(this.increaseSize)
		{
			Vector3 _newScale = this.parentObject.transform.localScale;
			
			_newScale.z += Time.deltaTime * sizeSpeed;
			this.parentObject.transform.localScale = _newScale;
			
			if(!this.playerIndex.GetComponent<SphereCollider>().
			   bounds.Contains(this.childObject.transform.position)
			   && this.playerIndex.GetComponent<SphereCollider>().
			   bounds.Intersects(GetComponent<CapsuleCollider>().bounds))
			{
				this.decreaseSize = true;
				this.increaseSize = false;
			}
		}
	}

	private void changeRotation()
	{
		Vector3 _direction = (this.playerIndex.GetComponent<SphereCollider>().bounds.center - this.parentObject.transform.position).normalized;
		Quaternion _lookDirection = Quaternion.LookRotation(_direction);
		this.parentObject.transform.rotation = Quaternion.Slerp(this.parentObject.transform.rotation, _lookDirection, 
		                                                       Time.deltaTime * this.rotationSpeed);
	}
	
	private void resizeParent()
	{
		if (this.decreaseSize) 
		{
			Vector3 _newScale = this.parentObject.transform.localScale;
			_newScale.z -= Time.deltaTime * sizeSpeed;
			this.parentObject.transform.localScale = _newScale;

			if(this.playerIndex.GetComponent<SphereCollider>().
			   bounds.Contains(this.childObject.transform.position) 
			   || !this.playerIndex.GetComponent<SphereCollider>().
			   bounds.Intersects(GetComponent<CapsuleCollider>().bounds))
			{
				this.decreaseSize = false;
				this.increaseSize = true;
			}
		}
	}

	private void executeResize()
	{
		if (!this.isStripePlaced && this.isTrigger) 
		{
			if(this.playerIndex != null)
			{
				this.resizeParent ();
				this.changeRotation ();
				this.changeSize ();
			}
			else
			{
				this.isTrigger = false;
				this.parentObject.transform.localScale = Vector3.one;
				this.parentObject.transform.rotation = Quaternion.Euler(Vector3.zero);
			}
		}
	}

	#region Script
	void Start()
	{
		this.childObject = transform.GetChild (0).gameObject;
		this.parentObject = transform.parent.gameObject;
	}
	
	void Update()
	{
		this.executeResize ();
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(!this.isStripePlaced)
		{
			if(other.tag.Equals("IndexUI"))
			{
				if (!this.isTrigger && this.isDraggable) 
				{
					this.playerIndex = other.gameObject;
					this.increaseSize = true;
					this.isTrigger = true;
				}
			}
		}	
	}
	#endregion

	#region Properties
	public Vector3 HandlePosition
	{
		get { return this.childObject.transform.position; }
	}
	#endregion
}
