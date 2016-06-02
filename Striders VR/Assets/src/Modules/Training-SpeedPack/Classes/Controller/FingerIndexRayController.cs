using UnityEngine;
using System.Collections;
using System;

public class FingerIndexRayController : MonoBehaviour {

	private GameObject placeButton;

	private HandModel handModel;

	private float hitHeight = 0.35f;
	private float currentRayDistance;

	private RaycastHit hit;

	private bool hitting;

	private void outHit()
	{
		if (hit.collider.tag.Equals ("SuitcaseSpot") && hit.distance < 0.25f && this.handModel.GetLeapHand ().IsLeft) 
		{
			this.hitting = true;
			hit.collider.GetComponent<SpotController> ().hoverColor (true);
			if(this.GetComponentInParent<GrabController>().IsTouching && hit.collider.GetComponent<SpotController> ().IsActive)
			{
				this.placeButton.GetComponent<PlaceButtonController>().buttonActivation(true);
				this.placeButton.GetComponent<PlaceButtonController>().CurrentSpot = hit.collider.GetComponent<SpotController> ().LocalSpot;
			}
		} 
		else if (hit.collider.tag.Equals ("PlayerPanelButtons") && hit.distance < 0.30f && this.handModel.GetLeapHand ().IsRight)
		{
			if(!hit.collider.GetComponent<PlaceButtonController> ().IsDisabled)
			{
				this.hitting = true;
				hit.collider.GetComponent<PlaceButtonController> ().hoverColor (true);
			}
		}
	}

	private void createRay(Vector3 direction)
	{
		Ray myRay = new Ray (transform.position, direction * hitHeight);
		if (!this.hitting) 
		{
			if (Physics.Raycast (myRay, out hit, hitHeight)) 
			{
				this.outHit ();
			}
		} 
		else if (this.hitting) 
		{
			try
			{
				currentRayDistance = Vector3.Distance (this.transform.position, hit.transform.position);
				
				if (currentRayDistance > hitHeight && hit.collider.tag.Equals ("SuitcaseSpot")) 
				{
					this.hitting = false;
					hit.collider.GetComponent<SpotController> ().hoverColor (false);
					this.placeButton.GetComponent<PlaceButtonController> ().buttonActivation (false);
					this.placeButton.GetComponent<PlaceButtonController> ().CurrentSpot = null;
				}
				else if (!this.GetComponentInParent<GrabController> ().IsTouching && this.handModel.GetLeapHand ().IsLeft) 
				{
					this.placeButton.GetComponent<PlaceButtonController> ().buttonActivation (false);
					this.placeButton.GetComponent<PlaceButtonController> ().CurrentSpot = null;
				}
				else if (currentRayDistance > 0.75f && hit.collider.tag.Equals ("PlayerPanelButtons")) 
				{
					this.hitting = false;
					hit.collider.GetComponent<PlaceButtonController> ().hoverColor (false);
				}
			}
			catch (NullReferenceException e)
			{
				Debug.Log (e.Message);
				this.hitting = false;
			}

		} 
//		else if (hit == null) 
//		{
//			this.hitting = false;
//		}
	}


	#region Script
	void Start () 
	{
		this.handModel = this.GetComponentInParent<HandModel> ();
		this.hitting = false;
		this.placeButton = GameObject.FindGameObjectWithTag ("PlayerPanelButtons");
	}

	void Update () 
	{
		if (this.handModel.GetLeapHand ().IsLeft) 
		{
			Debug.DrawRay (transform.position, -Vector3.forward * hitHeight);
			this.createRay (-Vector3.forward);
		} 
		else if (this.handModel.GetLeapHand ().IsRight) 
		{
			Debug.DrawRay (transform.position, -Vector3.forward * hitHeight);
			this.createRay(-Vector3.forward * 0.15f);
		}

	}
	#endregion
}
