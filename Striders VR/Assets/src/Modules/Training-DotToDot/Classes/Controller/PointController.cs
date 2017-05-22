using UnityEngine;
using System.Collections;
using StridersVR.Domain.DotToDot;

public class PointController : MonoBehaviour {

	public GameObject pointLight;
	public GameObject pointAura;
	public GameObject pointNumber;

	private bool changePoint = false;

	private Point localPoint;

	private GameObject pointManager;


	public void setLocalPoint(Point point)
	{
		this.localPoint = point;
		this.localPoint.setGameplayValues (this.pointLight, this.pointAura);
		this.pointNumber.GetComponent<TextMesh>().text = point.PointId.ToString();

		if(point.Position.y <= 0f)
		{
			this.pointNumber.transform.localPosition = new Vector3(0,-0.25f,0);
		}
		else if(point.Position.y > 0f)
		{
			this.pointNumber.transform.localPosition = new Vector3(0,0.25f,0);
		}
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
			if(this.pointManager != null &&
			   !this.pointManager.GetComponent<PointManagerController>().setPoint(this.localPoint))
			{
				this.localPoint.IsSelectedPoint = true;
				this.localPoint.turnOn();
			}
		}
		else if (other.tag.Equals("IndexRight"))
		{
			if(this.pointManager != null &&
			   this.pointManager.GetComponent<PointManagerController>().isSamePoint(this.localPoint))
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
			if(this.localPoint != null && this.localPoint.IsSelectedPoint)
			{
				this.changePoint = true;
				this.localPoint.IsSelectedPoint = false;
				this.localPoint.turnOff();
			}
		}
	}
	#endregion
}
