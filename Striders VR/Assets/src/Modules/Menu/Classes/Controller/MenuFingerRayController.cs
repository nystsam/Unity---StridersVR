using UnityEngine;
using System.Collections;
using StridersVR.Buttons;

public class MenuFingerRayController : MonoBehaviour {

	[SerializeField] private float hitRange;

	private RaycastHit hit;

	private bool isInfoButton = false;

	private GameObject currentObject = null;


	private void setRayCast()
	{
		Ray myRay = new Ray (transform.position, transform.forward * hitRange);
		Debug.DrawRay (transform.position, transform.forward * hitRange);
		if (Physics.Raycast (myRay, out hit, hitRange))
		{
			if(hit.collider.tag.Equals("StatsInfo"))
			{
				if(this.currentObject != null && this.currentObject != hit.collider.gameObject)
				{
					this.currentObject.GetComponent<ButtonInfo>().DisableButton();
				}

				this.isInfoButton = true;
				this.currentObject = hit.collider.gameObject;
				this.currentObject.GetComponent<ButtonInfo>().EnableButton();
			}
		}
		else if(this.currentObject != null)
		{
			if(this.isInfoButton)
			{
				this.currentObject.GetComponent<ButtonInfo>().DisableButton();
				this.currentObject = null;
				this.isInfoButton = false;
			}
		}
	}
	
	#region Script
	void Update () 
	{
		this.setRayCast();
	}
	#endregion
}
