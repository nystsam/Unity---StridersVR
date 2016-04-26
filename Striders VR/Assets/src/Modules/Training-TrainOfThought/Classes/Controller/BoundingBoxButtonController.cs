using UnityEngine;
using System.Collections;

public class BoundingBoxButtonController : MonoBehaviour {

	public float alphaSmoothValue;
	public GameObject buttonRenderer;
	
	private bool isTriggered = false;
	private bool changeColorDone = true;
	private Material myMaterial;
	private Color myColor;
	private GameObject attachedRailroadSwitch;

	private void TriggerBoundingBox()
	{
		if (isTriggered && !changeColorDone) {
			this.changeColor (alphaSmoothValue, 1, 1);
			this.turnSwitchLight(0.25f,3.5f,1);
			
		} 
		else if (!isTriggered && !changeColorDone) {
			this.changeColor (-alphaSmoothValue, 0.2f, -1);
			this.turnSwitchLight(-0.25f,0,-1);
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
	
	private void turnSwitchLight(float valueIncrement, float valueToStop, float numericDirection)
	{
		Transform objLight = this.attachedRailroadSwitch.transform.FindChild("HoverLight");
		Light light = objLight.GetComponent<Light> ();
		valueToStop = valueToStop * numericDirection;
		if((light.range * numericDirection) <= valueToStop )
			light.range += valueIncrement;
	}

	private bool isHandController(Collider other)
	{
		if (other.transform.parent.GetComponentInParent<HandModel> ()) 
		{
			return true;
		}

		return false;
	}


	#region Script
	void Start () 
	{
		myMaterial = buttonRenderer.GetComponent<MeshRenderer> ().material;
		myColor = myMaterial.color;
		myMaterial.color = new Color (myColor.r, myColor.g, myColor.b, 0.2f);
	}

	void Update () 
	{
		this.TriggerBoundingBox();
	}
	
	void OnTriggerStay(Collider other)
	{

		if(this.isHandController(other))
		{
/*
		if (other.tag.Equals ("Player")) {
*/
			if (!isTriggered) {
				this.isTriggered = true;
				changeColorDone = false;
			}
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if(this.isHandController(other))
		{
		/*
		if (other.tag.Equals ("Player")) {
		*/
			if (isTriggered) {
				this.isTriggered = false;
				changeColorDone = false;
			}
		}
	}
	#endregion

	#region Properties
	public GameObject AttachedRailroadSwitch
	{
		set { this.attachedRailroadSwitch = value; }
	}

	public void changeDirectionIndex()
	{
		this.attachedRailroadSwitch.GetComponent<RailroadSwitchController> ().changeDirectionIndex ();
	}
	#endregion
}
