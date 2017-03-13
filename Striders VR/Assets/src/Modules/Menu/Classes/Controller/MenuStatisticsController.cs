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

	private int userId;

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
		this.ResultMenu.transform.position = new Vector3(2,7.5f,6.5f);
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
		this.localManager.GetLastPlays(this.userId, this.currentTraining.Id);
		this.setPanelInfo();
	}

	public void GetTodayPlays()
	{
		this.localManager.GetTodayPlays(this.userId, this.currentTraining.Id);
		this.setPanelInfo();
	}

	public void GetYesterdayPlays()
	{
		this.localManager.GetYesterdayPlays(this.userId, this.currentTraining.Id);
		this.setPanelInfo();
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

		this.userId = GameObject.FindGameObjectWithTag("StaticUser").GetComponent<StaticUserController>().User.Id;

		this.StatisticsSelectionMenu.SetActive(false);
		this.ResultMenu.gameObject.SetActive(false);
	}
	#endregion
	
}
