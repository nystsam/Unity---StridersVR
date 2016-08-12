using UnityEngine;
using System.Collections;
using Leap;

public class HandModelExampleController : MonoBehaviour {

	public GameObject modelExampleContainer;
	
	private Controller leapController;
	
	private Frame frame;

	private Animator anim;

	private int allowToRotateHash;

	private bool isModelExampleEnabled = false;
	private bool allowToDetectPalm = false;

	private HandList hands;

	private Hand myHand;
	
	private bool isHand(Collider other)
	{
		if (other.GetComponentInParent<HandController> ())
			return true;
		return false;
	}

	private void disableModel()
	{
		this.allowToDetectPalm = false;
		this.isModelExampleEnabled = false;
		this.anim.SetBool(this.allowToRotateHash, false);
		this.modelExampleContainer.SetActive(false);
	}

	private void palmPosition()
	{
		if (this.allowToDetectPalm) 
		{
			if (myHand.PalmNormal.y > 0.7f) 
			{
				Vector3 posBall = myHand.PalmPosition.ToUnity ().normalized;
				posBall.y -= 0.25f;
			
				if (!isModelExampleEnabled) 
				{
					this.modelExampleContainer.SetActive (true);
					this.anim.SetBool (this.allowToRotateHash, true);
					this.isModelExampleEnabled = true;
				}
				this.modelExampleContainer.transform.localPosition = posBall;
			} 
			else if (this.isModelExampleEnabled && myHand.PalmNormal.y < 0.7f) 
			{
				this.disableModel ();
			}
		}
	}
	
	
	#region Script
	void Start () 
	{
		this.anim = this.modelExampleContainer.GetComponent<Animator> ();
		this.allowToRotateHash = Animator.StringToHash("AllowToRotate");
		
		this.leapController = new Controller ();
	}
	
	
	void Update () 
	{
		this.frame = this.leapController.Frame ();
		this.hands = frame.Hands;
		this.myHand = hands [0];
		this.palmPosition ();
	}

	void OnTriggerEnter(Collider other)
	{
		if (this.isHand (other)) 
		{	
			if (myHand.IsLeft) 
			{
				this.allowToDetectPalm = true;
			}
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if (this.isHand (other)) 
		{
			if(this.isModelExampleEnabled)
			{
				this.disableModel();
			}		
		}
	}
	#endregion

}
