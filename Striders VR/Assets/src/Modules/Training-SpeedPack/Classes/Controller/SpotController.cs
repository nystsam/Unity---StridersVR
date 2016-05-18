using UnityEngine;
using System.Collections;
using StridersVR.Domain.SpeedPack;

public class SpotController : MonoBehaviour {

	private Material availableSpotColor;
	private Material originalColor;

	private bool isActive;
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

	public void changeColor(bool hitting)
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


	#region Script
	void Awake () 
	{
		this.isActive = false;
		this.isHandHitting = false;
		this.colorChange = true;
		this.availableSpotColor = Resources.Load("Materials/Training-SpeedPack/MatAvailableSpot", typeof(Material)) as Material;
	}

	void Update()
	{
		if (this.localSpot != null && !this.isActive) 
		{
			this.originalColor = this.GetComponent<MeshRenderer> ().material;
			this.isActive = true;
		}
		this.rayHittingColor ();
	}

	void OnTriggerEnter(Collider other)
	{
		if (this.isHandController (other) && this.isActive) 
		{

		}
	}

	void OnTriggerExit(Collider other)
	{
		if (this.isHandController (other) && this.isActive) 
		{

		}
	}
	#endregion

	#region Properties
	public Spot LocalSpot
	{
		set { this.localSpot = value; }
	}
	#endregion

}
