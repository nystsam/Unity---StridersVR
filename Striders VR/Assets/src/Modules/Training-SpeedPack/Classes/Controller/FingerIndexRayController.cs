using UnityEngine;
using System;
using StridersVR.Domain;
using StridersVR.Domain.SpeedPack;

public class FingerIndexRayController : MonoBehaviour {

	private GameObject progressBar;
	private GameObject suitcaseContainer;
//	private GameObject placeButton;

	private HandModel handModel;

	private Spot hittingSpot;

	private ITouchBoard touchBoard;

	private float hitRange = 0.45f;
	private float currentRayDistance;

	private RaycastHit hit;

	private bool hitting;
	private bool placingItem = false;
	private bool isRayRangeIncreased = false;


	private void outHit()
	{
		if (hit.collider.tag.Equals ("SuitcaseSpot") && hit.distance < 0.35f && this.handModel.GetLeapHand ().IsLeft) 
		{
			this.hitting = true;
			hit.collider.GetComponent<SpotController> ().hoverColor (true);
			if(this.GetComponentInParent<GrabController>().IsGrabbingObject() && hit.collider.GetComponent<SpotController> ().IsActive)
			{
				this.hittingSpot = hit.collider.GetComponent<SpotController>().LocalSpot;
				this.touchBoard.startAction();
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
		Ray myRay = new Ray (transform.position, direction * hitRange);
		if (!this.hitting) 
		{
			if (Physics.Raycast (myRay, out hit, hitRange)) 
			{
				this.outHit ();
			}
		} 
		else if (this.hitting) 
		{
			try
			{
				currentRayDistance = Vector3.Distance (this.transform.position, hit.transform.position);
				
				if (currentRayDistance > hitRange && hit.collider.tag.Equals ("SuitcaseSpot")) 
				{
					this.hitting = false;
					this.placingItem = false;
					hit.collider.GetComponent<SpotController> ().hoverColor (false);
					this.touchBoard.cancelAction();
					this.hittingSpot = null;
				}
//				else if (!this.GetComponentInParent<GrabController> ().IsTouching && this.handModel.GetLeapHand ().IsLeft) 
//				{
//					this.placeButton.GetComponent<PlaceButtonController> ().buttonActivation (false);
//					this.placeButton.GetComponent<PlaceButtonController> ().CurrentSpot = null;
//				}
			}
			catch (NullReferenceException e)
			{
				Debug.Log (e.Message);
				Debug.Log ("Restarting Suitcase...");
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

		if (this.handModel.GetLeapHand ().IsLeft) 
		{
			this.touchBoard = GameObject.FindGameObjectWithTag("TouchBoard").GetComponent<TouchBoardController>();
		}
	}

	void Update () 
	{
//		if (this.handModel.GetLeapHand ().IsLeft) 
//		{
			if(this.GetComponentInParent<GrabController>().IsGrabbingObject() && !this.isRayRangeIncreased)
			{
				hitRange += 0.25f;
				this.isRayRangeIncreased = true;
			}
			else if(!this.GetComponentInParent<GrabController>().IsGrabbingObject() && this.isRayRangeIncreased)
			{
				hitRange -= 0.25f;
				this.isRayRangeIncreased = false;
			}
			Debug.DrawRay (transform.position, -Vector3.forward * hitRange);
			this.createRay (-Vector3.forward);
//		}

		if (this.placingItem) 
		{
			if(this.touchBoard.actionComplete())
			{
				this.placingItem = false;

				GameObject _draggableItem = this.GetComponentInParent<GrabController>().GetDraggableItem();
				this.suitcaseContainer.GetComponent<SuitcaseController>().placePlayerItem(this.hittingSpot, _draggableItem);
			}
		}

	}
	#endregion
}
