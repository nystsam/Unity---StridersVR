using UnityEngine;
using System.Collections;
using StridersVR.Modules.DotToDot.Logic.Representatives;

public class RefereeController : MonoBehaviour {

	public GameObject dotContainer;
	public GameObject endPointsContainer;
	public GameObject endPointStripe;

	private bool isHoldingDot = false;
	private bool isFirstStripe = true;

	private GameObject startingPoint = null;
	private GameObject endingPoint = null;
	private GameObject dotFigure = null;
	private GameObject previousEndPointBall = null;

	private Vector3 lastPointPosition = Vector3.zero;

	private RepresentativeReferee refereeLogic = new RepresentativeReferee ();

	private bool overideEndPointBall()
	{
		for (int i = 0; i < this.endPointsContainer.transform.childCount; i++) 
		{
			Transform _child = this.endPointsContainer.transform.GetChild(i);

			if(_child.localPosition == this.lastPointPosition)
			{
				this.previousEndPointBall = _child.gameObject;
				return true;
			}
		}
		return false;
	}

	private void createEndPointBall()
	{
		GameObject _newClone; 
		Material _newMat = Resources.Load("Materials/Training-DotToDot/MatCurrentStripe", typeof(Material)) as Material;

		if (!this.overideEndPointBall ()) 
		{
			_newClone = (GameObject)GameObject.Instantiate (this.endPointStripe, Vector3.zero, Quaternion.Euler (Vector3.zero));
		
			_newClone.transform.parent = this.endPointsContainer.transform;
			_newClone.transform.localPosition = this.lastPointPosition;
			_newClone.GetComponent<MeshRenderer> ().material = _newMat;
			this.previousEndPointBall = _newClone;
		} 
		else 
		{
			this.previousEndPointBall.GetComponent<MeshRenderer> ().material = _newMat;
		}
	}

	public void newEndingPointDiscover()
	{
		this.dotFigure.GetComponentInChildren<DotFigureController> ().NearlyPoint = true;
	}

	public void exitFromDiscoveredPoint()
	{
		if (this.dotFigure != null) 
		{
			this.endingPoint = null;
			this.dotFigure.GetComponentInChildren<DotFigureController> ().NearlyPoint = false;
		}
	}

	public bool isHandleIntersecting()
	{
		return this.dotFigure.GetComponentInChildren<DotFigureController> ().IsIntersecting;
	}

	public void placeDotFigure(Vector3 lastPosition)
	{
		if (this.dotFigure != null) 
		{
			if(this.isFirstStripe)
			{
				this.isFirstStripe = false;
			}

			if(this.lastPointPosition != Vector3.zero)
			{
				Material _newMat = Resources.Load("Materials/Training-DotToDot/MatEndPoint", typeof(Material)) as Material;
				this.previousEndPointBall.GetComponent<MeshRenderer> ().material = _newMat;
			}

			this.lastPointPosition = lastPosition;
			this.createEndPointBall();
			this.dotFigure.GetComponentInChildren<DotFigureController>().Placed = true;
			this.dotFigure = null;
			this.endingPoint = null;

			if(this.refereeLogic.pointPlaced())
			{
				if(this.refereeLogic.validateErrors(this.dotContainer))
				{
					this.refereeLogic.ChangeFigureModel = true;
					this.isHoldingDot = false;
					this.isFirstStripe = true;
					this.lastPointPosition = Vector3.zero;
				}
			}

		}
	}

	public bool checkLastPointPosition(Vector3 currentPosition)
	{
		if (this.lastPointPosition == currentPosition) 
		{
			return true;
		}
		return false;
	}

	public void setNumberOfStripes(int number)
	{
		this.refereeLogic.setNumberOfPoints (number);
	}


	#region Script

	#endregion

	#region Properties
	public bool IsHoldingDot
	{
		get { return this.isHoldingDot; }
		set { this.isHoldingDot = value; }
	}

	public bool IsFirstStripe
	{
		get { return this.isFirstStripe; }
	}

	public GameObject StartingPoint
	{
		get { return this.startingPoint; }
		set { this.startingPoint = value; }
	}

	public GameObject DotFigure
	{
		set { this.dotFigure = value; }
	}

	public GameObject EndingPoint
	{
		get { return this.endingPoint; }
		set { this.endingPoint = value; }
	}

	public bool ChangeFigureModel
	{
		get { return this.refereeLogic.ChangeFigureModel; }
		set { this.refereeLogic.ChangeFigureModel = value; }
	}
	#endregion
}
