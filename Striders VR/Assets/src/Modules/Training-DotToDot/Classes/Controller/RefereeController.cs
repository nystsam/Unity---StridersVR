using UnityEngine;
using System.Collections;

public class RefereeController : MonoBehaviour {

	private bool isHoldingDot = false;
	private GameObject currentDot = null;


	#region Properties
	public bool IsHoldingDot
	{
		get { return this.isHoldingDot; }
		set { this.isHoldingDot = value; }
	}

	public GameObject CurrentDot
	{
		get { return this.currentDot; }
		set { this.currentDot = value; }
	}
	#endregion
}
