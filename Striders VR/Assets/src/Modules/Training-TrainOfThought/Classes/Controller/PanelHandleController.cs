using UnityEngine;
using System.Collections;

public class PanelHandleController : MonoBehaviour {

	[SerializeField] private Vector3 velocity;
	[SerializeField] private Vector3 distance;

	[SerializeField] private float smoothTime;
	[SerializeField] private float timeBeforeStop = 1f;

	private bool allowToMove = false;
	private bool cancelMove = false;

	private HandGrabStrength rightHand;

	private IEnumerator waitingForHand()
	{
		yield return new WaitForSeconds(this.timeBeforeStop);
		if(this.cancelMove)
		{
			this.allowToMove = false;
			SwitchesPanelController.Current.AllowToMove(false);
			this.rightHand = null;
			Debug.Log ("Salio");
		}
	}

	#region Script
	void Update () 
	{
		if(this.allowToMove && this.rightHand != null)
		{
			if(this.rightHand.IsGrabbing())
			{
				Vector3 _newPosition = SwitchesPanelController.Current.GetPanelPosition();
				
				_newPosition = Vector3.SmoothDamp(_newPosition, this.rightHand.transform.position + this.distance, 
				                                  ref this.velocity, this.smoothTime);
				
				SwitchesPanelController.Current.SetPanelPosition(_newPosition);
			}
		}
	}

	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.GetComponentInParent<HandGrabStrength>() != null)
		{
			//Reivsar el panel UI si esta abierto para q no haga el siguiente codigo
			this.rightHand = other.gameObject.GetComponentInParent<HandGrabStrength>();
			if(this.rightHand.IsRightHand())
			{
				this.cancelMove = false;	
				SwitchesPanelController.Current.AllowToMove(true);
				this.allowToMove = true;
			}
			else
				this.rightHand = null;
		}
	}

	void OnCollisionExit(Collision other)
	{
		if(other.gameObject.GetComponentInParent<HandGrabStrength>() != null && this.allowToMove)
		{
			this.allowToMove = false;
			SwitchesPanelController.Current.AllowToMove(false);
			this.rightHand = null;
		}
	}
	#endregion
}
