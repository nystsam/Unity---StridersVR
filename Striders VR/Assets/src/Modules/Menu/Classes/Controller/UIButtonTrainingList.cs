using UnityEngine;
using System.Collections.Generic;
using StridersVR.Modules.Menu.Logic;
using StridersVR.Domain;
using StridersVR.Domain.Menu;

public class UIButtonTrainingList : MonoBehaviour {

	public GameObject trainingName;
	public GameObject trainingImage;

	private MenuTrainingList trainingList;

	private Training currentTrain;

	private float triggerDistance = 0.22f;

	private bool isPressed = false;

	private MenuButton localButton;
	
	private VirtualButton virtualButton;


	public void nextTraining()
	{
		this.currentTrain = this.trainingList.getNextTraining ();
		this.setValues();
	}

	public void previousTraining()
	{
		this.currentTrain = this.trainingList.getPreviousTraining ();
		this.setValues();
	}

	private void setValues()
	{
		this.trainingName.GetComponent<TextMesh> ().text = this.currentTrain.Name;
		this.trainingImage.GetComponent<MeshRenderer>().material = Resources.Load(this.currentTrain.Image, typeof(Material)) as Material;
	}

	/* Setting Training to Static User */
	private void selectTraining()
	{
		GameObject.FindGameObjectWithTag ("StaticUser").GetComponent<StaticUserController> ().setTraining (this.currentTrain);
	}
	/* ********************* */

	private void buttonPressed ()
	{
		if (!this.isPressed && this.virtualButton.IsButtonPressed (this.transform.localPosition, this.triggerDistance)) 
		{
			this.isPressed = true;
			this.localButton.buttonAction();
			this.selectTraining();
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
		this.localButton = new MenuButtonDifficulty ();
		this.trainingList = new MenuTrainingList ();
		this.currentTrain = this.trainingList.getCurrentTraining ();
		this.setValues();
	}

	void Update () 
	{
		this.transform.localPosition = this.virtualButton.ConstraintMovement (this.transform.localPosition, 0f, 0.25f);
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
