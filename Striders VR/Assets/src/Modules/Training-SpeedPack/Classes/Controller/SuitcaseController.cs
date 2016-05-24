using UnityEngine;
using System.Collections.Generic;
using StridersVR.Domain.SpeedPack;
using StridersVR.Modules.SpeedPack.Logic.Representatives;

public class SuitcaseController : MonoBehaviour {

	public ScriptableObject suitcasePartData;
	public ScriptableObject itemData;

	private Suitcase currentSuitcase;

	private RepresentativeSuitcase suitcaseLogic;

	public void placePlayerItem(Spot currentSpot)
	{
		GameObject _draggableItem = GameObject.FindGameObjectWithTag ("DraggableItem");

		_draggableItem.GetComponent<ItemDraggableController> ().stopDragging ();
		_draggableItem.GetComponent<BoxCollider> ().enabled = false;
		_draggableItem.transform.parent = this.transform.GetChild(0).Find ("SuitcasePart").Find ("Items");
		_draggableItem.transform.localPosition = currentSpot.SpotPosition;
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
	#endregion
}
