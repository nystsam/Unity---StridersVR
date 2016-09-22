using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIButtonTools : MonoBehaviour {

	public Transform buttonImage;

	private bool isPressed = false;
	private bool isHoving = false;

	private GameObject UIGameController;

	private void hoverImage(bool isHitting)
	{
		if (isHitting) 
		{
			this.buttonImage.GetComponent<RectTransform> ().localScale = new Vector3 (1.75f, 1.75f, 0);
		} 
		else 
		{
			this.buttonImage.GetComponent<RectTransform> ().localScale = new Vector3 (1, 1, 0);
		}
	}

	private IEnumerator pressingButton(Collider other)
	{
		yield return new WaitForSeconds (0.3f);
		if (this.isHoving && !this.isPressed)
		{
			this.isPressed = true;
			this.UIGameController.transform.FindChild("ToolsPanelUI").GetComponent<UIMenuOptions>().activeMenu();
		}
	}

	#region Script
	void Awake()
	{
		this.UIGameController = GameObject.FindGameObjectWithTag ("GameController");

	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals ("IndexUI")) 
		{
			this.isHoving = true;
			this.hoverImage(true);
			if(!this.isPressed)
				StartCoroutine(this.pressingButton(other));
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag.Equals ("IndexUI")) 
		{
			this.isHoving = false;
			this.hoverImage(false);
			this.isPressed = false;
		}
	}
	#endregion
}
