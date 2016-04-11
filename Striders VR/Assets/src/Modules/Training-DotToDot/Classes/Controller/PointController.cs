using UnityEngine;
using System.Collections.Generic;
using StridersVR.Domain.DotToDot;

public class PointController : MonoBehaviour {

	public Light pointLight;
	public GameObject drawlableFigurePrefab;

	private bool turnOn;
	private bool isAlredyCloned;
	private bool setupChildList;

	private VertexPoint vertexPointLocal;

	private GameObject newDrawlableFigure;
	private GameObject dotReferee;
	private GameObject dotContainer;

	private List<GameObject> childColliderList;


	private GameObject getChildcollider(Vector3 childPosition)
	{
		GameObject childDotcollider = null;
		
		for (int i = 0; i < this.dotContainer.transform.childCount; i++) 
		{
			childDotcollider = this.dotContainer.transform.GetChild(i).FindChild("PointCollider").gameObject;
			if(childDotcollider.GetComponent<PointController>().VertexPointLocal.VertexPointPosition == childPosition)
				break;
		}
		
		return childDotcollider;
	}
	
	private void setNeighbourDots()
	{
		if (this.vertexPointLocal != null && !this.setupChildList) 
		{
			foreach (Vector3 childPosition in this.vertexPointLocal.NeighbourVectorList) 
			{
				GameObject childCollider = this.getChildcollider(childPosition);
				if(childCollider != null)
				{
					this.childColliderList.Add(childCollider);
				}
			}
			this.setupChildList = true;
		}
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
			this.dotReferee.GetComponent<RefereeController> ().CurrentDotFigure = _newClone;
			this.dotReferee.GetComponent<RefereeController> ().StartingPoint = this.gameObject;
			this.dotReferee.GetComponent<RefereeController> ().DotFigure = _newClone;
			this.newDrawlableFigure = _newClone;
			this.isAlredyCloned = true;
		}

	}

	public void resetDrawlableFigure()
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
		this.childColliderList = new List<GameObject> ();
	}

	void Update () 
	{
		//this.setNeighbourDots ();
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
				this.starDragging(other.collider);
			}
			else if(this.dotReferee.GetComponent<RefereeController> ().IsHoldingDot && !this.isAlredyCloned)
			{
				GameObject _startingPoint = this.dotReferee.GetComponent<RefereeController> ().StartingPoint;

				if(_startingPoint != null)
				{
					_startingPoint.GetComponent<PointController>().DrawlableFigure = null;
					_startingPoint.GetComponent<PointController>().isAlredyCloned = false;

					this.dotReferee.GetComponent<RefereeController> ().PointBoundingBoxCenter = this.GetComponent<SphereCollider>().bounds.center;
					this.dotReferee.GetComponent<RefereeController> ().CurrentDotFigure = null;
					this.dotReferee.GetComponent<RefereeController> ().StartingPoint = null;
					this.dotReferee.GetComponent<RefereeController> ().placeDotFigure();
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
