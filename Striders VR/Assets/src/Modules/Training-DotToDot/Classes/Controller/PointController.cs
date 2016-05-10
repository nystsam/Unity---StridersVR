using UnityEngine;
using System.Collections.Generic;
using StridersVR.Domain.DotToDot;
using StridersVR.Modules.DotToDot.Logic.Representatives;

public class PointController : MonoBehaviour {

	public Light pointLight;
	public GameObject drawlableFigurePrefab;

	private bool turnOn;
	private bool isAlredyCloned;

	private PointDot localPointDot;

	private RepresentativePoint pointLogic;

	private GameObject newDrawlableFigure;
	private GameObject dotReferee;
	private GameObject dotContainer;


	private bool isHand(Collider other)
	{
		if (other.GetComponentInParent<HandController> ())
			return true;
		return false;
	}

	private void starDragging(Collider other)
	{
		GameObject _newClone;

		if (!isAlredyCloned) 
		{
			_newClone = (GameObject)GameObject.Instantiate (this.drawlableFigurePrefab, Vector3.zero, Quaternion.Euler (new Vector3(0,180,0)));
			_newClone.transform.parent = this.transform.parent;
			_newClone.transform.localPosition = Vector3.zero;

			this.dotReferee.GetComponent<RefereeController> ().IsHoldingDot = true;
			this.dotReferee.GetComponent<RefereeController> ().StartingPoint = this.gameObject;
			this.dotReferee.GetComponent<RefereeController> ().DotFigure = _newClone;
			this.newDrawlableFigure = _newClone;
			this.isAlredyCloned = true;
		}
	}

	private void resetDrawlableFigure()
	{	
		if (!this.dotReferee.GetComponent<RefereeController> ().IsHoldingDot && this.newDrawlableFigure != null) 
		{
			GameObject.Destroy(this.newDrawlableFigure);
			this.dotReferee.GetComponent<RefereeController> ().StartingPoint = null;
			this.newDrawlableFigure = null;
			this.isAlredyCloned = false;
		}
	}

	public void setLocalPointDot(PointDot localPointDot)
	{
		this.localPointDot = localPointDot;
		this.pointLogic.LocalPointDot = this.localPointDot;
	}

	public void validateNeighbour(PointDot possibleNeighbourPoint)
	{
		this.pointLogic.validateNeighbour (possibleNeighbourPoint);
	}

	public int numberOfErrors()
	{
		return this.pointLogic.ErrorCount;
	}

	#region Script
	void Awake () 
	{
		this.turnOn = false;
		this.isAlredyCloned = false;
		this.newDrawlableFigure = null;
		this.dotReferee = GameObject.FindGameObjectWithTag("DotReferee");
		this.dotContainer = GameObject.FindGameObjectWithTag ("DotContainer");

		this.pointLogic = new RepresentativePoint ();
	}

	void Update () 
	{
//		if (!this.setupChildList) 
//		{
//			this.setupChildList = this.pointLogic.setNeighbourDots(this.vertexPointLocal);
//
//		}
		this.transform.localPosition = Vector3.zero;

		this.resetDrawlableFigure ();

		if (turnOn && this.pointLight.intensity < 2) 
		{
			this.pointLight.intensity += 0.1f;
		} 
		else if (!turnOn && this.pointLight.intensity > 0.4f) 
		{
			this.pointLight.intensity -= 0.1f;
		}
	}

