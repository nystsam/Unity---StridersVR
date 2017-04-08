using UnityEngine;
using System.Collections;
using StridersVR.Domain;

public class SessionController : MonoBehaviour {

	public GameObject LoginPanel;
	public GameObject MenuContainer;
	public GameObject ResultMenu;

	public static SessionController Current;

	public SessionController()
	{
		Current = this;
	}

	public void SetUser(User newUser)
	{
		GameObject.FindGameObjectWithTag ("StaticUser").GetComponent<StaticUserController> ().User = newUser;
		MenuStatisticsController.Current.CurrentUser = newUser;
		this.LoginPanel.SetActive(false);
		this.ShowMainMenu();
		this.ChangeView(true);
	}

	public void ClearUser()
	{
		GameObject.FindGameObjectWithTag ("StaticUser").GetComponent<StaticUserController> ().User = null;
		MenuStatisticsController.Current.CurrentUser = null;
		this.LoginPanel.SetActive(true);
		this.ResetContainers();
		this.ChangeView(false);
	}

	private void ChangeView(bool isLogIn)
	{
		CameraUITools.Current.ChangePosition(isLogIn);	
	}

	private void ResetContainers(){

		this.ResultMenu.SetActive(false);
		foreach(Transform _menu in this.MenuContainer.transform)
		{
			_menu.gameObject.SetActive(false);
		}
	}

	private void ShowMainMenu(){
		foreach(Transform _menu in this.MenuContainer.transform)
		{
			if(_menu.name.Equals("MainMenu"))
			{
				_menu.gameObject.SetActive(true);
				break;
			}
		}
	}

	#region Script
	void Start () 
	{
		this.ResetContainers();
		if(GameObject.FindGameObjectWithTag ("StaticUser").GetComponent<StaticUserController> ().User != null)
		{
			this.LoginPanel.SetActive(false);
			this.ShowMainMenu();
			this.ChangeView(true);
		}

	}

	void Update () 
	{
	
	}
	#endregion
}
