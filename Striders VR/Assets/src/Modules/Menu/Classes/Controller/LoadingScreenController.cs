using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreenController : MonoBehaviour {

	[SerializeField] private Slider progressBar;

	[SerializeField] private Text percentage;
	[SerializeField] private Text postProgress;

	private bool turnOnLoadingScreen;
	private bool waitingForLoad;
	private bool waitingForAnyKey;

	private string sceneName = "";

	private int loadProcess;

	private AsyncOperation async;

	private void loadingScreen()
	{
		if (this.turnOnLoadingScreen) 
		{
			this.turnOnLoadingScreen = false;
			async = Application.LoadLevelAsync (this.sceneName);
			this.async.allowSceneActivation = false;
			this.waitingForLoad = true;
			//StartCoroutine(this.displayLoadingScreen());
		}

		if (this.waitingForLoad) 
		{
			if(async.progress < 0.9f)
			{
				this.loadProcess = (int) (async.progress * 100);
				this.percentage.text = this.loadProcess.ToString() + "%";
				this.progressBar.value = async.progress;
			}
			else if(async.progress >= 0.9f && !this.waitingForAnyKey)
			{
				this.percentage.text = "100%";
				this.progressBar.value = 1;
				this.postProgress.gameObject.SetActive(true);
				this.waitingForAnyKey = true;
				this.waitingForLoad = false;
			}
		}

		if (Input.GetMouseButton(0) && this.waitingForAnyKey) 
		{
			async.allowSceneActivation = true;
		}
	}


	#region Script
	void Awake () 
	{
		this.turnOnLoadingScreen = false;
		this.waitingForLoad = false;
		this.waitingForAnyKey = false;
		this.loadProcess = 0;
	}

	void Update () 
	{
		this.loadingScreen ();
	}
	#endregion

	#region Properties
	public bool TurnOnLoadingScreen
	{
		set { this.turnOnLoadingScreen = value; }
	}

	public string SceneName
	{
		set { this.sceneName = value; }
	}
	#endregion
}
