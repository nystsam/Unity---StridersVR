using UnityEngine;
using System.Collections;

public class UIMenuOptions : MonoBehaviour {

	private bool isActive;

	private UIButtonActions buttonReset;
	private UIButtonActions buttonExit;
	private UIButtonActions buttonClose;

	public bool activeMenu()
	{
		if (!this.isActive) 
		{
			this.isActive = true;
			this.gameObject.SetActive(true);

			return true;
		}

		return false;
	}

	public void desactiveMenu()
	{
		this.isActive = false;
		this.gameObject.SetActive (false);
	}

	#region Script
	void Awake () 
	{
		this.isActive = false;

		this.buttonReset = this.transform.FindChild("PanelButtons").GetComponentInChildren<UIButtonResetController>();
		this.buttonExit = this.transform.FindChild("PanelButtons").GetComponentInChildren<UIButtonExitController>();
		this.buttonClose = this.transform.FindChild("PanelButtons").GetComponentInChildren<UIButtonCloseController>();

		this.gameObject.SetActive(false);
	}

	void Update () 
	{
	
	}
	#endregion
}
