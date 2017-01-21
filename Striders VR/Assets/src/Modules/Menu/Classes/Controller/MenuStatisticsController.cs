using UnityEngine;
using System.Collections;
using StridersVR.Modules.Menu.Logic;

public class MenuStatisticsController : MonoBehaviour {

	public static MenuStatisticsController Current;

	public GameObject ButtonsContainer;

	private StatisticManager localManager;

	private bool buttonsCreated = false;


	public MenuStatisticsController()
	{
		Current = this;
	}

	public void CreateButtons()
	{
		if(!this.buttonsCreated)
			this.StartCoroutine(waitForSeconds());
	}

	private IEnumerator waitForSeconds()
	{
		yield return new WaitForSeconds(0.75f);
		this.localManager.InstantiateButtons(this.ButtonsContainer);
		this.buttonsCreated = true;
	}



	#region Script
	void Start()
	{
		this.localManager = new StatisticManager();
	}
	#endregion
	
}
