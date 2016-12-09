using UnityEngine;
using System.Collections;
using StridersVR.Domain;

[RequireComponent(typeof (Rigidbody))]
[RequireComponent(typeof (SpringJoint))]
public class FinishModelButtonController : MonoBehaviour {

	private Transform gameButton;

	private VirtualButton buttonVr;

	private float triggerDistance = 0.35f;

	private bool isPressed;
	private bool isFinger;


	private bool isRightFinger(GameObject other)
	{
		if (other.tag.Equals ("IndexRight"))
			return true;
		return false;
	}

	private void buttonAction()
	{
		GameObject _pointManager = GameObject.FindGameObjectWithTag("GameController");

		_pointManager.GetComponent<PointManagerController> ().finishModel ();
	}

	private void buttonPressed ()
	{
		if (!this.isPressed && this.buttonVr.IsButtonPressed (-this.transform.localPosition, this.triggerDistance)) 
		{
			if(this.isFinger)
			{
				this.isPressed = true;
				this.buttonAction();
			}
		} 
		else if (this.isPressed && this.buttonVr.IsButtonReleased (-this.transform.localPosition, this.triggerDistance)) 
		{	
			this.isPressed = false;
		}
	}

	#region Button Decoration

	#endregion

	#region Script
	void Awake () 
	{
		this.buttonVr = new VirtualButton (this.transform.localPosition, 200, Vector3.forward);
		this.gameButton = this.transform.GetChild (0);
		this.GetComponent<SpringJoint> ().connectedAnchor = this.gameButton.position;
		this.isPressed = false;
		this.isFinger = false;
	}

	void Update () 
	{
		this.transform.localPosition = this.buttonVr.ConstraintMovement (this.transform.localPosition, -0.5f, 0);
		this.GetComponent<Rigidbody> ().AddRelativeForce(this.buttonVr.ApplyRelativeSpring (this.transform.localPosition));

		this.buttonPressed ();
	}

	void OnCollisionEnter(Collision other)
	{
		this.isFinger = this.isRightFinger (other.gameObject);
	}

	void OnCollisionExit(Collision other)
	{
		this.isFinger = this.isRightFinger (other.gameObject);
	}
	#endregion
}
