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

	private bool isPressed;
	private bool isLightOn;

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
		this.isLightOn = true;
		this.attrachedSwitch.GetComponent<RailroadSwitchController>().activateIndicators(true);
	}

	public void turnLightOff()
	{
		this.isLightOn = false;
		this.attrachedSwitch.GetComponent<RailroadSwitchController>().activateIndicators(false);
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
	#endregion

//	public float spring;
//	public float triggerDistance;
//	public float max_distance;
//	public float min_distance;
//	public GameObject pressedAddon;
//	
//	private bool is_pressed;
//	private Vector3 resting_position;
//	private Material childMaterial;
//
//	private void constraintMovement()
//	{
//		Vector3 _local_position = transform.localPosition;
//		_local_position.y = Mathf.Clamp (_local_position.y, this.min_distance, this.max_distance);
//		transform.localPosition = _local_position;
//	}
//	
//	private void appliySpring()
//	{
//		transform.GetComponent<Rigidbody> ().AddRelativeForce (-this.spring * (transform.localPosition - this.resting_position));
//	}
//	
//	private void checkTrigger()
//	{
//		if (!this.is_pressed) {
//			if (transform.localPosition.y < this.triggerDistance) {
//				this.buttonPressedBlur(1);
//				this.is_pressed = true;
//				transform.parent.GetComponent<BoundingBoxButtonController> ().changeDirectionIndex ();
//			}
//		} else if (this.is_pressed) {
//			if (transform.localPosition.y > this.triggerDistance) {
//				this.buttonPressedBlur(0);
//				this.is_pressed = false;
//			}
//		}
//	}
//	
//	private void buttonPressedBlur(float alpha)
//	{
//		childMaterial.color = new Color(childMaterial.color.r,childMaterial.color.g, childMaterial.color.b, alpha);
//	}
//
//
//	#region Script
//	void Start()
//	{
//		this.resting_position = transform.localPosition;
//		this.is_pressed = false;
//		childMaterial = pressedAddon.GetComponent<MeshRenderer>().material;
//	}
//	
//	void Update()
//	{
//		this.constraintMovement();
//		this.appliySpring ();
//		this.checkTrigger ();
//	}
//	#endregion
}
