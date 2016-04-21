using UnityEngine;
using System.Collections;
using StridersVR.Modules.DotToDot.Logic.Representatives;

public class RefereeController : MonoBehaviour {

	public GameObject dotContainer;
	public GameObject endPointsContainer;
	public GameObject endPointStripe;

	private bool isHoldingDot = false;
	private bool isFirstStripe = true;

	private GameObject currentDotFigure = null;
	private GameObject startingPoint = null;
	private GameObject dotFigure = null;
	private GameObject previousEndPoint = null;

	private Vector3 pointBoundingBoxCenter;
	private Vector3 lastVertexPosition = Vector3.zero;

	private RepresentativeReferee refereeLogic = new RepresentativeReferee ();

	private bool overideEndPoint()
	{
		for (int i = 0; i < this.endPointsContainer.transform.childCount; i++) 
		{
			Transform _child = this.endPointsContainer.transform.GetChild(i);

			if(_child.localPosition == this.lastVertexPosition)
			{
				this.previousEndPoint = _child.gameObject;
				return true;
			}
		}
		return false;
	}

	private void createEndPoint()
	{
		GameObject _newClone; 
		Material _newMat = Resources.Load("Materials/Training-DotToDot/MatCurrentStripe", typeof(Material)) as Material;

		if (!this.overideEndPoint ()) 
		{
			_newClone = (GameObject)GameObject.Instantiate (this.endPointStripe, Vector3.zero, Quaternion.Euler (Vector3.zero));
		
			_newClone.transform.parent = this.endPointsContainer.transform;
			_newClone.transform.localPosition = this.lastVertexPosition;
			_newClone.GetComponent<MeshRenderer> ().material = _newMat;
			this.previousEndPoint = _newClone;
		} 
		else 
		{
			this.previousEndPoint.GetComponent<MeshRenderer> ().material = _newMat;
		}
	}

	public void placeDotFigure(Vector3 lastPosition)
	{
		if (this.dotFigure != null) 
		{
			if(this.isFirstStripe)
			{
				this.isFirstStripe = false;
			}

			if(this.lastVertexPosition != Vector3.zero)
			{
				Material _newMat = Resources.Load("Materials/Training-DotToDot/MatEndPoint", typeof(Material)) as Material;
				this.previousEndPoint.GetComponent<MeshRenderer> ().material = _newMat;
			}

			this.lastVertexPosition = lastPosition;
			this.createEndPoint();
			this.dotFigure.GetComponentInChildren<DotFigureController>().Placed = true;
			this.dotFigure = null;

			if(this.refereeLogic.pointPlaced(this.dotContainer))
			{
				this.refereeLogic.removeCurrentFigureModel(this.dotContainer, this.endPointsContainer);
			}
		}
	}

	public bool checkLastVertexPosition(Vector3 currentPosition)
	{
		if (this.lastVertexPosition == currentPosition) 
		{
			return true;
		}
		return false;
	}

	public void setNumberOfPoitns(int number)
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

	public GameObject CurrentDotFigure
	{
		get { return this.currentDotFigure; }
		set { this.currentDotFigure = value; }
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

	public Vector3 PointBoundingBoxCenter
	{
		get { return this.pointBoundingBoxCenter; }
		set { this.pointBoundingBoxCenter = value; }
	}

	public bool ChangeFigureModel
	{
		get { return this.refereeLogic.ChangeFigureModel; }
		set { this.refereeLogic.ChangeFigureModel = value; }
	}
	#endregion
}
