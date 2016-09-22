using UnityEngine;
using System.Collections;
using StridersVR.Domain;

public class UIButtonCloseController : MonoBehaviour, UIButtonActions {

	private GameObject UIGameController;

	private bool isPressed = false;

	private VirtualButton buttonVr;
	
	private float triggerDistance = 0.075f;

	#region UIAction
	public void buttonHover(bool isHitting)
	{
	}
	#endregion

	private void buttonAction()
	{
		this.UIGameController.transform.FindChild("ToolsPanelUI").GetComponent<UIMenuOptions>().desactiveMenu();
	}
	
	private void buttonPressed ()
	{
		if (!this.isPressed && this.buttonVr.IsButtonPressed (-this.transform.localPosition, this.triggerDistance)) 
		{
			this.isPressed = true;
			this.buttonAction();
		} 
		else if (this.isPressed && this.buttonVr.IsButtonReleased (-this.transform.localPosition, this.triggerDistance)) 
		{	
			this.isPressed = false;
		}
	}


	#region Script
	void Awake () 
	{
		this.buttonVr = new VirtualButton (this.transform.localPosition, 100, Vector3.forward);
		this.UIGameController = GameObject.FindGameObjectWithTag ("GameController");
	}

	void Update () 
	{
		this.transform.localPosition = this.buttonVr.ConstraintMovement (this.transform.localPosition, -0.1f, 0f);
		this.GetComponent<Rigidbody> ().AddRelativeForce(this.buttonVr.ApplyRelativeSpring (this.transform.localPosition));

		this.buttonPressed ();
	}
	#endregion
}
