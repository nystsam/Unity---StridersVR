using UnityEngine;
using System.Collections;

public class RefereeController : MonoBehaviour {

	private bool isHoldingDot = false;

	private GameObject currentDotFigure = null;
	private GameObject startingPoint = null;
	private GameObject dotFigure = null;

	private Vector3 pointBoundingBoxCenter;

	public void placeDotFigure()
	{
		if (this.dotFigure != null) 
		{
			this.dotFigure.GetComponentInChildren<DotFigureController>().Placed = true;
			this.dotFigure = null;
		}
	}


	#region Properties
	public bool IsHoldingDot
	{
		get { return this.isHoldingDot; }
		set { this.isHoldingDot = value; }
	}

	public GameObject CurrentDotFigure
	{
		get { return this.currentDotFigure; }
		set { this.currentDotFigure = value; }
	}

	public GameObject StartingPoint
	{
		get { return this.startingPoint; }
		set { this.startingPoint = value; }
	}

	public GameObject DotFigure
	{
		set { this.dotFigure = value; }
	}

	public Vector3 PointBoundingBoxCenter
	{
		get { return this.pointBoundingBoxCenter; }
		set { this.pointBoundingBoxCenter = value; }
	}
	#endregion
}
