﻿using UnityEngine;
using System;
using StridersVR.Domain.SpeedPack;

public class FingerIndexRayController : MonoBehaviour {

	public GameObject progressBar;

	private GameObject suitcaseContainer;
//	private GameObject placeButton;

	private HandModel handModel;

	private Spot hittingSpot;

	private float hitHeight = 0.45f;
	private float currentRayDistance;

	private RaycastHit hit;

	private bool hitting;
	private bool placingItem = false;

	private void outHit()
	{
		if (hit.collider.tag.Equals ("SuitcaseSpot") && hit.distance < 0.35f && this.handModel.GetLeapHand ().IsLeft) 
		{
			this.hitting = true;
			hit.collider.GetComponent<SpotController> ().hoverColor (true);
			if(this.GetComponentInParent<GrabController>().IsTouching && hit.collider.GetComponent<SpotController> ().IsActive)
			{
//				this.placeButton.GetComponent<PlaceButtonController>().buttonActivation(true);
//				this.placeButton.GetComponent<PlaceButtonController>().CurrentSpot = hit.collider.GetComponent<SpotController> ().LocalSpot;
				this.hittingSpot = hit.collider.GetComponent<SpotController>().LocalSpot;
				this.progressBar.GetComponent<RProgressBarController>().startLoading();
				this.placingItem = true;
			}
		} 
//		else if (hit.collider.tag.Equals ("PlayerPanelButtons") && hit.distance < 0.30f && this.handModel.GetLeapHand ().IsRight)
//		{
//			if(!hit.collider.GetComponent<PlaceButtonController> ().IsDisabled)
//			{
//				this.hitting = true;
//				hit.collider.GetComponent<PlaceButtonController> ().hoverColor (true);
//			}
//		}
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
					this.placingItem = false;
					hit.collider.GetComponent<SpotController> ().hoverColor (false);

					this.progressBar.GetComponent<RProgressBarController>().stopLoading();
					this.hittingSpot = null;
//					this.placeButton.GetComponent<PlaceButtonController> ().buttonActivation (false);
//					this.placeButton.GetComponent<PlaceButtonController> ().CurrentSpot = null;
				}
//				else if (!this.GetComponentInParent<GrabController> ().IsTouching && this.handModel.GetLeapHand ().IsLeft) 
//				{
//					this.placeButton.GetComponent<PlaceButtonController> ().buttonActivation (false);
//					this.placeButton.GetComponent<PlaceButtonController> ().CurrentSpot = null;
//				}
				else if (currentRayDistance > 0.75f && hit.collider.tag.Equals ("PlayerPanelButtons")) 
				{
					this.hitting = false;
					hit.collider.GetComponent<PlaceButtonController> ().hoverColor (false);
				}
			}
			catch (NullReferenceException e)
			{
				Debug.LogException(e);
				this.hitting = false;
			}

		} 		
	}


	#region Script
	void Awake()
	{
		this.suitcaseContainer = GameObject.FindGameObjectWithTag("Container");
	}

	void Start () 
	{
		this.handModel = this.GetComponentInParent<HandModel> ();
		this.hitting = false;
//		this.placeButton = GameObject.FindGameObjectWithTag ("PlayerPanelButtons");
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

		if (this.placingItem) 
		{
			if(this.progressBar.GetComponent<RProgressBarController>().IsDone)
			{
				this.placingItem = false;
				this.suitcaseContainer.GetComponent<SuitcaseController>().placePlayerItem(this.hittingSpot);
			}
		}

	}
	#endregion
}
