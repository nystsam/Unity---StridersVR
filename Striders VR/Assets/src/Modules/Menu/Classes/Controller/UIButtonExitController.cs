using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using StridersVR.Domain;
using StridersVR.Domain.Menu;

public class UIButtonExitController : MonoBehaviour {

	public GameObject buttonShape;
	public GameObject buttonText;

	public float triggerDistance;
	public float spring;
	public float min;
	public float max;

	private GameObject UIGameController;

	private Material colorMain;
	private Material colorPressed;
	
	private string colorTextMain;
	private string colorTextPressed;
	
	private bool isPressed = false;
	
	private VirtualButton buttonVr;


	private void buttonAction()
	{
		ToolButton _reset = new ToolButtonExit();
		
		this.UIGameController.transform.FindChild("ToolsPanelUI").GetComponent<UIMenuOptions>().callConfimation(_reset);			
	}
	
	private void buttonPressed ()
	{
		if (!this.isPressed && this.buttonVr.IsButtonPressed (-this.transform.localPosition, this.triggerDistance)) 
		{
			this.isPressed = true;
			//this.changeColor(true);
			this.buttonAction();
		} 
		else if (this.isPressed && this.buttonVr.IsButtonReleased (-this.transform.localPosition, this.triggerDistance)) 
		{
			this.isPressed = false;
			//this.changeColor(false);
		}
	}

	#region Button Decoration
	private void changeColor(bool val)
	{
		Color _textColor;
		
		if(val)
		{
			this.buttonShape.GetComponent<MeshRenderer>().material = this.colorPressed;
			
			Color.TryParseHexString(this.colorTextPressed, out _textColor);
			this.buttonText.GetComponent<Text>().color = _textColor;
		}
		else
		{
			this.buttonShape.GetComponent<MeshRenderer>().material = this.colorMain;
			
			Color.TryParseHexString(this.colorTextMain, out _textColor);
			this.buttonText.GetComponent<Text>().color = _textColor;
		}
		
	}
	#endregion

	#region Script
	void Awake () 
	{
		this.buttonVr = new VirtualButton (this.transform.localPosition, spring, Vector3.forward);
		this.colorMain = Resources.Load ("Materials/MatUIMenuColor2", typeof(Material)) as Material;
		this.colorPressed = Resources.Load ("Materials/MatMainColor", typeof(Material)) as Material;
		this.colorTextMain = "18CAE6FF";
		this.colorTextPressed = "1A6B79FF";
		this.UIGameController = GameObject.FindGameObjectWithTag ("PlayerPanelButtons");
	}

	void Update()
	{
		this.transform.localPosition = this.buttonVr.ConstraintMovement (this.transform.localPosition, min, max);
		this.GetComponent<Rigidbody> ().AddRelativeForce(this.buttonVr.ApplyRelativeSpring (this.transform.localPosition));
		
		this.buttonPressed ();
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.collider.GetComponentInParent<HandModel>() != null)
		{
			this.changeColor(true);
		}
	}
	
	void OnCollisionExit(Collision other)
	{
		if(other.collider.GetComponentInParent<HandModel>() != null)
		{
			this.changeColor(false);
		}
	}
	
	void OnDisable()
	{
		this.transform.localPosition = this.buttonVr.RestingPosition;
		this.GetComponent<Rigidbody>().velocity = Vector3.zero;
		this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		this.changeColor(false);
	}
	#endregion
}
