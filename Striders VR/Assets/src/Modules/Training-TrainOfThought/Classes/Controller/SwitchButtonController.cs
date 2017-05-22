using UnityEngine;
using System.Collections;
using StridersVR.Domain;

[RequireComponent(typeof (Rigidbody))]
[RequireComponent(typeof (SpringJoint))]
public class SwitchButtonController : MonoBehaviour {

	/*
	public Light buttonLight;
	public TextMesh buttonNumber;
	*/
	public TextMesh buttonNumber;
	public Material current;
	public Material hover;

	public MeshRenderer background;

	public float triggerDistance;
	public float spring;
	public float min;
	public float max;

	private GameObject attrachedSwitch;

	private bool isPressed = false;
	private bool collisionDetected = false;

	private bool alreadyPressed = false;

	private VirtualButton buttonVr;

	private Vector3 currentPosition;

	private float startingZ;
	private float startingX;

	private AudioSource buttonSound;

	public void setAttachedSwitch(GameObject newSwitch, int number)
	{
		this.attrachedSwitch = newSwitch;
		this.buttonNumber.text = number.ToString();
		Vector3 _currentRotation, _newRotation;
		_currentRotation = this.attrachedSwitch.GetComponent<RailroadSwitchController>().switchDirection.transform.localRotation.eulerAngles;
		_newRotation = new Vector3(0,_currentRotation.y, 0);
		this.transform.localRotation = Quaternion.Euler(_newRotation);
	}


	#region Decoration
	public void EnableButton()
	{
		this.GetComponent<Rigidbody>().isKinematic = false;
		this.background.material = this.hover;
	}
	
	public void DisableButton()
	{
		this.GetComponent<Rigidbody>().isKinematic = true;
		this.background.material = this.current;
		this.CancelForce();
	}

	private void CancelForce()
	{
		this.transform.localPosition = this.buttonVr.RestingPosition;
		this.GetComponent<Rigidbody>().velocity = Vector3.zero;
		this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
	}
	/*
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
	*/
	#endregion
	private void buttonAction()
	{
		if(!this.alreadyPressed)
		{
			this.alreadyPressed = true;
			if(this.buttonSound != null)
			{
				this.buttonSound.Play();
			}
			this.attrachedSwitch.GetComponent<RailroadSwitchController>().changeDirectionIndex();
		}
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
		this.GetComponent<Rigidbody>().isKinematic = true;
		this.buttonSound = this.GetComponent<AudioSource>();
	}

	void Update()
	{
		this.currentPosition = this.transform.localPosition;
		this.currentPosition.z = this.startingZ;
		this.currentPosition.x = this.startingX;
		this.transform.localPosition = this.buttonVr.ConstraintMovement (this.currentPosition, min, max);
		this.GetComponent<Rigidbody> ().AddRelativeForce(this.buttonVr.ApplyRelativeSpring (this.transform.localPosition));

		this.buttonPressed ();

		if(this.alreadyPressed)
		{
			if(this.transform.localPosition == this.buttonVr.RestingPosition)
			{
				this.alreadyPressed = false;
			}
		}

		if(attrachedSwitch != null)
		{
			Vector3 _currentRotation, _newRotation;
			_currentRotation = this.attrachedSwitch.GetComponent<RailroadSwitchController>().switchDirection.transform.localRotation.eulerAngles;
			_newRotation = new Vector3(0,_currentRotation.y, 0);
			this.transform.localRotation = Quaternion.Euler(_newRotation);
		}

		/*
		if(SwitchesPanelController.Current.IsControlMoving())
		{
			this.GetComponent<SpringJoint>().connectedAnchor = this.transform.position;
		}
		*/
	}

	void OnCollisionEnter(Collision other)
	{
		if(!other.collider.tag.Equals("IndexUI") && !other.collider.tag.Equals("IndexRight"))
		{
			Physics.IgnoreCollision(this.GetComponent<BoxCollider>(), other.collider);
		}

		if(other.collider.tag.Equals("IndexUI") || other.collider.tag.Equals("IndexRight"))
		{
			this.EnableButton();
			this.collisionDetected = true;
		}
	}

	void OnCollisionExit(Collision other)
	{	
		if(other.collider.tag.Equals("IndexUI") || other.collider.tag.Equals("IndexRight"))
		{
			this.DisableButton();
		}
		else if(this.collisionDetected)
		{
			this.collisionDetected = false;
			this.DisableButton();
		}
	}
	#endregion
}
