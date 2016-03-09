using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using StridersVR.Modules.Menu.Logic;
//using UnityEngine.SceneManagement;

public class MenuCamera : MonoBehaviour {

	public Transform currentMount;
	public float speedFactor = 0.1f;
	public Text trainingName;
	public GameObject loadingScreen;

	private ContextMenuCamera menuCamera;
	private Transform newMount = null;


	#region script	
	void Start(){

		this.menuCamera = new ContextMenuCamera (this.trainingName);
		this.menuCamera.changeToTrainingState ();
		this.menuCamera.startFeatures ();
	}

	void Update () {
	
		transform.position = Vector3.Lerp (transform.position, currentMount.position, speedFactor);
		transform.rotation = Quaternion.Slerp (transform.rotation, currentMount.rotation, speedFactor);

	}
	#endregion


	public void setUsername(string newUser)
	{
		this.menuCamera.ActualUser = newUser;
	}

	public void setCameraMount()
	{
		this.currentMount = newMount;
		this.newMount = null;
	}

	public void nextTraining()
	{
		this.menuCamera.setNextTraining ();
	}

	public void previousTraining ()
	{
		this.menuCamera.setPreviousTraining ();
	}

    	public void startTraining()
	{
		this.loadingScreen.SetActive (true);
		this.loadingScreen.GetComponent<LoadingScreenController> ().SceneName = this.menuCamera.getTrainingName;
		this.loadingScreen.GetComponent<LoadingScreenController> ().TurnOnLoadingScreen = true;
		//Application.LoadLevel (this.menuCamera.getTrainingName);
		//SceneManager.LoadScene (1);
    	}



	#region States
	public void setLoginState(Transform newMount)
	{
		this.menuCamera.changeToLoginState ();
		if (this.menuCamera.startFeatures ()) 
		{
			this.newMount = newMount;
			this.setCameraMount();
		} else 
		{
			this.menuCamera.loginFailure (GameObject.FindGameObjectWithTag ("LoginMessage").GetComponent<Text> ());
		}
	}

	public void setLogoutState(Transform newMount)
	{
		this.menuCamera.changeToLogoutState ();
		this.menuCamera.startFeatures ();
		this.newMount = newMount;
		this.setCameraMount();
	}

	public void setTrainingState(Transform newMount)
	{
		this.menuCamera.changeToTrainingState ();
		this.menuCamera.startFeatures ();
		this.newMount = newMount;
		this.setCameraMount();
	}

	public void setStatisticsState(Transform newMount)
	{
		this.menuCamera.changeToLogoutState ();
		this.menuCamera.startFeatures ();
		this.newMount = newMount;
		this.setCameraMount();
	}

	public void setPreviousState(Transform newMount)
	{
		this.menuCamera.changeToLoginState ();
		this.newMount = newMount;
		this.setCameraMount ();
	}
	#endregion



}
