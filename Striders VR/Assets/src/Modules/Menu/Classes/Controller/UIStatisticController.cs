using UnityEngine;
using System.Collections.Generic;
using StridersVR.Modules.Menu.Logic;

public class UIStatisticController : MonoBehaviour {

	public GameObject ButtonsContainer;

	private StatisticManager localManager;

	#region Script
	void Start () 
	{
		this.localManager = new StatisticManager();
		this.localManager.InstantiateButtons(this.ButtonsContainer);
	}
	#endregion
	
}
