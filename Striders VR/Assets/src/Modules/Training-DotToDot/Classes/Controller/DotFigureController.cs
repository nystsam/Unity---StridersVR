using UnityEngine;
using System.Collections;

public class DotFigureController : MonoBehaviour {

	public float distanceToChange;
	public float sizeSpeed;
	public float rotationSpeed;

	private bool placed = false;
	private bool turnedOff = false;
	private bool isTrigger = false;
	private bool intersectToResize = false;
	private bool allowToResize = false;
	private bool allowToRotate = false;

	private Collider triggerCollider;

	private Transform figureParentResizable;

	private Vector3 triggerPosition;
	private Vector3 placedEdgePosition;

	private GameObject handleObject;
	private GameObject dotReferee;


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

				this.placedEdgePosition = this.dotReferee.GetComponent<RefereeController> ().PointBoundingBoxCenter;
				_resize.z = Vector3.Distance(this.placedEdgePosition, this.figureParentResizable.position)*5;

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
		this.resizeParent ();
		this.changeRotation ();
		this.changeSize ();

		if (this.placed) {
			this.turnOffDot ();
			Vector3 _direction = (this.placedEdgePosition - this.figureParentResizable.position).normalized;
			Quaternion _lookDirection = Quaternion.LookRotation(_direction);
			this.figureParentResizable.rotation = Quaternion.Slerp(this.figureParentResizable.rotation, _lookDirection, 
			                                                       Time.deltaTime * 30);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		// ****************************************
		// aplicar regla para reconocer leap motion
		// ****************************************
		if (other.name.Equals ("CubeTrigger")) 
		{
			if (!isTrigger && !placed) 
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
		// ****************************************
		// aplicar regla para reconocer leap motion
		// ****************************************
		if (other.name.Equals ("CubeTrigger")) 
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
	#endregion

}
