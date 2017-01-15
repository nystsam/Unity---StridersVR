using UnityEngine;
using System.Collections;
using StridersVR.Domain;
using StridersVR.Domain.Menu;

public class UIButtonStartButton : MonoBehaviour {

	private GameObject currentUser;

	private float triggerDistance = 0.15f;
	
	private bool isPressed = false;
	
	private VirtualButton virtualButton;


	private void startGame()
	{
		//FIXME Hacer un timer o algo antes de empezar
		Application.LoadLevel (this.currentUser.GetComponent<StaticUserController>().Training.Name);
	}

	private void buttonPressed ()
	{
		if (!this.isPressed && this.virtualButton.IsButtonPressed (this.transform.localPosition, this.triggerDistance)) 
		{
			this.isPressed = true;
			if(this.currentUser.GetComponent<StaticUserController>().isValidGame())
			{
				this.currentUser.GetComponent<StaticUserController>().gameSelected();
				this.startGame();
			}

		} 
		else if (this.isPressed && this.virtualButton.IsButtonReleased (this.transform.localPosition, this.triggerDistance)) 
		{	
			this.isPressed = false;
		}
	}

	#region Script
	void Awake () 
	{
		this.virtualButton = new VirtualButton(this.transform.localPosition, 200, Vector3.forward);
	}

	void Start()
	{
		this.currentUser = GameObject.FindGameObjectWithTag("StaticUser");
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
