using UnityEngine;
using System.Collections;
using StridersVR.Domain;

[RequireComponent(typeof (Rigidbody))]
[RequireComponent(typeof (SpringJoint))]
public class SwitchButtonController : MonoBehaviour {

	public Light buttonLight;
	public TextMesh buttonNumber;

	public float triggerDistance;
	public float spring;
	public float min;
	public float max;

	private GameObject attrachedSwitch;

	private bool isPressed = false;
	private bool isLightOn = false;

	private VirtualButton buttonVr;

	private Vector3 currentPosition;

	private float startingZ;
	private float startingX;


	public void setAttachedSwitch(GameObject newSwitch, int switchNumber)
	{
		this.attrachedSwitch = newSwitch;
		this.buttonNumber.text = switchNumber.ToString();
	}


	#region Decoration
	public void turnLightOn()
	{
		if(!this.isLightOn)
		{
			this.isLightOn = true;
			this.attrachedSwitch.GetComponent<RailroadSwitchController>().activateIndicators(true);
		}
	}

	public void turnLightOff()
	{
		if(this.isLightOn)
		{
			this.isLightOn = false;
			this.attrachedSwitch.GetComponent<RailroadSwitchController>().activateIndicators(false);
		}
	}

	private void setLight()
	{
		if(this.isLightOn)
		{
			if(this.buttonLight.intensity < 8)
				this.buttonLight.intensity += 0.4f;
		}
		else
			if(this.buttonLight.intensity > 0)
				this.buttonLight.intensity -= 0.4f;
	}
	#endregion
	private void buttonAction()
	{
		this.attrachedSwitch.GetComponent<RailroadSwitchController>().changeDirectionIndex();
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
	void Start()
	{
		this.buttonVr = new VirtualButton (this.transform.localPosition, spring, Vector3.up);
		this.startingZ = this.transform.localPosition.z;
		this.startingX = this.transform.localPosition.x;
		this.GetComponent<SpringJoint>().connectedAnchor = this.transform.position;
	}

	void Update()
	{
		this.currentPosition = this.transform.localPosition;
		this.currentPosition.z = this.startingZ;
		this.currentPosition.x = this.startingX;
		this.transform.localPosition = this.buttonVr.ConstraintMovement (this.currentPosition, min, max);
		this.GetComponent<Rigidbody> ().AddRelativeForce(this.buttonVr.ApplyRelativeSpring (this.transform.localPosition));

		this.buttonPressed ();
		this.setLight();

		if(SwitchesPanelController.Current.IsControlMoving())
		{
			this.GetComponent<SpringJoint>().connectedAnchor = this.transform.position;
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if(!other.collider.tag.Equals("IndexUI") && !other.collider.tag.Equals("IndexRight"))
		{
			Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), other.collider);
		}
	}
	#endregion
}
