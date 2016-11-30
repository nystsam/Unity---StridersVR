using UnityEngine;
using System.Collections;
using StridersVR.Domain.DotToDot;
using StridersVR.Domain;

public class PointAdvisorController : MonoBehaviour {

	private PointAdvisor localPointAdvisor;

	private bool isPlacingPoint = false;

	private ITouchBoard touchBoard;

	public bool setPoint(Point newPoint)
	{
		if (!this.localPointAdvisor.setTouchingPoint (newPoint)) 
		{
			if(this.localPointAdvisor.isAvailablePoint(newPoint))
			{
				this.isPlacingPoint = true;
				this.touchBoard.startAction();
				return false;
			}
		}

		return true;
	}

	public bool isSamePoint(Point point)
	{
		if(point == this.localPointAdvisor.CurrentPoint)
		{
			return true;
		}

		return false;
	}

	public void cancelCurrentStripe()
	{
		this.localPointAdvisor.cancelCurrentStripe ();
	}

	public void cancelPlacingPoint()
	{
		if(this.isPlacingPoint)
		{
			this.isPlacingPoint = false;
			this.touchBoard.cancelAction();
		}
	}

	private void placePoint()
	{
		if (this.isPlacingPoint) 
		{
			if(this.touchBoard.actionComplete())
			{
				this.isPlacingPoint = false;
				this.localPointAdvisor.placeStripe();
			}
		}
	}

	#region Script
	void Awake () 
	{
		this.localPointAdvisor = new PointAdvisor ();
		this.touchBoard = GameObject.FindGameObjectWithTag ("TouchBoard").GetComponent<TouchBoardIndexController> ();
	}

	void Update()
	{
		this.placePoint ();
	}
	#endregion
}
