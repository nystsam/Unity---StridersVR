using UnityEngine;
using System.Collections;
using Leap;

public class HandFingerColliderController : MonoBehaviour {

	public GameObject indexColliderLeft;
	public GameObject indexColliderRight;

	void Start () 
	{
		if (this.GetComponent<HandModel> ().GetLeapHand ().IsRight) 
		{
			this.indexColliderLeft.SetActive(false);
		}
		else if(this.GetComponent<HandModel> ().GetLeapHand ().IsLeft)
		{
			this.indexColliderRight.SetActive(false);
		}
	}
	
}
