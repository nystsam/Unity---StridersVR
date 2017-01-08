using UnityEngine;
using System.Collections;
using StridersVR.Domain;

[RequireComponent(typeof (Rigidbody))]
[RequireComponent(typeof (SpringJoint))]
public class FinishModelButtonController : MonoBehaviour {

	private Transform gameButton;

	private GameObject gameController;

	private VirtualButton buttonVr;

	private float triggerDistance = 0.35f;
	private float timer = 0f;

	private bool isPressed;
	private bool isFinger;
	private bool timerStart = false;
	private bool timerDone = false;

	private TextMesh textMesh;

	private Material buttonMaterial;

	private string textColor;
	private string backgroundColor;

	private bool isRightFinger(GameObject other)
	{
		if (other.tag.Equals ("IndexRight"))
			return true;
		return false;
	}

	private void buttonAction()
	{
		this.gameController.GetComponent<PointManagerController> ().revealModel ();
	}

	private void buttonPressed ()
	{
		if (!this.isPressed && this.buttonVr.IsButtonPressed (-this.transform.localPosition, this.triggerDistance)) 
		{
			if(this.isFinger)
			{
				this.isPressed = true;
				this.buttonAction();
			}
		} 
		else if (this.isPressed && this.buttonVr.IsButtonReleased (-this.transform.localPosition, this.triggerDistance)) 
		{	
			this.isPressed = false;
		}
	}

	#region Button Decoration
	public void activeTimer()
	{
		this.timerStart = true;
	}

	private void destroyButton()
	{
		if(this.timerStart && !this.timerDone)
		{
			this.timer += Time.deltaTime;

			if(this.timer >= 1f)
				this.fadeIn();

			if(this.timer >= 2f)
			{
				this.timerDone = true;
				this.gameController.GetComponent<PointManagerController>().setFinishButtonStatus(true);

				GameObject.Destroy(this.gameObject);
			}
		}
	}

	private void fadeIn()
	{
		Color _text, _bg;

		Color.TryParseHexString(this.buttonMaterial.color.ToHexStringRGBA(), out _bg);
		Color.TryParseHexString(this.textMesh.color.ToHexStringRGBA(), out _text);

		_bg.a -= 0.000015f;
		_text.a -= 0.0075f;

		this.buttonMaterial.color = _bg;
		this.textMesh.color = _text;
	}

	private void resetColor()
	{
		Color _text, _bg;

		Color.TryParseHexString(this.backgroundColor, out _bg);
		Color.TryParseHexString(this.textColor, out _text);

		this.buttonMaterial.color = _bg;
		this.textMesh.color = _text;
	}

	private void setChildComponents()
	{
		this.gameButton = this.transform.GetChild (0);
		this.buttonMaterial = this.gameButton.GetComponent<MeshRenderer>().material;
		this.textMesh = this.gameButton.GetChild(0).GetComponent<TextMesh>();

		this.textColor = this.textMesh.color.ToHexStringRGBA();
		this.backgroundColor = this.buttonMaterial.color.ToHexStringRGBA();
	}
	#endregion

	#region Script
	void Awake () 
	{
		this.buttonVr = new VirtualButton (this.transform.localPosition, 500, Vector3.forward);
		this.gameController = GameObject.FindGameObjectWithTag("GameController");

		this.setChildComponents();

		this.GetComponent<SpringJoint> ().connectedAnchor = this.gameButton.position;
		this.isPressed = false;
		this.isFinger = false;
	}

	void Update () 
	{
		this.transform.localPosition = this.buttonVr.ConstraintMovement (this.transform.localPosition, -0.5f, 0);
		this.GetComponent<Rigidbody> ().AddRelativeForce(this.buttonVr.ApplyRelativeSpring (this.transform.localPosition));

		this.buttonPressed ();
	}

	void LateUpdate()
	{
		this.destroyButton();
	}

	void OnCollisionEnter(Collision other)
	{
		this.isFinger = this.isRightFinger (other.gameObject);

		if(this.isFinger && this.timerStart)
		{
			this.timerStart = false;
			this.timer = 0f;
			this.resetColor();
		}
	}

	void OnCollisionExit(Collision other)
	{
		this.isFinger = this.isRightFinger (other.gameObject);

		if(this.isFinger && !this.timerStart)
		{
			this.timerStart = true;
		}
	}
	#endregion
}
