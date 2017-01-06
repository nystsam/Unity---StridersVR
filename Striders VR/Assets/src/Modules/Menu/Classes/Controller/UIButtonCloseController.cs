using UnityEngine;
using System.Collections;
using StridersVR.Domain;

public class UIButtonCloseController : MonoBehaviour {

	private GameObject UIGameController;

	private bool isPressed = false;

	public float triggerDistance;
	public float spring;
	public float min;
	public float max;

	private VirtualButton buttonVr;


	private void buttonAction()
	{
		this.UIGameController.transform.FindChild("ToolsPanelUI").GetComponent<UIMenuOptions>().desactiveMenu();
		CameraUITools.Current.ChangePosition(false);
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
		this.buttonVr = new VirtualButton (this.transform.localPosition, spring, Vector3.forward);
		this.UIGameController = GameObject.FindGameObjectWithTag ("PlayerPanelButtons");
	}

	void Update () 
	{
		this.transform.localPosition = this.buttonVr.ConstraintMovement (this.transform.localPosition, min, max);
		this.GetComponent<Rigidbody> ().AddRelativeForce(this.buttonVr.ApplyRelativeSpring (this.transform.localPosition));

		this.buttonPressed ();
	}

	void OnDisable()
	{
		this.transform.localPosition = this.buttonVr.RestingPosition;
		this.GetComponent<Rigidbody>().velocity = Vector3.zero;
		this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
	}
	#endregion
}
