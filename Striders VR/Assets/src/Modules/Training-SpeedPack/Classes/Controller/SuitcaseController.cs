using UnityEngine;
using System.Collections.Generic;
using StridersVR.Domain.SpeedPack;
using StridersVR.Modules.SpeedPack.Logic.Representatives;

public class SuitcaseController : MonoBehaviour {

	public ScriptableObject suitcasePartData;


	private Suitcase currentSuitcase;

	private RepresentativeSuitcase suitcaseLogic;

	#region Script
	void Awake () 
	{
		this.suitcaseLogic = new RepresentativeSuitcase (this.gameObject);
		this.suitcaseLogic.SetData = this.suitcasePartData;
		this.currentSuitcase = this.suitcaseLogic.getSuitcase ();
	}

	void Update () 
	{
	
	}
	#endregion
}
