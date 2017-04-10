using UnityEngine;
using System.Collections;
using StridersVR.Modules.Menu.Logic;
using StridersVR.Domain;
using StridersVR.Buttons;

public class MenuStatisticsController : MonoBehaviour {

	public static MenuStatisticsController Current;

	public GameObject StatisticsSelectionMenu;
	public GameObject StatisticsMenu;
	public GameObject ButtonsContainer;
	public GameObject PanelInfoContainer;

	[SerializeField] private MenuResultController ResultMenu;

	private StatisticManager localManager;

	private User currentUser;
	public User CurrentUser {
		get { return currentUser; }
		set { currentUser = value; }
	}

	private Training currentTraining;

	private StatisticsPanelButton currentButton;
	private StatisticsPanelButton defaultButton;


	public MenuStatisticsController()
	{
		Current = this;
	}

	public void ShowDetails(Statistic newStatistic)
	{
		this.StatisticsSelectionMenu.SetActive(false);
		this.ResultMenu.transform.position = new Vector3(2,7.5f,10.5f);
		this.ResultMenu.gameObject.SetActive(true);
		this.ResultMenu.SetStatistic(newStatistic);
		this.ResultMenu.SetData();
	}

	public void SelectTraining(Training newTraining)
	{
		this.currentTraining = newTraining;

		this.StatisticsSelectionMenu.transform.GetChild (0).GetChild(0).GetComponent<TextMesh>().text = newTraining.Name;

		this.NewButtonActive(this.defaultButton);
		this.GetLastPlays();
	}

	public void NewButtonActive(StatisticsPanelButton newButton)
	{
		if(this.currentButton != null)
			this.currentButton.ButtonActivation(false);

		this.currentButton = newButton;
		this.currentButton.ButtonActivation(true);

	}

	public void GetLastPlays()
	{
		if(this.currentUser != null)
		{
			this.localManager.GetLastPlays(this.currentUser.Id, this.currentTraining.Id);
			this.setPanelInfo();
		}
	}

	public void GetTodayPlays()
	{
		if(this.currentUser != null)
		{
			this.localManager.GetTodayPlays(this.currentUser.Id, this.currentTraining.Id);
			this.setPanelInfo();
		}
	}

	public void GetYesterdayPlays()
	{
		if(this.currentUser != null)
		{
			this.localManager.GetYesterdayPlays(this.currentUser.Id, this.currentTraining.Id);
			this.setPanelInfo();
		}
	}

	public void SetDefaultButton(StatisticsPanelButton defaultButton)
	{
		this.defaultButton = defaultButton;
	}

	private void setPanelInfo()
	{
		this.localManager.RemovePanelInfo(this.PanelInfoContainer);
		this.localManager.InstantiateButtons(this.PanelInfoContainer);
	}

	#region Script
	void Start()
	{
		this.localManager = new StatisticManager();
		this.localManager.InstantiateButtons(this.ButtonsContainer, this.StatisticsMenu, this.StatisticsSelectionMenu);
		if(GameObject.FindGameObjectWithTag ("StaticUser").GetComponent<StaticUserController> ().User != null)
		{
			this.currentUser = GameObject.FindGameObjectWithTag ("StaticUser").GetComponent<StaticUserController> ().User;
		}
		this.StatisticsSelectionMenu.SetActive(false);
		this.ResultMenu.gameObject.SetActive(false);
	}
	#endregion
	
}
