using UnityEngine;
using System.Collections;
using StridersVR.Domain.DotToDot;
using StridersVR.Domain;

public class PointManagerController : MonoBehaviour {

	public GameObject pointsContainer;
	public GameObject verifer;

	private PointManager localPointManager;

	private bool isPlacingPoint = false;
	private bool requesttModel = false;
	private bool verificationStarted = false;

	private ITouchBoard touchBoard;


	public void finishModel()
	{
		bool _result = this.localPointManager.finishModel();

		// Animacion para desplazar el modelo a los puntos reales
		this.verificationStarted = true;
		this.verifer.GetComponent<VerifierController>().setAnimation(_result);
		// PUNTAJE
	}

	public void setModel(Model newModel)
	{
		this.requesttModel = false;
		this.localPointManager.CurrentModel = newModel;
		this.localPointManager.instantiatePoints ();
	}

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

	private IEnumerator resetModel()
	{
		yield return new WaitForSeconds(1.5f);
		foreach(Transform child in this.pointsContainer.transform)
		{
			GameObject.Destroy(child.gameObject);
		}
		// Deshabilitar el verifer
		// crear un bool que lo lea el platform model controller
		// borrar el modelo en el platformodel
	}

	private void currentResult()
	{
		if(this.verificationStarted)
		{
			if(this.verifer.GetComponent<VerifierController>().IsVerificationDone)
			{
				this.verificationStarted = false;
			}
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

	#region Script
	void Awake () 
	{
		this.localPointManager = new PointManager (this.pointsContainer);
		this.touchBoard = GameObject.FindGameObjectWithTag ("TouchBoard").GetComponent<TouchBoardIndexController> ();
	}

	void Update()
	{
		this.placePoint ();
		this.currentResult();
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
