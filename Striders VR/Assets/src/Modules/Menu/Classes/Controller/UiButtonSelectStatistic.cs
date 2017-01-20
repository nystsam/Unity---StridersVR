using UnityEngine;
using System.Collections;
using StridersVR.Domain;
using StridersVR.Domain.Menu;
using StridersVR.ScriptableObjects.Menu;

public class UiButtonSelectStatistic : MonoBehaviour {
		
	public TextMesh buttonText;

	public float triggerDistance;
	public float spring;
	public float min;
	public float max;
	
	private bool isPressed = false;
	
	private VirtualButton virtualButton;
	
	private Training currentTraining;


	public void SetTraining(Training newTraining)
	{
		currentTraining = newTraining;
		this.buttonText.text = currentTraining.Name;
	}

	private void buttonPressed ()
	{
		if (!this.isPressed && this.virtualButton.IsButtonPressed (this.transform.localPosition, this.triggerDistance)) 
		{
			this.isPressed = true;
		} 
		else if (this.isPressed && this.virtualButton.IsButtonReleased (this.transform.localPosition, this.triggerDistance)) 
		{	
			this.isPressed = false;
		}
	}
	
	#region Script
	void Start () 
	{
		this.virtualButton = new VirtualButton (this.transform.localPosition, spring, Vector3.forward);
	}
	
	
	void Update () 
	{
		this.transform.localPosition = this.virtualButton.ConstraintMovement (this.transform.localPosition, min, max);
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
