using UnityEngine;
using System.Collections;

public class BoundingBoxButtonController : MonoBehaviour {

	public float alphaSmoothValue;
	public GameObject buttonRenderer;
	
	private bool isTriggered = false;
	private bool changeColorDone = true;
	private Material myMaterial;
	private Color myColor;


	#region Script
	void Start () 
	{
		myMaterial = buttonRenderer.GetComponent<MeshRenderer> ().material;
		myColor = myMaterial.color;
		myMaterial.color = new Color (myColor.r, myColor.g, myColor.b, 0.2f);
	}

	void Update () 
	{
		this.setMaterialColor();
	}
	
	void OnTriggerStay(Collider other)
	{
		if (other.tag.Equals ("Player")) {
			if (!isTriggered) {
				this.isTriggered = true;
				changeColorDone = false;
			}
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if (other.tag.Equals ("Player")) {
			if (isTriggered) {
				this.isTriggered = false;
				changeColorDone = false;
			}
		}
	}
	#endregion

	private void setMaterialColor()
	{
		if (isTriggered && !changeColorDone) {
			this.changeColor (alphaSmoothValue, 1, 1);
		} 
		else if (!isTriggered && !changeColorDone) {
			this.changeColor (-alphaSmoothValue, 0.2f, -1);
		}
	}

	private void changeColor(float localAlphaValueSmooth, float valueToStop, float numericDirection)
	{
		float currentAlpha = myMaterial.color.a;
		valueToStop = valueToStop * numericDirection;
		if((currentAlpha * numericDirection) <= valueToStop)
			myMaterial.color = new Color(myColor.r, myColor.g, myColor.b, currentAlpha + localAlphaValueSmooth);
		else
			changeColorDone = true;	
	}
}
