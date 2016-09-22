using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using StridersVR.Domain;

public class UIButtonResetController : MonoBehaviour, UIButtonActions {

	public GameObject buttonShape;
	public GameObject buttonText;

	private Material colorMain;
	private Material colorHover;
	private Material colorPressed;

	private string colorTextMain;
	private string colorTextHover;
	private string colorTextPressed;

	private bool isPressed = false;

	private VirtualButton buttonVr;

	private float triggerDistance = 0.075f;

	#region UIAction
	public void buttonHover(bool isHitting)
	{
		Color _textColor;
		
		if (isHitting) 
		{
			this.GetComponent<MeshRenderer>().material = this.colorHover;
			
			Color.TryParseHexString(this.colorTextHover, out _textColor);
			this.buttonText.GetComponent<Text>().color = _textColor;
		}
		else
		{
			this.GetComponent<MeshRenderer>().material = this.colorMain;
			
			Color.TryParseHexString(this.colorTextMain, out _textColor);
			this.buttonText.GetComponent<Text>().color = _textColor;
		}
	}
	#endregion

	public void buttonAction()
	{
		// Sacar un diaglo de Si o No

		Training _currentTraining;

		_currentTraining = GameObject.FindGameObjectWithTag("StaticUser").GetComponent<StaticUserController>().Training;
		Application.LoadLevel (_currentTraining.Name);		
	}

	public void buttonPressed ()
	{
		if (!this.isPressed && this.buttonVr.IsButtonPressed (-this.transform.localPosition, this.triggerDistance)) 
		{
			Color _textColor;
			
			this.isPressed = true;
			this.buttonShape.GetComponent<MeshRenderer>().material = this.colorPressed;
			
			Color.TryParseHexString(this.colorTextPressed, out _textColor);
			this.buttonText.GetComponent<Text>().color = _textColor;

			this.buttonAction();
		} 
		else if (this.isPressed && this.buttonVr.IsButtonReleased (-this.transform.localPosition, this.triggerDistance)) 
		{
			Color _textColor;
			
			this.isPressed = false;
			this.buttonShape.GetComponent<MeshRenderer>().material = this.colorMain;
			
			Color.TryParseHexString(this.colorTextMain, out _textColor);
			this.buttonText.GetComponent<Text>().color = _textColor;
		}
	}

	#region Script
	void Awake () 
	{
		this.buttonVr = new VirtualButton (this.transform.localPosition, 100, Vector3.forward);
		this.colorMain = Resources.Load ("Materials/MatUIMenuColor2", typeof(Material)) as Material;
		this.colorHover = Resources.Load ("Materials/MatTouch", typeof(Material)) as Material;
		this.colorPressed = Resources.Load ("Materials/MatMainColor", typeof(Material)) as Material;
		this.colorTextMain = "18CAE6FF";
		this.colorTextHover = "F25E21FF";
		this.colorTextPressed = "1A6B79FF";
	}

	void Update()
	{
		this.transform.localPosition = this.buttonVr.ConstraintMovement (this.transform.localPosition, -0.1f, 0f);
		this.GetComponent<Rigidbody> ().AddRelativeForce(this.buttonVr.ApplyRelativeSpring (this.transform.localPosition));

		this.buttonPressed ();
	}
	#endregion
}
