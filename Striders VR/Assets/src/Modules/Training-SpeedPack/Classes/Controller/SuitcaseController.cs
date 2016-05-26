using UnityEngine;
using System.Collections.Generic;
using StridersVR.Domain.SpeedPack;
using StridersVR.Modules.SpeedPack.Logic.Representatives;

public class SuitcaseController : MonoBehaviour {

	public ScriptableObject suitcasePartData;
	public ScriptableObject itemData;

	private GameObject part;

	private bool allowStartAnimation = false;
	private bool partSelected = false;

	private int currentPartIndex;

	private Suitcase currentSuitcase;

	private RepresentativeSuitcase suitcaseLogic;

	private void animateParts()
	{
		if (!this.partSelected) 
		{
			this.part = this.transform.GetChild(this.currentPartIndex).gameObject;
			this.partSelected = true;
			this.part.GetComponent<SuitcasePartController>().allowAnimation();
		}

		if (this.part != null && this.part.GetComponent<SuitcasePartController>().IsAnimationDone) 
		{
			this.currentPartIndex --;
			if(this.currentPartIndex > 0)
			{
				this.partSelected = false;
				/* Pintar objetos en la siguiente parte */
			}
			else
			{
				this.allowStartAnimation = false;
			}
			this.part.GetComponent<SuitcasePartController>().reflectItems(this.transform.GetChild(this.currentPartIndex).gameObject);
			this.part = null;
		}
	}

	public void placePlayerItem(Spot currentSpot)
	{
		GameObject _draggableItem = GameObject.FindGameObjectWithTag ("DraggableItem");

		_draggableItem.GetComponent<ItemDraggableController> ().stopDragging ();
		_draggableItem.GetComponent<BoxCollider> ().enabled = false;
		_draggableItem.transform.parent = this.transform.GetChild(0).Find ("SuitcasePart").Find ("Items");
		_draggableItem.transform.localPosition = currentSpot.SpotPosition;

		this.currentPartIndex = this.transform.childCount - 1;
		this.allowStartAnimation = true;
	}


	#region Script
	void Awake () 
	{
		this.suitcaseLogic = new RepresentativeSuitcase (this.gameObject);
		this.suitcaseLogic.SetPartData = this.suitcasePartData;
		this.suitcaseLogic.SetItemData = this.itemData;
		this.currentSuitcase = this.suitcaseLogic.getSuitcase ();
	}

	void Start()
	{
		this.suitcaseLogic.spawnItems (this.currentSuitcase);
		this.suitcaseLogic.spawnPlayerItem ();
	}

	void Update()
	{
		if (this.allowStartAnimation) 
		{
			this.animateParts();
		}
	}
	#endregion
}
