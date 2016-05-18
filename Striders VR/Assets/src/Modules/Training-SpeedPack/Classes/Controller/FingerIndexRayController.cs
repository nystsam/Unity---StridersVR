using UnityEngine;
using System.Collections;

public class FingerIndexRayController : MonoBehaviour {

	private float hitHeight = 0.35f;
	private float currentRayDistance;

	private RaycastHit hit;

	private bool hitting;


	#region Script
	void Start () 
	{
		this.hitting = false;
	}

	void Update () 
	{
		Debug.DrawRay (transform.position, -Vector3.forward * hitHeight);
		Ray myRay = new Ray (transform.position, -Vector3.forward * hitHeight);
		if (!this.hitting) 
		{
			if (Physics.Raycast (myRay, out hit, hitHeight)) 
			{
				if(hit.collider.tag.Equals("SuitcaseSpot") && hit.distance < 0.25f)
				{
					this.hitting = true;
					hit.collider.GetComponent<SpotController>().changeColor(true);

				}
			}
		} 
		else if (this.hitting) 
		{
			currentRayDistance = Vector3.Distance(this.transform.position, hit.transform.position);

			if(currentRayDistance > hitHeight)
			{
				this.hitting = false;
				hit.collider.GetComponent<SpotController>().changeColor(false);
			}
		}
	}
	#endregion
}
