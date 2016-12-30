using UnityEngine;
using System.Collections;
using StridersVR.Domain.DotToDot;
using StridersVR.Domain;

public class PointManagerController : MonoBehaviour {

	public GameObject pointsContainer;
	public GameObject verifer;

	private GameObject scoreController;

	private PointManager localPointManager;

	private bool isPlacingPoint = false;
	private bool requesttModel = false;
	private bool verificationStarted = false;
	private bool allowShowFinishButton = true;
	private bool allowShowModel = false;

	private ITouchBoard touchBoard;


	#region Point & Stripe controller
	public bool setPoint(Point newPoint)
	{
		if (!this.localPointManager.setTouchingPoint (newPoint)) 
		{
			if(this.localPointManager.isAvailablePoint(newPoint))
			{
				this.isPlacingPoint = true;
				this.touchBoard.startAction();
				return false;
			}
		}

		return true;
	}

	public bool isSamePoint(Point point)
	{
		if(point == this.localPointManager.CurrentPoint)
		{
			return true;
		}

		return false;
	}

	public void cancelCurrentStripe()
	{
		this.localPointManager.cancelCurrentStripe ();
	}

	public void cancelPlacingPoint()
	{
		if(this.isPlacingPoint)
		{
			this.isPlacingPoint = false;
			this.touchBoard.cancelAction();
		}
	}

	private void placePoint()
	{
		if (this.isPlacingPoint) 
		{
			if(this.touchBoard.actionComplete())
			{
				this.isPlacingPoint = false;
				this.localPointManager.placeStripe();
			}
		}
	}
	#endregion

	#region Model controller
	public void gameOver()
	{
		this.clearChilds();
		this.localPointManager = null;
	}

	public void revealModel()
	{
		GameObject _modelController = GameObject.FindGameObjectWithTag("Respawn");

		if(_modelController.transform.GetChild(0).GetComponent<ModelController>() != null)
		{
			this.allowShowModel = false;
			
			_modelController.transform.GetChild(0).GetComponent<ModelController>().revealOriginalModel();
		}
	}

	public void finishModel()
	{
		bool _result = this.localPointManager.finishModel();

		this.verificationStarted = true;
		this.verifer.GetComponent<VerifierController>().setAnimation(_result);
		this.clearChilds();

		this.scoreController.GetComponent<ScoreDotsController>().setScore(_result);
		this.localPointManager = new PointManager(this.pointsContainer);
	}
	
	public void setModel(Model newModel)
	{
		this.requesttModel = false;
		this.allowShowModel = true;
		this.localPointManager.CurrentModel = newModel;
		this.localPointManager.instantiatePoints ();
		this.scoreController.GetComponent<ScoreDotsController>().newModel();
	}

	#region Hand-Methods
	public void setFinishButtonStatus(bool status)
	{
		this.allowShowFinishButton = status;
	}

	public bool getFinishButtonStatus()
	{
		return this.allowShowFinishButton;
	}

	public bool getModelStatus()
	{
		return this.allowShowModel;
	}

	public void addRevealCount()
	{
		this.localPointManager.addModelRevealCount();
		this.scoreController.GetComponent<ScoreDotsController>().addReveal();
	}

	public int getRevealCount()
	{
		return this.localPointManager.ModelRevealCount;
	}
	#endregion

	private void clearChilds()
	{
		foreach(Transform child in this.pointsContainer.transform)
		{
			GameObject.Destroy(child.gameObject);
		}
	}

	private void verification()
	{
		if(this.verificationStarted)
		{
			if(this.verifer.GetComponent<VerifierController>().IsVerificationDone)
			{
				this.verificationStarted = false;
				this.verifer.GetComponent<VerifierController>().resetAnimation();

				StartCoroutine(resetModel());
			}
		}
	}

	private IEnumerator resetModel()
	{
		yield return new WaitForSeconds(1.5f);
		GameObject _modelController = GameObject.FindGameObjectWithTag("Respawn");
		
		if(_modelController.transform.GetChild(0).GetComponent<ModelController>() != null)
		{
			_modelController.transform.GetChild(0).GetComponent<ModelController>().clearCurrentModel();
		}
		
	}
	#endregion

	#region Script
	void Awake () 
	{
		this.localPointManager = new PointManager (this.pointsContainer);
		this.touchBoard = GameObject.FindGameObjectWithTag ("TouchBoard").GetComponent<TouchBoardIndexController> ();
		this.scoreController = GameObject.FindGameObjectWithTag("Finish");
	}

	void Update()
	{
		this.placePoint ();
		this.verification();
	}
	#endregion

	#region Properties
	public bool RequestModel
	{
		get { return this.requesttModel; }
		set { this.requesttModel = value; }
	}
	#endregion
}
