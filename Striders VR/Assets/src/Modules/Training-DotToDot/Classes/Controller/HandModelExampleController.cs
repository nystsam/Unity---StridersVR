using UnityEngine;
using System.Collections;
using Leap;

public class HandModelExampleController : MonoBehaviour {

	private GameObject model;

	private GameObject gameController;

	private HandModel leftHand;

	private bool isLeftHand = false;
	private bool showingModel = false;

	Vector3 velocity;

	private void resetModel()
	{
		this.showingModel = false;
		this.model.transform.position = new Vector3(0,-10,0);
		this.model.transform.localScale = new Vector3(1,1,1);
	}

	private void showModel()
	{
		// Conocer dificultad, obtener cantidad de revelaciones (1) y activar bool que no permita las siguientes condiciones
		if(this.leftHand.palm.up.y < -0.6f && !this.showingModel)
		{
			this.showingModel = true;
			this.model.transform.position = this.leftHand.palm.position + new Vector3(0,0.75f,0);
			this.model.transform.localScale = new Vector3(0.4f,0.4f,0.4f);
			this.gameController.GetComponent<PointManagerController>().addRevealCount();
		
		}
		else if(this.leftHand.palm.up.y > -0.6f)
		{
			this.resetModel();
		}
		
		if(this.showingModel)
		{
			this.model.transform.position = Vector3.SmoothDamp(this.model.transform.position, 
			                                                   this.leftHand.palm.position + new Vector3(0,0.75f,0), 
			                                                   ref this.velocity, 
			                                                   0.3f);
		}
	}

	#region Script
	void Start () 
	{
		if(this.GetComponent<HandModel>().GetLeapHand().IsLeft)
		{
			this.leftHand = this.GetComponent<HandModel>();
			this.isLeftHand = true;
			this.model = GameObject.FindGameObjectWithTag("Respawn");
			this.gameController = GameObject.FindGameObjectWithTag("GameController");
			this.velocity = new Vector3(6,6,6);

			//Conocer la dificultad para limitar a 1
		}
	}

	void Update () 
	{
		if(this.isLeftHand)
		{
			if(this.gameController.GetComponent<PointManagerController>().getModelStatus())
			{
				this.showModel();
			}
			else if(!this.gameController.GetComponent<PointManagerController>().getModelStatus())
			{
				this.resetModel();
			}
		}
	}
	#endregion

}
