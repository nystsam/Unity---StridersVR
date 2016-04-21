using UnityEngine;
using System.Collections.Generic;
using StridersVR.Domain.DotToDot;
using StridersVR.Modules.DotToDot.Logic.Representatives;

public class PointController : MonoBehaviour {

	public Light pointLight;
	public GameObject drawlableFigurePrefab;

	private bool turnOn;
	private bool isAlredyCloned;
	private bool setupChildList;

	private VertexPoint vertexPointLocal;

	private RepresentativePoint pointLogic;

	private GameObject newDrawlableFigure;
	private GameObject dotReferee;
	private GameObject dotContainer;


	private void starDragging(Collider other)
	{
		GameObject _newClone;

		if (!isAlredyCloned) 
		{
			_newClone = (GameObject)GameObject.Instantiate (this.drawlableFigurePrefab, Vector3.zero, Quaternion.Euler (new Vector3(0,180,0)));
			_newClone.transform.parent = this.transform.parent;
			_newClone.transform.localPosition = Vector3.zero;

			this.dotReferee.GetComponent<RefereeController> ().IsHoldingDot = true;
			this.dotReferee.GetComponent<RefereeController> ().CurrentDotFigure = _newClone;
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
			this.dotReferee.GetComponent<RefereeController> ().CurrentDotFigure = null;
			this.dotReferee.GetComponent<RefereeController> ().StartingPoint = null;
			this.newDrawlableFigure = null;
			this.isAlredyCloned = false;
		}
	}

	public void validateNeighbour(Vector3 endPoint)
	{
		this.pointLogic.validateNeighbour (endPoint);
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
		this.setupChildList = false;
		this.vertexPointLocal = null;
		this.newDrawlableFigure = null;
		this.dotReferee = GameObject.FindGameObjectWithTag("DotReferee");
		this.dotContainer = GameObject.FindGameObjectWithTag ("DotContainer");

		this.pointLogic = new RepresentativePoint (this.dotContainer);
	}

	void Update () 
	{
		if (!this.setupChildList) 
		{
			this.setupChildList = this.pointLogic.setNeighbourDots(this.vertexPointLocal);

		}
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
		if (other.collider.name.Equals ("CubeTrigger")) 
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
					if(this.dotReferee.GetComponent<RefereeController> ().checkLastVertexPosition(this.vertexPointLocal.VertexPointPosition))
					{
						this.starDragging(other.collider);
					}
				}
			}
			else if(this.dotReferee.GetComponent<RefereeController> ().IsHoldingDot && !this.isAlredyCloned)
			{
				GameObject _startingPoint = this.dotReferee.GetComponent<RefereeController> ().StartingPoint;

				if(_startingPoint != null)
				{
					_startingPoint.GetComponent<PointController>().DrawlableFigure = null;
					_startingPoint.GetComponent<PointController>().isAlredyCloned = false;
					_startingPoint.GetComponent<PointController>().validateNeighbour(this.vertexPointLocal.VertexPointPosition);

					this.dotReferee.GetComponent<RefereeController> ().PointBoundingBoxCenter = this.GetComponent<SphereCollider>().bounds.center;
					this.dotReferee.GetComponent<RefereeController> ().CurrentDotFigure = null;
					this.dotReferee.GetComponent<RefereeController> ().StartingPoint = null;
					this.dotReferee.GetComponent<RefereeController> ().placeDotFigure(this.vertexPointLocal.VertexPointPosition);
				}
			}
		} 
	}
	
	void OnCollisionExit(Collision other)
	{
		// ****************************************
		// aplicar regla para reconocer leap motion
		// ****************************************
		if (other.collider.name.Equals ("CubeTrigger")) 
		{
			this.turnOn = false;
			this.resetDrawlableFigure();

		}
	}
	#endregion

	#region Properties
	public VertexPoint VertexPointLocal
	{
		get { return this.vertexPointLocal; }
		set { this.vertexPointLocal = value; }
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
