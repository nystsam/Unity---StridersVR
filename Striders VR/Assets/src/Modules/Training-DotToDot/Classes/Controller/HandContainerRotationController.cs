using UnityEngine;
using System.Collections;
using Leap;

public class HandContainerRotationController : MonoBehaviour {

	private bool isRightHandController(Collider other)
	{
		if (other.GetComponentInParent<HandController> ()) 
		{
			if(other.GetComponentInParent<HandModel>().GetLeapHand().IsRight)
				return true;
		}
		return false;
	}


	#region Script
	void Awake()
	{
	}

	#endregion
}