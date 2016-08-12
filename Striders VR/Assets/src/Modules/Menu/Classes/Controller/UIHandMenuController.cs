using UnityEngine;
using Leap;
using System.Collections;

public class UIHandMenuController : MonoBehaviour {

	private bool isButtonActive = false;
	private bool isInstantiated = false;
	private bool isHoving = false;
	private bool isPanelUp = false;
	private bool isPanelButtonPressed = true;

	private GameObject UIbutton;
	private GameObject UIOptions;

	private UIButtonActions buttonReset;
	private UIButtonActions buttonExit;
	private UIButtonActions buttonClose;

	private Transform indexBone3;
 
	private HandModel hand;

	private void createMenuOptions()
	{
		GameObject _uiToolsPrefab;

		_uiToolsPrefab = Resources.Load ("Prefabs/Menu/ToolsPanelUI", typeof(GameObject)) as GameObject;
		
		this.UIOptions = (GameObject)GameObject.Instantiate (_uiToolsPrefab, Vector3.zero, _uiToolsPrefab.transform.rotation);
		
		this.UIOptions.transform.position = new Vector3(-0.2f, 1.6f, 0.1f);
		//			this.UIOptions.transform.parent = this.hand.palm;
		//			this.UIOptions.transform.localPosition = new Vector3(-4.85f, 0, -3f);
		//			this.UIOptions.transform.localRotation = Quaternion.Euler(new Vector3(280, 0, 180));
		//			this.UIOptions.transform.localScale = new Vector3(4.8f, 2.75f, 4.8f);
		
		
		this.buttonReset = this.UIOptions.transform.FindChild("PanelButtons").GetComponentInChildren<UIButtonResetController>();
		this.buttonExit = this.UIOptions.transform.FindChild("PanelButtons").GetComponentInChildren<UIButtonExitController>();
		this.buttonClose = this.UIOptions.transform.FindChild("PanelButtons").GetComponentInChildren<UIButtonCloseController>();
		
		this.UIOptions.SetActive(false);

	}

	private void createHandUI()
	{
		if (this.hand.GetLeapHand ().IsLeft) 
		{
			GameObject _uiPrefab;
			
			_uiPrefab = Resources.Load ("Prefabs/Menu/OptionUI", typeof(GameObject)) as GameObject;
			
			this.UIbutton = (GameObject)GameObject.Instantiate (_uiPrefab, Vector3.zero, Quaternion.identity);
			this.UIbutton.transform.parent = this.hand.palm;
			this.UIbutton.transform.localPosition = new Vector3 (-1f, 0, -3f);
			this.UIbutton.transform.localRotation = Quaternion.Euler(new Vector3(-90,0,0));
			this.UIbutton.transform.localScale = new Vector3(0.6f, 0.6f, 0.001f);
			this.UIbutton.SetActive (false);

			this.isPanelButtonPressed = false;
			this.isInstantiated = true;
		}
		else if (this.hand.GetLeapHand ().IsRight) 
		{
			GameObject _clone, _indexColliderPrefab;

			_indexColliderPrefab = Resources.Load("Prefabs/Menu/IndexColliderUI", typeof(GameObject)) as GameObject;

			_clone = (GameObject)GameObject.Instantiate (_indexColliderPrefab, Vector3.zero, Quaternion.identity);
			_clone.transform.parent = this.hand.fingers [1].bones [3];
			_clone.transform.localPosition = Vector3.zero;
			_clone.GetComponent<SphereCollider>().radius = 0.1f;
		}
	}

	private void enableUIButtonTools()
	{
		if(this.isInstantiated)
		{
//			if (this.hand.GetPalmRotation().x <= -0.7f && 
//			    !this.isButtonActive) 
//			{
//				this.UIbutton.SetActive (true);
//				this.isButtonActive = true;
//			} 
//			else if(this.hand.GetPalmRotation().x >= -0.7f &&
//			        this.isButtonActive)
//			{
//				this.UIbutton.SetActive (false);
//				this.UIbutton.GetComponent<UIButtonTools>().resetButton();
//				this.isButtonActive = false;
//
//				if(this.isPanelUp)
//				{
//					this.UIOptions.SetActive(false);
//					this.UIbutton.GetComponent<UIButtonTools> ().resetButton();
//					this.isPanelUp = false;
//				}
//			}

			if (this.hand.GetPalmRotation().x >= -0.7f && 
			    !this.isButtonActive) 
			{
				this.UIbutton.SetActive (true);
				this.isButtonActive = true;
			} 
			else if(this.hand.GetPalmRotation().x <= -0.7f &&
			        this.isButtonActive)
			{
				this.UIbutton.SetActive (false);
				this.UIbutton.GetComponent<UIButtonTools>().resetButton();
				this.isButtonActive = false;

//				if(this.isPanelUp)
//				{
//					this.UIOptions.SetActive(false);
//					this.UIbutton.GetComponent<UIButtonTools> ().resetButton();
//					this.isPanelUp = false;
//				}
			}
		}
	}

	private void checkUIButtonToolsPress()
	{
		if (this.hand.GetLeapHand ().IsLeft && !this.isPanelUp) 
		{
			if(this.UIbutton.GetComponent<UIButtonTools> ().IsPressed)
			{
				this.createMenuOptions();
				this.isPanelUp = true;
				this.UIOptions.SetActive(true);
				this.UIbutton.GetComponent<UIButtonTools> ().resetButton();
			}
		}
	}

	private void pressButtons()
	{
		if (!this.isPanelButtonPressed) 
		{
			if(this.buttonReset != null && this.buttonReset.buttonPressed())
			{
				this.isPanelButtonPressed = true;
				// ESPERAR UN RATO O HACER ALGO
				this.buttonReset.buttonAction(this.UIOptions);
			}
			else if(this.buttonClose != null && this.buttonClose.buttonPressed())
			{
				this.isPanelButtonPressed = true;
				this.buttonClose.buttonAction(this.UIOptions);

				GameObject.Destroy(this.UIOptions);

				this.buttonClose = null;
				this.buttonExit = null;
				this.buttonReset = null;
				this.isPanelUp = false;
				this.isPanelButtonPressed = false;
			}
		}
	}


	#region Script
	void Start () 
	{
		this.hand = this.transform.GetComponent<HandModel> ();
		this.createHandUI ();
	}

	void Update () 
	{
		this.enableUIButtonTools ();
		this.checkUIButtonToolsPress ();
		this.pressButtons ();
	
	}
	#endregion
}
