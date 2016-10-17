using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using StridersVR.Domain;
using StridersVR.Modules.Menu.Logic;

public class UIButtonPager : MonoBehaviour {

	public Image buttonImage;

	public bool isNext;

	public GameObject trainingList;
	
	private bool isPressed = false;
	//private bool colorChanged = false;

	private float triggerDistance = 0.15f;

	private VirtualButton virtualButton;


	private void buttonPressed ()
	{
		if (!this.isPressed && this.virtualButton.IsButtonPressed (this.transform.localPosition, this.triggerDistance)) 
		{
			this.isPressed = true;
			if(this.isNext)
			{
				this.trainingList.GetComponent<UIButtonTrainingList>().nextTraining();
			}
			else
			{
				this.trainingList.GetComponent<UIButtonTrainingList>().previousTraining();
			}
		} 
		else if (this.isPressed && this.virtualButton.IsButtonReleased (this.transform.localPosition, this.triggerDistance)) 
		{	
			this.isPressed = false;
		}
	}


	#region Script
	void Awake () 
	{
		this.virtualButton = new VirtualButton (this.transform.localPosition, 300, Vector3.forward);
	}

	void Update () 
	{
		this.transform.localPosition = this.virtualButton.ConstraintMovement (this.transform.localPosition, 0f, 0.2f);
		this.GetComponent<Rigidbody> ().AddRelativeForce(this.virtualButton.ApplyRelativeSpring (this.transform.localPosition));
		
		this.buttonPressed ();
	}

//	void OnCollisionEnter(Collision other)
//	{
//		if (other.collider.GetComponentInParent<HandController> ()) 
//		{
//			if(!this.colorChanged)
//			{
//				this.colorChanged = true;
//				this.buttonImage.color = new Color(1,1,1,1);
//			}
//		}
//	}
//
//	void OnCollisionExit(Collision other)
//	{
//		if (other.collider.GetComponentInParent<HandController> ()) 
//		{
//			if(this.colorChanged)
//			{
//				this.colorChanged = false;
//				this.buttonImage.color = new Color(1,1,1,0.275f);
//			}
//		}
//	}
	#endregion
}
