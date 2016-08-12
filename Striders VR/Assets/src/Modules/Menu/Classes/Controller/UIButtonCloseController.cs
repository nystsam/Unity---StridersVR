using UnityEngine;
using System.Collections;
using StridersVR.Domain;

public class UIButtonCloseController : MonoBehaviour, UIButtonActions {

	private bool isPressed = false;

	private VirtualButton buttonVr;
	
	private float triggerDistance = 0.025f;




	#region UIAction
	public void buttonHover(bool isHitting)
	{
	}
	
	public void buttonAction(GameObject menuOptions)
	{
		this.isPressed = false;
	}
	
	public bool buttonPressed ()
	{
		return this.isPressed;
	}
	#endregion

	#region Script
	void Awake () 
	{
		this.buttonVr = new VirtualButton (this.transform.localPosition, 100, Vector3.forward);
	}

	void Update () 
	{
		this.transform.localPosition = this.buttonVr.ConstraintMovement (this.transform.localPosition, -0.05f, 0f);
		this.GetComponent<Rigidbody> ().AddRelativeForce(this.buttonVr.ApplyRelativeSpring (this.transform.localPosition));
		
		if (!this.isPressed && this.buttonVr.IsButtonPressed (-this.transform.localPosition, this.triggerDistance)) 
		{
			this.isPressed = true;
		} 
		else if (this.isPressed && this.buttonVr.IsButtonReleased (-this.transform.localPosition, this.triggerDistance)) 
		{	
			this.isPressed = false;
		}
	}
	#endregion
}
