using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RProgressBarController : MonoBehaviour {

	public Transform loadingBar;
	public Transform textLoading;

	private float currentAmount;
	[SerializeField] private float speed;

	private bool isDone = false;

	private void loadBar()
	{
		if (this.currentAmount < 100) 
		{
			this.currentAmount += Time.deltaTime * this.speed;
			this.textLoading.GetComponent<Text> ().text = ((int)currentAmount).ToString () + "%";
			this.loadingBar.GetComponent<Image> ().fillAmount = this.currentAmount / 100;
		} 
		else
		{
			this.isDone = true;
			this.gameObject.SetActive (false);
		}
	}

	public void startLoading()
	{
		this.gameObject.SetActive (true);
		this.currentAmount = 0;
		this.isDone = false;
	}

	public void stopLoading()
	{
		this.gameObject.SetActive (false);
	}


	#region Script
	void Awake()
	{
		this.gameObject.SetActive (false);
	}

	void Update () 
	{
		if (!isDone) 
		{
			this.loadBar();
		}
	}
	#endregion

	#region Properties
	public bool IsDone
	{
		get { return this.isDone; }
	}
	#endregion
}
