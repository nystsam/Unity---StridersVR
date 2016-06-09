using UnityEngine;
using System.Collections;
using StridersVR.Domain.SpeedPack;

public class SpotController : MonoBehaviour {

	private Material availableSpotColor;
	private Material originalColor;

	private bool isActive;
	private bool isColliderUp;
	private bool isHandHitting;
	private bool colorChange;

	private	Spot localSpot;

	private bool isHandController(Collider other)
	{
		if (other.GetComponentInParent<HandController> ())
			return true;
		return false;
	}

	private void rayHittingColor()
	{
		if (this.colorChange && this.isActive) 
		{
			if (this.isHandHitting) 
			{
				this.GetComponent<MeshRenderer>().material = this.availableSpotColor;
				this.colorChange = false;
			}
			else
			{
				this.GetComponent<MeshRenderer>().material = this.originalColor;
				this.colorChange = false;
			}
		}
	}

	public void hoverColor(bool hitting)
	{
		this.colorChange = true;
		if (hitting) 
		{
			this.isHandHitting = true;	
		} 
		else 
		{
			this.isHandHitting = false;
		}
	}

	public void activeSpot()
	{
		this.originalColor = this.GetComponent<MeshRenderer> ().material;
		this.isActive = true;
	}


	#region Script
	void Awake () 
	{
		this.isActive = false;
		this.isColliderUp = true;
		this.isHandHitting = false;
		this.colorChange = true;
		this.availableSpotColor = Resources.Load("Materials/Training-SpeedPack/MatAvailableSpot", typeof(Material)) as Material;
	}

	void Update()
	{
		if (this.isActive) 
		{
			if(this.localSpot.IsMainSpot && this.isColliderUp)
			{
				this.isColliderUp = false;
				this.GetComponent<BoxCollider>().size = Vector3.zero;
			}
		}
		this.rayHittingColor ();
	}
	#endregion

	#region Properties
	public Spot LocalSpot
	{
		get { return this.localSpot; }
		set { this.localSpot = value; }
	}
	public bool IsActive
	{
		get { return this.isActive; }
	}
	#endregion

}
