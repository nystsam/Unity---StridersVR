using UnityEngine;
using System.Collections;

public class StripeModelController : MonoBehaviour {

	public GameObject handle;
	public GameObject endStripe;

	public void setEndStripe()
	{
		Transform _handleParent = handle.transform.parent;
		
		handle.transform.parent = transform;
		endStripe.transform.localPosition = handle.transform.localPosition;
		endStripe.SetActive(true);
		
		handle.transform.parent = _handleParent;
	}

	#region Script
	void Start () 
	{
		this.setEndStripe ();
	}
	#endregion
}
