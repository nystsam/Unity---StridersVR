using UnityEngine;
using System.Collections;
using StridersVR.Domain.SpeedPack;
using StridersVR.Domain;

public class PlaceButtonController : MonoBehaviour {

	private GameObject suitcaseContainer;
	private GameObject hand;

	private Spot currentSpot;

	private Material hoverButtonColor;
	private Material originalColor;
	private Material disableColor;

	private bool isHandHitting;
	private bool colorChange;
	private bool isDisable = true;
	private bool isPressed = false;

	private VirtualButton buttonVr;

	private float triggerDistance = 0.075f;

	private bool isHandController(Collider other)
	{
		if (other.GetComponentInParent<HandController> ())
			return true;
		return false;
	}

	private void placeItem()
	{
		if (currentSpot != null) 
		{
			this.buttonActivation(false);
			this.suitcaseContainer.GetComponent<SuitcaseController>().placePlayerItem(this.currentSpot);
		}
	}

	private void rayHittingColor()
	{
		if (this.colorChange && !this.isDisable) 
		{
			if (this.isHandHitting) 
			{
				this.transform.GetChild(0).GetComponent<MeshRenderer>().material = this.hoverButtonColor;
				this.colorChange = false;
			}
			else
			{
				this.transform.GetChild(0).GetComponent<MeshRenderer>().material = this.originalColor;
				this.colorChange = false;
			}
		}
	}

	public void hoverColor(bool hitting)
	{
		this.colorChange = true;
		if (hitting) 
		{
			this.isHandHitting = true;	
		} 
		else 
		{
			this.isHandHitting = false;
		}
	}

	public void buttonActivation(bool activation)
	{
		Color _textColor;

		this.GetComponent<BoxCollider>().enabled = activation;
		if (activation) 
		{
			// color de letras A2EEF2FF
			Color.TryParseHexString("A2EEF2FF", out _textColor);
			this.transform.GetChild (0).GetComponent<MeshRenderer> ().material = this.originalColor;
			this.transform.GetChild(0).GetChild(0).GetComponent<TextMesh>().color = _textColor;
			this.isDisable = false;
		} 
		else 
		{
			// color de letras 2B2B2BFF
			Color.TryParseHexString("2B2B2BFF", out _textColor);
			this.transform.GetChild(0).GetComponent<MeshRenderer>().material = disableColor;
			this.transform.GetChild(0).GetChild(0).GetComponent<TextMesh>().color = _textColor;
			this.isDisable = true;
		}
	}

	#region Script
	void Awake () 
	{
		this.buttonVr = new VirtualButton (this.transform.localPosition, 100, Vector3.forward);

		this.originalColor = this.transform.GetChild(0).GetComponent<MeshRenderer> ().material;
		this.hoverButtonColor = Resources.Load("Materials/Training-SpeedPack/MatHover", typeof(Material)) as Material;
		this.disableColor = Resources.Load("Materials/Training-SpeedPack/MatDisableColor", typeof(Material)) as Material;
		this.suitcaseContainer = GameObject.FindGameObjectWithTag("Container");

		this.buttonActivation (false);

	}

	void Update () 
	{
		this.rayHittingColor ();
		this.transform.localPosition = this.buttonVr.ConstraintMovement (this.transform.localPosition, -0.1f, 0f);
		this.GetComponent<Rigidbody> ().AddRelativeForce(this.buttonVr.ApplyRelativeSpring (this.transform.localPosition));

		if (!this.isPressed && this.buttonVr.IsButtonPressed (-this.transform.localPosition, this.triggerDistance)) 
		{
			this.isPressed = true;
			if(hand != null)
			{
				this.placeItem();
			}

		} 
		else if (this.isPressed && this.buttonVr.IsButtonReleased (-this.transform.localPosition, this.triggerDistance)) 
		{
			this.isPressed = false;
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if (this.isHandController (other.collider)) 
		{
			this.hand = other.gameObject;
			//this.placeItem();
		}
	}

	void OnCollisionExit(Collision other)
	{
		if (this.isHandController (other.collider)) 
		{
			this.hand = null;
		}
	}
	#endregion

	#region Properties
	public Spot CurrentSpot
	{
		set { this.currentSpot = value; }
	}
	public bool IsDisabled
	{
		get { return this.isDisable; }
	}
	#endregion
}
