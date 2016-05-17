using UnityEngine;
using System.Collections.Generic;
using StridersVR.Domain.SpeedPack;
using StridersVR.Modules.SpeedPack.Logic.Representatives;

public class SuitcaseController : MonoBehaviour {

	public ScriptableObject suitcasePartData;
	public ScriptableObject itemData;

	private Suitcase currentSuitcase;

	private RepresentativeSuitcase suitcaseLogic;

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

	void Update () 
	{
	
	}
	#endregion
}