	void OnCollisionEnter(Collision other)
	{
		// ****************************************
		// aplicar regla para reconocer leap motion
		// ****************************************
//		if (other.collider.name.Equals ("CubeTrigger") && !this.dotReferee.GetComponent<RefereeController> ().ChangeFigureModel) 
		if (this.isHand(other.collider) && !this.dotReferee.GetComponent<RefereeController> ().ChangeFigureModel) 
		{
			this.turnOn = true;

			if(!this.dotReferee.GetComponent<RefereeController> ().IsHoldingDot)
			{
				if(this.dotReferee.GetComponent<RefereeController> ().IsFirstStripe)
				{
					this.starDragging(other.collider);
				}
				else
				{
					if(this.dotReferee.GetComponent<RefereeController> ().checkLastPointPosition(this.localPointDot.PointPosition))
					{
						this.starDragging(other.collider);
					}
				}
			}
			else if(this.dotReferee.GetComponent<RefereeController> ().IsHoldingDot && !this.isAlredyCloned)
			{
				if(this.dotReferee.GetComponent<RefereeController> ().EndingPoint == null)
				{
					this.dotReferee.GetComponent<RefereeController> ().EndingPoint = this.gameObject;
					this.dotReferee.GetComponent<RefereeController> ().newEndingPointDiscover();
				}

				if(this.dotReferee.GetComponent<RefereeController> ().isHandleIntersecting())
				{
					GameObject _startingPoint = this.dotReferee.GetComponent<RefereeController> ().StartingPoint;

					if(_startingPoint != null)
					{
						_startingPoint.GetComponent<PointController>().DrawlableFigure = null;
						_startingPoint.GetComponent<PointController>().isAlredyCloned = false;
						_startingPoint.GetComponent<PointController>().validateNeighbour(this.localPointDot);

						this.dotReferee.GetComponent<RefereeController> ().EndingPoint = this.gameObject;
						this.dotReferee.GetComponent<RefereeController> ().StartingPoint = null;
						this.dotReferee.GetComponent<RefereeController> ().placeDotFigure(this.localPointDot.PointPosition);
					}
				}
			}

//			if(!this.dotReferee.GetComponent<RefereeController> ().IsHoldingDot)
//			{
//				if(this.dotReferee.GetComponent<RefereeController> ().IsFirstStripe)
//				{
//					this.starDragging(other.collider);
//				}
//				else
//				{
//					if(this.dotReferee.GetComponent<RefereeController> ().checkLastVertexPosition(this.vertexPointLocal.VertexPointPosition))
//					{
//						this.starDragging(other.collider);
//					}
//				}
//			}
//			else if(this.dotReferee.GetComponent<RefereeController> ().IsHoldingDot && !this.isAlredyCloned)
//			{
//				GameObject _startingPoint = this.dotReferee.GetComponent<RefereeController> ().StartingPoint;
//
//				if(_startingPoint != null)
//				{
//					_startingPoint.GetComponent<PointController>().DrawlableFigure = null;
//					_startingPoint.GetComponent<PointController>().isAlredyCloned = false;
//					_startingPoint.GetComponent<PointController>().validateNeighbour(this.vertexPointLocal.VertexPointPosition);
//
//					this.dotReferee.GetComponent<RefereeController> ().EndingPoint = this.gameObject;
//					this.dotReferee.GetComponent<RefereeController> ().CurrentDotFigure = null;
//					this.dotReferee.GetComponent<RefereeController> ().StartingPoint = null;
//					this.dotReferee.GetComponent<RefereeController> ().placeDotFigure(this.vertexPointLocal.VertexPointPosition);
//				}
//			}
		} 
	}
	
	void OnCollisionExit(Collision other)
	{
		// ****************************************
		// aplicar regla para reconocer leap motion
		// ****************************************
//		if (other.collider.name.Equals ("CubeTrigger"))
		if(this.isHand(other.collider))
		{
			this.turnOn = false;

			if(this.dotReferee.GetComponent<RefereeController> ().IsHoldingDot && !this.isAlredyCloned)
			{
				this.dotReferee.GetComponent<RefereeController> ().exitFromDiscoveredPoint();
			}

			this.resetDrawlableFigure();

		}
	}
	#endregion

	#region Properties
	public PointDot LocalPointDot
	{
		get { return this.localPointDot; }
	}

	public GameObject DrawlableFigure
	{
		get { return this.newDrawlableFigure; }
		set { this.newDrawlableFigure = value; }
	}

	public bool IsAlredyCloned
	{
		set { this.isAlredyCloned = value; }
	}
	#endregion
}
