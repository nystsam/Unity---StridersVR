using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using StridersVR.Modules.Menu.Logic;
using StridersVR.Domain;
using StridersVR.Domain.Menu;

public class UIButtonTrainingList : MonoBehaviour {

	public GameObject trainingName;

	private MenuTrainingList trainingList;

	private Training currentTrain;

	private float triggerDistance = 0.15f;

	private bool isPressed = false;

	private MenuButton localButton;
	
	private VirtualButton virtualButton;

	//FIXME Enviar info al static user del entrenamiento seleccionado

	public void nextTraining()
	{
		this.currentTrain = this.trainingList.getNextTraining ();

		this.trainingName.GetComponent<Text> ().text = this.currentTrain.Name;
	}

	public void previousTraining()
	{
		this.currentTrain = this.trainingList.getPreviousTraining ();
		
		this.trainingName.GetComponent<Text> ().text = this.currentTrain.Name;
	}

	private void buttonPressed ()
	{
		if (!this.isPressed && this.virtualButton.IsButtonPressed (this.transform.localPosition, this.triggerDistance)) 
		{
			this.isPressed = true;
			this.localButton.buttonAction();
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

		this.trainingName.GetComponent<Text> ().text = this.currentTrain.Name;
	}

	void Update () 
	{
		this.transform.localPosition = this.virtualButton.ConstraintMovement (this.transform.localPosition, 0f, 0.2f);
		this.GetComponent<Rigidbody> ().AddRelativeForce(this.virtualButton.ApplyRelativeSpring (this.transform.localPosition));

		this.buttonPressed ();
	}
	#endregion
}
