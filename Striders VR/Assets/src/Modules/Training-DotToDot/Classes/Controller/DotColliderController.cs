using UnityEngine;
using System.Collections.Generic;
using StridersVR.Domain.DotToDot;

public class DotColliderController : MonoBehaviour {

	public Light colliderLight;

	private bool turnOn = false;
	private bool setupChildList = false;
	private Vector3 parentCurrentRotation;
	private VertexPoint vertexPointLocal = null;
	private GameObject dotContainer;
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

	#region Script
	void Awake()
	{
		this.childColliderList = new List<GameObject> ();
		this.dotContainer = GameObject.FindGameObjectWithTag ("DotContainer");
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

	void OnTriggerEnter(Collider other)
	{
		this.turnOn = true;
		Debug.Log (this.childColliderList.Count);
	}

	void OnTriggerExit()
	{
		this.turnOn = false;
		transform.parent.localEulerAngles = this.parentCurrentRotation;
		transform.parent.localScale = new Vector3 (1, 1, 1);
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
