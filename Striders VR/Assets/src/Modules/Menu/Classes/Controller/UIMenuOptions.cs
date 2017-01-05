using UnityEngine;
using System.Collections;
using StridersVR.Domain.Menu;

public class UIMenuOptions : MonoBehaviour {

	public GameObject confirmation;
	public GameObject doActionButton;

	private bool isActive;

//	private UIButtonActions buttonReset;
//	private UIButtonActions buttonExit;
//	private UIButtonActions buttonClose;

	public bool activeMenu()
	{
		if (!this.isActive) 
		{
			this.isActive = true;
			this.gameObject.SetActive(true);
			this.confirmation.SetActive(false);

			return true;
		}

		return false;
	}

	public void desactiveMenu()
	{
		this.isActive = false;
		this.gameObject.SetActive (false);
	}

	public void callConfimation(ToolButton actionButton)
	{
		this.confirmation.SetActive(true);
		this.doActionButton.GetComponent<UIButtonConfirmation>().setToolAction(actionButton);
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
