using UnityEngine;
using System.Collections;
using StridersVR.Domain.DotToDot;

public class PointController : MonoBehaviour {

	public GameObject pointLight;
	public GameObject pointAura;

	private bool changePoint = false;

	private Point localPoint;

	private GameObject pointAdvisor;

	#region Script
	void Awake () 
	{
		this.localPoint = new Point(this.transform.localPosition);
		this.localPoint.setPointGraphics (this.pointLight, this.pointAura);

		this.pointAdvisor = GameObject.FindGameObjectWithTag("GameController");
	}

	void Update()
	{
		if (this.changePoint) 
		{
			this.changePoint = false;
			this.pointAdvisor.GetComponent<PointAdvisorController>().cancelPlacingPoint();
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals ("IndexUI")) 
		{
			if(!this.pointAdvisor.GetComponent<PointAdvisorController>().setPoint(this.localPoint))
			{
				this.localPoint.IsSelectedPoint = true;
				this.localPoint.turnOn();
			}
		}
		else if (other.tag.Equals("IndexRight"))
		{
			if(this.pointAdvisor.GetComponent<PointAdvisorController>().isSamePoint(this.localPoint))
			{
				this.pointAdvisor.GetComponent<PointAdvisorController>().cancelCurrentStripe();
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
