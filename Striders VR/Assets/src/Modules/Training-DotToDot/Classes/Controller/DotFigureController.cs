using UnityEngine;
using System.Collections;

public class DotFigureController : MonoBehaviour {

	public float distanceToChange;
	public float sizeSpeed;
	public float rotationSpeed;

	private bool placed = false;
	private bool isIntersecting = false;
	private bool nearlyPoint = false;
	private bool turnedOff = false;
	private bool isTrigger = false;
	private bool intersectToResize = false;
	private bool allowToResize = false;
	private bool allowToRotate = false;

	private Collider triggerCollider;

	private Transform figureParentResizable;

	private Vector3 triggerPosition;

	private GameObject attachedPoint;
	private GameObject handleObject;
	private GameObject dotReferee;


	private bool isHand(Collider other)
	{
		if (other.GetComponentInParent<HandController> ()) 
		{
			Transform physicHand = other.GetComponentInParent<HandController> ().transform.FindChild("RigidRoundHand(Clone)");
			Collider indexBone3 = physicHand.FindChild("index").GetChild(2).GetComponent<CapsuleCollider>();
			this.triggerCollider = indexBone3;

			return true;
		}
		return false;
	}

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
			Vector3 _newScale = this.figureParentResizable.localScale;
			float _newDistance = Vector3.Distance (this.triggerPosition, 
			                                       this.triggerCollider.transform.localPosition) * sizeSpeed;
			_newScale.z += _newDistance;
			this.figureParentResizable.localScale = _newScale;

			if(!this.intersectToResize)
			{
				if(this.triggerCollider.bounds.Contains(this.handleObject.GetComponent<BoxCollider>().bounds.center))
				{
					this.intersectToResize = true;	
				}
			}

			if(!this.triggerCollider.bounds.Contains(this.handleObject.transform.position)
			   && this.triggerCollider.bounds.Intersects(GetComponent<CapsuleCollider>().bounds))
			{
				if(this.intersectToResize)
				{
					this.allowToResize = true;
				}
			}
			this.triggerPosition = this.triggerCollider.transform.localPosition;
		}
	}

	private void changeRotation()
	{
		if (this.allowToRotate) 
		{
			Vector3 _direction = (this.triggerCollider.bounds.center - this.figureParentResizable.position).normalized;
			Quaternion _lookDirection = Quaternion.LookRotation(_direction);
			this.figureParentResizable.rotation = Quaternion.Slerp(this.figureParentResizable.rotation, _lookDirection, 
			                                                       Time.deltaTime * this.rotationSpeed);
		}
	}

	private void resizeParent()
	{
		if (this.allowToResize) 
		{
			Vector3 _newScale = this.figureParentResizable.localScale;
			_newScale.z -= 1.5f;
			this.figureParentResizable.localScale = _newScale;
			if(this.triggerCollider.bounds.Contains(this.handleObject.transform.position))
			{
				this.allowToResize = false;
			}
			else if(Vector3.Distance(this.triggerCollider.bounds.center, this.handleObject.transform.position) > 2.5)
			{
				_newScale.z = 1;
				this.figureParentResizable.localScale = _newScale;
				this.allowToResize = false;
			}
		}
	}

	private void turnOffDot()
	{
		if (!turnedOff) 
		{
			this.turnedOff = true;
			this.isTrigger = false;
			this.allowToRotate = false;
			this.allowToResize = false;
			if (this.dotReferee.GetComponent<RefereeController> ().IsHoldingDot) {
				this.dotReferee.GetComponent<RefereeController> ().IsHoldingDot = false;
			}

			if(this.placed)
			{
				Vector3 _resize = this.figureParentResizable.localScale;

				_resize.z = Vector3.Distance(this.attachedPoint.GetComponent<SphereCollider>().bounds.center, this.figureParentResizable.position)*25;

				this.figureParentResizable.transform.localScale = _resize;
			}
		}
	}


	#region Script
	void Start()
	{
		this.handleObject = transform.GetChild (0).gameObject;
		this.figureParentResizable = transform.parent;
		this.dotReferee = GameObject.FindGameObjectWithTag("DotReferee");
	}

	void Update()
	{
		if (this.triggerCollider != null) 
		{
			this.resizeParent ();
			this.changeRotation ();
			this.changeSize ();
		}

		if (this.placed && this.isIntersecting) 
		{
			this.turnOffDot ();
			Vector3 _direction = (this.attachedPoint.GetComponent<SphereCollider>().bounds.center - this.figureParentResizable.position).normalized;
			Quaternion _lookDirection = Quaternion.LookRotation(_direction);
			this.figureParentResizable.rotation = Quaternion.Slerp(this.figureParentResizable.rotation, _lookDirection, 
		        	                                               Time.deltaTime * 30);
		}

		if (this.nearlyPoint) 
		{
			if (this.attachedPoint == null) {
				this.attachedPoint = this.dotReferee.GetComponent<RefereeController> ().EndingPoint;
			}
			
			if (this.attachedPoint.GetComponent<SphereCollider> ().bounds.Contains (this.handleObject.GetComponent<BoxCollider> ().bounds.center)) {
				this.isIntersecting = true;
				this.nearlyPoint = false;
			}
		} 
		else if(!this.isIntersecting &&!this.placed)
		{
			if (this.attachedPoint != null) 
			{
				this.attachedPoint = null;
			}
		}
		
	}

	void OnTriggerEnter(Collider other)
	{
		// ****************************************
		// aplicar regla para reconocer leap motion
		// ****************************************
//		if (other.name.Equals ("CubeTrigger")) 
		if(this.isHand(other))
		{
			if (!isTrigger && !placed) 
			{
				this.isTrigger = true;
				//this.triggerCollider = other;
				this.triggerPosition = this.triggerCollider.transform.localPosition;
				this.allowToRotate = true;
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		// ****************************************
		// aplicar regla para reconocer leap motion
		// ****************************************
//		if (other.name.Equals ("CubeTrigger")) 
		if(this.isHand(other))
		{
			if(!other.bounds.Contains(this.GetComponent<CapsuleCollider>().bounds.max) 
			   && !other.bounds.Contains(this.handleObject.GetComponent<BoxCollider>().bounds.center)
			   && this.intersectToResize)
			{
				this.turnOffDot ();
			}
		}
	}
	#endregion

	#region Properties
	public bool Placed
	{
		get { return this.placed; }
		set { this.placed = value; }
	}

	public bool NearlyPoint
	{
		set { this.nearlyPoint = value; }
	}

	public bool IsIntersecting
	{
		get { return this.isIntersecting; }
	}
	#endregion

}
