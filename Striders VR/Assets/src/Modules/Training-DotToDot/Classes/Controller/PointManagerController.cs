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
	private bool isFinishinModel = false;
	// Solo para Hard
	private bool exampleConstraint = true;

	private Vector3 containerPostion;

	private ITouchBoard touchBoard;

	private float timeShowing = 0;

	private float timeComplete = 0;

	private ActivityDotToDot currentActivity;

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
		this.allowShowModel = false;
		this.localPointManager = null;
	}

	public void revealModel()
	{
		GameObject _modelController = GameObject.FindGameObjectWithTag("Respawn");

		if(_modelController.transform.GetChild(0).GetComponent<ModelController>() != null && !this.isFinishinModel)
		{
			this.allowShowModel = false;
			this.isFinishinModel = true;
			_modelController.transform.GetChild(0).GetComponent<ModelController>().revealOriginalModel();
		}
	}

	public void finishModel()
	{
		bool _result = this.localPointManager.finishModel();

		this.localPointManager.StartTiming = false;
		this.currentActivity.setTimeComplete(this.timeComplete);
		this.currentActivity.IsCorrect = _result;
		this.currentActivity.Revelations = this.localPointManager.ModelRevealCount;
		StatisticsDotToDotController.Current.addNewResult(this.currentActivity);
		this.verificationStarted = true;
		this.verifer.GetComponent<VerifierController>().setAnimation(_result);
		this.clearChilds();

		this.scoreController.GetComponent<ScoreDotsController>().setScore(_result);
		this.localPointManager = new PointManager(this.pointsContainer);
		this.currentActivity = new ActivityDotToDot();
	}
	
	public void setModel(Model newModel)
	{
		this.requesttModel = false;
		this.allowShowModel = true;
		this.exampleConstraint = true;
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

	public void showingModel(bool val)
	{
		if(this.localPointManager != null)
		{
			this.localPointManager.IsShowingExample = val;
			
			if(!val)
			{
				this.currentActivity.addTimeRevelation(this.timeShowing);
				this.timeShowing = 0;
			}
		}
	}

	public void resetCurrentStripe()
	{
		if(!this.scoreController.GetComponent<ScoreDotsController>().IsGameTimerEnd)
			this.localPointManager.resetCurrentStripe();
	}

	public void addRevealCount()
	{
		this.localPointManager.addModelRevealCount();
		this.scoreController.GetComponent<ScoreDotsController>().addReveal();

		if(GameObject.FindGameObjectWithTag ("StaticUser").GetComponent<StaticUserController> ()
		   .Training.Difficulty.Equals("Hard"))
		{
			if(this.localPointManager.ModelRevealCount >= 2)
			{
				this.exampleConstraint = false;
			}
		}
	}
	
	public int getRevealCount()
	{
		return this.localPointManager.ModelRevealCount;
	}

	public bool getExampleStatus()
	{
		return this.exampleConstraint;
	}

	public void hidePointsCointainer()
	{
		this.pointsContainer.transform.localPosition = new Vector3(0,-100,0);
	}

	public void showPointsCointainer()
	{
		this.pointsContainer.transform.localPosition = this.containerPostion;
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
			this.isFinishinModel = false;
			_modelController.transform.GetChild(0).GetComponent<ModelController>().clearCurrentModel();
		}
		
	}
	#endregion

	#region Script
	void Awake () 
	{
		this.localPointManager = new PointManager (this.pointsContainer);
		this.currentActivity = new ActivityDotToDot();
		this.touchBoard = GameObject.FindGameObjectWithTag ("TouchBoard").GetComponent<TouchBoardIndexController> ();
		this.scoreController = GameObject.FindGameObjectWithTag("Finish");
		this.containerPostion = this.pointsContainer.transform.localPosition;
	}

	void Update()
	{
		if(!this.scoreController.GetComponent<ScoreDotsController>().IsGameTimerEnd)
		{
			this.placePoint ();
			this.verification();
			
			if(this.localPointManager.IsShowingExample)
			{
				this.timeShowing += Time.deltaTime;
			}
			
			if(this.localPointManager.StartTiming)
			{
				this.timeComplete += Time.deltaTime;
			}		
		}
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
