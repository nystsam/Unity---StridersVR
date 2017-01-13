using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using StridersVR.Domain.Menu;

public class UIMenuOptions : MonoBehaviour {

	public GameObject confirmation;
	public GameObject doActionButton;
	public GameObject actionName;

	public static UIMenuOptions Current;

	private bool isActive;
	private bool disableHandActions = false;
	public bool DisableHandActions
	{
		get { return disableHandActions; }
	}


	public UIMenuOptions()
	{
		Current = this;
	}

	public bool activeMenu()
	{
		if (!this.isActive) 
		{
			this.disableHandActions = true;
			this.isActive = true;
			this.gameObject.SetActive(true);
			this.confirmation.SetActive(false);

			return true;
		}

		return false;
	}

	public void desactiveMenu()
	{
		this.disableHandActions = false;
		this.isActive = false;
		this.gameObject.SetActive (false);
	}

	public void callConfimation(ToolButton actionButton)
	{
		this.confirmation.SetActive(true);
		this.doActionButton.GetComponent<UIButtonConfirmation>().setToolAction(actionButton);
		this.actionName.GetComponent<Text>().text = "¿Está seguro que desea " + actionButton.getActionName() + "?"; 
		this.desactiveMenu();
	}

	#region Script
	void Awake () 
	{
		this.isActive = false;

//		this.buttonReset = this.transform.FindChild("PanelButtons").GetComponentInChildren<UIButtonResetController>();
//		this.buttonExit = this.transform.FindChild("PanelButtons").GetComponentInChildren<UIButtonExitController>();
//		this.buttonClose = this.transform.FindChild("PanelButtons").GetComponentInChildren<UIButtonCloseController>();

		this.gameObject.SetActive(false);
	}

	void Update () 
	{
	
	}
	#endregion
}
