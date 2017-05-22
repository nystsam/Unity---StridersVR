using UnityEngine;
using System.Collections;
using Leap;

public class HandModelExampleController : MonoBehaviour {

	private GameObject model;

	private GameObject gameController;

	private HandModel leftHand;

	private bool isLeftHand = false;
	private bool showingModel = false;
	private bool allowToShow = true;
	private bool isHardDifficulty = false;

	private Animator anim;

	Vector3 velocity;

	private void resetModel()
	{
		this.showingModel = false;
		this.gameController.GetComponent<PointManagerController>().showingModel(false);
		this.gameController.GetComponent<PointManagerController>().showPointsCointainer();
		this.model.transform.position = new Vector3(0,-10,0);
		this.model.transform.localScale = new Vector3(1,1,1);
		//this.activateAnimation(false);
	}

	private void activateAnimation(bool val)
	{
		int _animVariable = Animator.StringToHash("AllowRotateModel");
		
		this.anim.SetBool(_animVariable, val);
	}

	private void showModel()
	{		
		if(this.isHardDifficulty)
			this.allowToShow = this.gameController.GetComponent<PointManagerController>().getExampleStatus();

		if(this.leftHand.palm.up.y < -0.6f && !this.showingModel && this.allowToShow)
		{
			this.showingModel = true;
			this.model.transform.position = this.leftHand.palm.position + new Vector3(0,0.75f,0);
			this.model.transform.localScale = new Vector3(0.6f,0.6f,0.6f);
			//this.activateAnimation(true);
			this.gameController.GetComponent<PointManagerController>().showingModel(true);
			this.gameController.GetComponent<PointManagerController>().resetCurrentStripe();
			this.gameController.GetComponent<PointManagerController>().hidePointsCointainer();
			this.gameController.GetComponent<PointManagerController>().addRevealCount();
		}
		else if(this.leftHand.palm.up.y > -0.6f && this.showingModel)
		{
			this.resetModel();
		}
		
		if(this.showingModel)
		{
			this.model.transform.position = Vector3.SmoothDamp(this.model.transform.position, 
			                                                   this.leftHand.palm.position + new Vector3(0,0.75f,0), 
			                                                   ref this.velocity, 
			                                                   0.3f);

			Vector3 _direction = (CameraUITools.Current.transform.position - this.model.transform.position).normalized;
			Quaternion _lookDirection = Quaternion.LookRotation(_direction);
			Quaternion _newRotationValue = Quaternion.Slerp(this.model.transform.rotation, _lookDirection, 
			                                                 Time.deltaTime * 5);
			Vector3 _newRotation = new Vector3(0,_newRotationValue.eulerAngles.y, 0);
			this.model.transform.rotation = Quaternion.Euler(_newRotation);
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
			this.anim = this.model.GetComponentInChildren<Animator>();

			if(GameObject.FindGameObjectWithTag ("StaticUser").GetComponent<StaticUserController> ()
			   .Training.Difficulty.Equals("Hard"))
			{
				this.isHardDifficulty = true;
			}
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

	void OnDestroy()
	{
		if(this.isLeftHand && this.gameObject != null && this.model != null)
		{
			if(this.gameController != null)
			{
				this.gameController.GetComponent<PointManagerController>().resetCurrentStripe();
				if(this.showingModel)
					this.resetModel();	
			}

		}
	}
	#endregion

}
