using UnityEngine;
using System.Collections;
using StridersVR.Domain;
using StridersVR.Domain.Menu;
using StridersVR.ScriptableObjects.Menu;

public class MenuButtonController : MonoBehaviour {

	public string buttonName;

	public ScriptableObjectMenuButtons buttonData;


	private float triggerDistance = 0.15f;

	private bool isPressed = false;

	private MenuButton localButton;

	private VirtualButton virtualButton;


	private void buttonPressed ()
	{
		if (!this.isPressed && this.virtualButton.IsButtonPressed (this.transform.localPosition, this.triggerDistance)) 
		{
			this.isPressed = true;
			this.localButton.buttonAction();
		} 
		else if (this.isPressed && this.virtualButton.IsButtonReleased (this.transform.localPosition, this.triggerDistance)) 
		{	
			this.isPressed = false;
		}
	}

	#region Script
	void Awake () 
	{
		this.localButton = this.buttonData.getButton (this.buttonName);
		this.virtualButton = new VirtualButton (this.transform.localPosition, 200, Vector3.forward);
	}
	

	void Update () 
	{
		this.transform.localPosition = this.virtualButton.ConstraintMovement (this.transform.localPosition, 0f, 0.2f);
		this.GetComponent<Rigidbody> ().AddRelativeForce(this.virtualButton.ApplyRelativeSpring (this.transform.localPosition));
		
		this.buttonPressed ();
	}

	void OnDisable()
	{
		this.transform.localPosition = this.virtualButton.RestingPosition;
		this.GetComponent<Rigidbody>().velocity = Vector3.zero;
		this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
	}
	#endregion
}
