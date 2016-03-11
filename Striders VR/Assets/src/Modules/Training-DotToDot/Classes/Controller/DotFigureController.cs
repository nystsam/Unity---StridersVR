using UnityEngine;
using System.Collections;

public class DotFigureController : MonoBehaviour {

	public float distanceToChange;
	public float sizeSpeed;
	public float rotationSpeed;

	private bool isTrigger = false;
	private Collider triggerCollider;
	private Transform triggerColliderParent;
	private Vector3 triggerPosition;
	private bool allowToResize = false;
	private bool allowToRotate = false;
	private GameObject handleObject;


	private bool triggerDistance()
	{
		float _currentDistance = Vector3.Distance (this.triggerPosition, transform.localPosition);
		if (_currentDistance > this.distanceToChange)
			return true;
		return false;
	}

	private void changeSize()
	{
		if(this.isTrigger && this.triggerDistance())
		{
			Vector3 _newScale = this.triggerColliderParent.localScale;
			float _newDistance = Vector3.Distance (this.triggerPosition, 
			                                       this.triggerCollider.transform.localPosition) * sizeSpeed;
			_newScale.z += _newDistance;
			this.triggerColliderParent.localScale = _newScale;

			if(!this.triggerCollider.bounds.Contains(this.handleObject.transform.position)
			   && this.triggerCollider.bounds.Intersects(GetComponent<CapsuleCollider>().bounds))
			{
				this.allowToResize = true;
			}
			this.triggerPosition = this.triggerCollider.transform.localPosition;
		}
	}

	private void changeRotation()
	{
		if (this.allowToRotate) 
		{
			Vector3 _direction = (this.triggerCollider.transform.position - this.triggerColliderParent.position).normalized;
			Quaternion _lookDirection = Quaternion.LookRotation(_direction);
			this.triggerColliderParent.rotation = Quaternion.Slerp(this.triggerColliderParent.rotation, _lookDirection, 
			                                                       Time.deltaTime * this.rotationSpeed);
		}
	}

	private void resizeParent()
	{
		if (this.allowToResize) 
		{
			Vector3 _newScale = this.triggerColliderParent.localScale;
			_newScale.z -= 0.5f;
			this.triggerColliderParent.localScale = _newScale;
			if(this.triggerCollider.bounds.Contains(this.handleObject.transform.position))
			{
				this.allowToResize = false;
			}
		}
	}

	#region Script
	void Start()
	{
		this.handleObject = transform.GetChild (0).gameObject;
		this.triggerColliderParent = transform.parent;
	}

	void Update()
	{
		this.changeRotation ();
		this.changeSize ();
		this.resizeParent ();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.name.Equals ("CubeTrigger")) 
		{
			if (!isTrigger) 
			{
				this.isTrigger = true;
				this.triggerPosition = other.transform.localPosition;
				this.triggerCollider = other;
				this.allowToRotate = true;
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		this.isTrigger = false;
		this.allowToRotate = false;
		this.allowToResize = false;
	}
	#endregion

}
