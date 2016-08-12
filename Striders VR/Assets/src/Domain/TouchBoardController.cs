using UnityEngine;
using StridersVR.Domain;
using System.Collections;

public class TouchBoardController : MonoBehaviour, ITouchBoard {

	public GameObject progressBar;

	private Vector3 currentPosition;

	private bool isHandController(Collider other)
	{
		if (other.GetComponentInParent<HandController> ())
			return true;
		return false;
	}

	#region ITouchBoard
	public void startAction()
	{
		this.progressBar.GetComponent<RProgressBarController> ().startLoading(this.currentPosition);
	}

	public void cancelAction()
	{
		this.progressBar.GetComponent<RProgressBarController>().stopLoading();
	}

	public bool actionComplete()
	{
		return this.progressBar.GetComponent<RProgressBarController> ().IsDone;
	}
	#endregion

	#region Script
	void Start () 
	{
		this.currentPosition = new Vector3 (-5, -5, -5);
	}

	void OnTriggerStay(Collider other)
	{
		if (this.isHandController (other)) 
		{
			if(other.transform.position != this.currentPosition)
			{
				this.currentPosition = other.transform.position;
			}
		}
	}
	#endregion


}
