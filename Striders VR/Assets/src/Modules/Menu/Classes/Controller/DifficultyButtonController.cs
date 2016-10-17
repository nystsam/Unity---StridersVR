using UnityEngine;
using System.Collections;
using StridersVR.Domain.Menu;
using StridersVR.Domain;

public class DifficultyButtonController : MonoBehaviour {

	public GameObject selectionController;

	public string difficulty;

	public GameObject imageButton;


	private DifficultyButton localButton;

	private float triggerDistance = 0.175f;
	
	private bool isPressed = false;

	private VirtualButton virtualButton;

	//FIXME Enviar info al static user de la dificultad seleccionada

	private void buttonPressed ()
	{
		if (!this.isPressed && this.virtualButton.IsButtonPressed (this.transform.localPosition, this.triggerDistance)) 
		{
			this.isPressed = true;
			this.selectionController.GetComponent<DifficultySelectionController>().selectDifficulty(this.localButton);
		} 
		else if (this.isPressed && this.virtualButton.IsButtonReleased (this.transform.localPosition, this.triggerDistance)) 
		{	
			this.isPressed = false;
		}
	}

	#region Script
	void Awake () 
	{
		this.virtualButton = new VirtualButton (this.transform.localPosition, 300, Vector3.forward);
		this.localButton = new DifficultyButton (this.gameObject, this.difficulty);
	}
	

	void Update () 
	{
		this.transform.localPosition = this.virtualButton.ConstraintMovement (this.transform.localPosition, 0f, 0.2f);
		this.GetComponent<Rigidbody> ().AddRelativeForce(this.virtualButton.ApplyRelativeSpring (this.transform.localPosition));
		
		this.buttonPressed ();
	}
	#endregion
}
