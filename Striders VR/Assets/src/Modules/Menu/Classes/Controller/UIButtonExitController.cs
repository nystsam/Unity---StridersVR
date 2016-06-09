using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIButtonExitController : MonoBehaviour, UIButtonActions {

	public GameObject buttonText;
	
	private Material colorMain;
	private Material colorHover;
	private Material colorPressed;
	
	private string colorTextMain;
	private string colorTextHover;
	private string colorTextPressed;
	
	private bool isPressed = false;
	
	#region UIAction
	public void buttonHover(bool isHitting)
	{
		Color _textColor;
		
		if (isHitting) 
		{
			this.GetComponent<MeshRenderer>().material = this.colorHover;
			
			Color.TryParseHexString(this.colorTextHover, out _textColor);
			this.buttonText.GetComponent<Text>().color = _textColor;
		}
		else
		{
			this.GetComponent<MeshRenderer>().material = this.colorMain;
			
			Color.TryParseHexString(this.colorTextMain, out _textColor);
			this.buttonText.GetComponent<Text>().color = _textColor;
		}
	}
	
	public void buttonAction()
	{
		
	}

	public bool buttonPressed ()
	{
		return this.isPressed;
	}
	#endregion
	
	#region Script
	void Awake () 
	{
		this.colorMain = Resources.Load ("Materials/MatMainColor", typeof(Material)) as Material;
		this.colorHover = Resources.Load ("Materials/MatTouch", typeof(Material)) as Material;
		this.colorPressed = Resources.Load ("Materials/MatUIButtonPressed", typeof(Material)) as Material;
		this.colorTextMain = "18CAE6FF";
		this.colorTextHover = "F25E21FF";
		this.colorTextPressed = "FFEA23FF";
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals ("IndexUI")) 
		{
			this.buttonHover(true);
		}
	}
	
	void OnTriggerStay(Collider other)
	{
		if (other.tag.Equals ("IndexUI")) 
		{
			if(Vector3.Distance(other.transform.position, this.transform.position) < 0.07f && !this.isPressed)
			{
				Color _textColor;
				
				this.isPressed = true;
				this.GetComponent<MeshRenderer>().material = this.colorPressed;
				
				Color.TryParseHexString(this.colorTextPressed, out _textColor);
				this.buttonText.GetComponent<Text>().color = _textColor;
			}
			else if(Vector3.Distance(other.transform.position, this.transform.position) > 0.08f && this.isPressed)
			{
				Color _textColor;
				
				this.isPressed = false;
				this.GetComponent<MeshRenderer>().material = this.colorHover;
				
				Color.TryParseHexString(this.colorTextHover, out _textColor);
				this.buttonText.GetComponent<Text>().color = _textColor;
			}
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if (other.tag.Equals ("IndexUI")) 
		{
			this.buttonHover(false);
		}
	}
	#endregion
}
