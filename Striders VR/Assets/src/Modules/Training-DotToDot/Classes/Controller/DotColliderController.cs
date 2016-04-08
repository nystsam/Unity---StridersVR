using UnityEngine;
using System.Collections.Generic;
using StridersVR.Domain.DotToDot;

public class DotColliderController : MonoBehaviour {

	public Light colliderLight;
	public GameObject dotFigure;
	public GameObject placedCollider;

	private bool turnOn = false;
	private bool setupChildList = false;
	private bool isHolding = false;
	private Vector3 parentCurrentRotation;
	private VertexPoint vertexPointLocal = null;
	private GameObject dotContainer;
	private GameObject dotReferee;
	private List<GameObject> childColliderList;

	private GameObject getChildcollider(Vector3 childPosition)
	{
		GameObject childDotcollider = null;

		for (int i = 0; i < this.dotContainer.transform.childCount; i++) 
		{
			childDotcollider = this.dotContainer.transform.GetChild(i).FindChild("Resizable").FindChild("DotCollider").gameObject;
			if(childDotcollider.GetComponent<DotColliderController>().vertexPointLocal.VertexPointPosition == childPosition)
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

	private bool availableDotCollider(Collider other)
	{
		if (other.tag.Equals ("DotCollider") && !this.dotFigure.GetComponent<DotFigureController> ().Placed) 
		{
			return true;
//			if(!other.GetComponent<DotColliderController>().isPlaced())
//			{
//				return true;
//			}
		}

		return false;
	}

	public bool isPlaced()
	{
		return this.dotFigure.GetComponent<DotFigureController> ().Placed;
	}

	public void placeDot()
	{
		this.dotFigure.GetComponent<DotFigureController>().Placed = true;
		this.dotFigure.GetComponent<DotFigureController>().enabled = false;

		this.dotReferee.GetComponent<RefereeController> ().IsHoldingDot = false;	
		this.dotReferee.GetComponent<RefereeController> ().CurrentDot = null;
	}


	#region Script
	void Awake()
	{
		this.childColliderList = new List<GameObject> ();
		this.dotContainer = GameObject.FindGameObjectWithTag ("DotContainer");
		this.dotReferee = GameObject.FindGameObjectWithTag("DotReferee");
		this.parentCurrentRotation = transform.parent.localEulerAngles;
	}

	void Update()
	{
		this.setNeighbourDots ();

		if (turnOn && this.colliderLight.intensity < 2) 
		{
			this.colliderLight.intensity += 0.1f;
		} 
		else if (!turnOn && this.colliderLight.intensity > 0.2f) 
		{
			this.colliderLight.intensity -= 0.1f;
		}
	}

//	void OnTriggerEnter(Collider other)
//	{
//		// ****************************************
//		// aplicar regla para reconocer leap motion
//		// ****************************************
//
//		this.turnOn = true;
//		if (!this.dotReferee.GetComponent<RefereeController> ().IsHoldingDot) 
//		{
//			this.dotReferee.GetComponent<RefereeController> ().IsHoldingDot = true;	
//			this.dotReferee.GetComponent<RefereeController> ().CurrentDot = this.gameObject;
//		} 
//		else if(this.dotReferee.GetComponent<RefereeController> ().IsHoldingDot && other.name.Equals ("CubeTrigger"))
//		{
//			GameObject _holdedDot = this.dotReferee.GetComponent<RefereeController> ().CurrentDot;
//			Debug.Log ("Llego");
//			_holdedDot.GetComponent<DotColliderController>().placeDot();
//		}
//
//
//	}
//
//	void OnTriggerExit()
//	{
//		this.turnOn = false;
//		if (this.dotReferee.GetComponent<RefereeController> ().IsHoldingDot) 
//		{
//			this.dotReferee.GetComponent<RefereeController> ().IsHoldingDot = false;	
//			this.dotReferee.GetComponent<RefereeController> ().CurrentDot = null;
//		}
//
//		if (!this.dotFigure.GetComponent<DotFigureController> ().Placed) 
//		{
//			transform.parent.localEulerAngles = this.parentCurrentRotation;
//			transform.parent.localScale = new Vector3 (1, 1, 1);
//		}
//	}

	void OnCollisionEnter(Collision other)
	{
		if (other.collider.name.Equals ("CubeTrigger")) 
		{
			this.turnOn = true;
			if (!this.dotReferee.GetComponent<RefereeController> ().IsHoldingDot) 
			{
				this.isHolding = true;
				this.dotReferee.GetComponent<RefereeController> ().IsHoldingDot = true;	
			} 
		} 
		else if (this.availableDotCollider(other.collider) && this.isHolding) 
		{
			this.placeDot();
			this.isHolding = false;

			this.GetComponent<BoxCollider>().enabled = false;
			this.placedCollider.GetComponent<BoxCollider>().enabled = true;
			this.dotReferee.GetComponent<RefereeController> ().IsHoldingDot = false;	
			this.dotReferee.GetComponent<RefereeController> ().CurrentDot = null;
		}
	}

	void OnCollisionExit(Collision other)
	{
		if (other.collider.name.Equals ("CubeTrigger")) 
		{
			this.turnOn = false;
			if (this.dotReferee.GetComponent<RefereeController> ().IsHoldingDot) 
			{
				this.dotReferee.GetComponent<RefereeController> ().IsHoldingDot = false;	
				this.dotReferee.GetComponent<RefereeController> ().CurrentDot = null;
			}
			if (!this.dotFigure.GetComponent<DotFigureController> ().Placed) 
			{
				transform.parent.localEulerAngles = this.parentCurrentRotation;
				transform.parent.localScale = new Vector3 (1, 1, 1);
			}
		}
	}
	#endregion

	#region Properties
	public VertexPoint VertexPointLocal
	{
		get { return this.vertexPointLocal; }
		set { this.vertexPointLocal = value; }
	}
	#endregion
}
