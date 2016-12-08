using UnityEngine;
using System.Collections;
using StridersVR.Domain.DotToDot;

public class PointController : MonoBehaviour {

	public GameObject pointLight;
	public GameObject pointAura;

	private bool changePoint = false;

	private Point localPoint;

	private GameObject pointManager;


	public void setLocalPoint(Point point)
	{
		this.localPoint = point;
		this.localPoint.setGameplayValues (this.pointLight, this.pointAura);
	}

	#region Script
	void Awake () 
	{
		this.pointManager = GameObject.FindGameObjectWithTag("GameController");
	}

	void Update()
	{
		if (this.changePoint) 
		{
			this.changePoint = false;
			this.pointManager.GetComponent<PointManagerController>().cancelPlacingPoint();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals ("IndexUI")) 
		{
			if(!this.pointManager.GetComponent<PointManagerController>().setPoint(this.localPoint))
			{
				this.localPoint.IsSelectedPoint = true;
				this.localPoint.turnOn();
			}
		}
		else if (other.tag.Equals("IndexRight"))
		{
			if(this.pointManager.GetComponent<PointManagerController>().isSamePoint(this.localPoint))
			{
				this.pointManager.GetComponent<PointManagerController>().cancelCurrentStripe();
				this.localPoint.IsSelectedPoint = false;
				this.localPoint.turnOff();
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag.Equals ("IndexUI")) 
		{
			if(this.localPoint.IsSelectedPoint)
			{
				this.changePoint = true;
				this.localPoint.IsSelectedPoint = false;
				this.localPoint.turnOff();
			}
		}
	}
	#endregion
}
