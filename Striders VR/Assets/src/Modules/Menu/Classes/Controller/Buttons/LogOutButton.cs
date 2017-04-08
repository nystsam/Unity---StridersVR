using UnityEngine;
using System.Collections;

public class LogOutButton : MonoBehaviour {

	[SerializeField] private float sizeTouch = 2f;
	private float sizeCurrent = 1;

	private bool isTouching = false;
	private bool isPressed = false;

	private Vector3 initScale;

	private IEnumerator pressingButton(Collider other)
	{
		yield return new WaitForSeconds (0.25f);
		if (this.isTouching && !this.isPressed)
		{
			this.isPressed = true;
			SessionController.Current.ClearUser();
		}
	}

	#region Script
	void Start () 
	{
		this.initScale = this.transform.localScale;
	}

	void Update () 
	{
		if(this.isTouching)
		{
			if(this.sizeCurrent <= this.sizeTouch)
			{
				Vector3 _current = this.transform.localScale;
				this.sizeCurrent += 0.04f;
				_current.x = this.sizeCurrent;
				_current.y = this.sizeCurrent;
				this.transform.localScale = _current;
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag.Equals ("IndexUI")) 
		{
			this.isTouching = true;
			if(!this.isPressed)
				StartCoroutine(this.pressingButton(other));
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if (other.tag.Equals ("IndexUI")) 
		{
			this.isTouching = false;
			this.isPressed = false;
			this.sizeCurrent = 1f;
			this.transform.localScale = this.initScale;
		}
	}
	#endregion
}
