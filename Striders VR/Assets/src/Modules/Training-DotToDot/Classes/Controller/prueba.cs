using UnityEngine;
using System.Collections;

public class prueba : MonoBehaviour {

	public float distanceToChange;
	public float sizeSmooth;

	private bool isEnter = false;
	private bool rotateRequest = false;
	private Vector3 lastValueToResize;
	private Vector3 triggerPosition;
	private Transform colliderParent;

	private Vector3 direction;
	private Quaternion lookDirection;

	private bool allowToChange()
	{
		float _currentDistance = Vector3.Distance (this.triggerPosition, transform.localPosition);
		if (_currentDistance > this.distanceToChange)
			return true;
		return false;
	}

	private void changeSize()
	{
		if(this.isEnter && this.allowToChange())
		{
			Vector3 _newScale = new Vector3(0,0,0);
			_newScale.z = Vector3.Distance (this.triggerPosition, transform.localPosition) * sizeSmooth;

			if(this.lastValueToResize.z > transform.localPosition.z){
				this.colliderParent.localScale -= _newScale;
			}
			/*
			else if(this.lastValueToResize.z < transform.localPosition.z){
				this.colliderParent.localScale += _newScale;
			}*/

			this.colliderParent.LookAt (transform.position);
			this.lastValueToResize = transform.localPosition;
			this.triggerPosition = transform.localPosition;
		}
	}


	#region Script
	void Start()
	{
		this.lastValueToResize = new Vector3(0,0,0);
	}

	void Update()
	{
		if (this.rotateRequest) 
		{
			RaycastHit hit;
			Ray detectionRay = new Ray (this.colliderParent.position, this.colliderParent.forward);
			Debug.DrawRay (this.colliderParent.position, this.colliderParent.forward);
			/*
			if (Physics.Raycast (detectionRay, out hit, this.curveDetectionDistance)) 
			{
				if (hit.collider.tag.Equals ("Curve")) 
				{
					this.changeDirection (hit.collider);
				}
			}
			*/
			this.direction = (transform.position - this.colliderParent.position).normalized;
			this.lookDirection = Quaternion.LookRotation(this.direction);

			this.colliderParent.rotation = Quaternion.Slerp(this.colliderParent.rotation, lookDirection, Time.deltaTime * 0.1f);

		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.name.Equals ("Cylinder")) 
		{
			if (!isEnter) 
			{
				this.isEnter = true;
				this.triggerPosition = transform.localPosition;
				this.colliderParent = other.gameObject.transform.parent;
			}
			this.changeSize ();
			this.rotateRequest = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		this.isEnter = false;
		this.rotateRequest = false;
	}
	#endregion

}
