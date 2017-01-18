using UnityEngine;
using UnityEngine.UI;
using System;
using StridersVR.Modules.TrainOfThought.Logic.Representatives;

public class ScoresController : MonoBehaviour {

	public GameObject platform;
	public GameObject startTimeText;
	public Text timingMin;
	public Text timingSeg;
	public Text countingCurrent;
	public Text countingTotal;
	public float trainTimeInSeconds;
	public float timeToStart;

	private TimeSpan gameTimer;
	private RepresentativeTrainScore trainScore;
	private bool gameBegin = false;
	private bool gameTimerEnd = false;
	private bool isDone = false;

	private void getReady()
	{
		if (this.timeToStart > 0) 
		{
			this.startTimeText.GetComponent<TextMesh> ().text = this.timeToStart.ToString ("F0");
			this.timeToStart -= Time.deltaTime;
		} 
		else if (!this.gameBegin) 
		{
			this.startTimeText.GetComponent<TextMesh> ().text = "...";
			this.gameBegin = true;
			this.platform.GetComponent<PlatformController>().AllowToSpawnTrain = true;
		} 
		else if (this.timeToStart > -2) 
		{
			this.timeToStart -= Time.deltaTime;
		}
		else if (Math.Round(this.timeToStart) == -2) 
		{
			this.startTimeText.GetComponent<TextMesh> ().text = "";
			this.timeToStart = -3;
		}	
	}
	
	private void timing()
	{
		if (this.gameBegin && !this.gameTimerEnd) 
		{
			if (this.gameTimer.Seconds == 59)
				this.timingMin.text = "0" + this.gameTimer.Minutes.ToString ();
			
			this.trainTimeInSeconds -= Time.deltaTime;
			this.gameTimer = TimeSpan.FromSeconds (trainTimeInSeconds);
			this.timingSeg.text = this.gameTimer.Seconds.ToString ("00");
		}
	}
	
	private void timeOut()
	{
		if (this.gameTimer.TotalSeconds <= 0 && !this.gameTimerEnd) 
		{
			this.gameTimerEnd = true;
			this.platform.GetComponent<PlatformController>().AllowToSpawnTrain = false;
		}
		else if (this.gameTimerEnd) 
		{
			if(this.trainScore.remainingTrains() && !this.isDone)
			{
				this.isDone = true;
				int success = int.Parse(this.countingCurrent.text);
				int total = int.Parse(this.countingTotal.text);

				StatisticsFocusRouteController.Current.calculateResults();
				StatisticsFocusRouteController.Current.SetResults(success, total);
			}
		}
	}


	#region Script
	void Awake () 
	{
		this.trainScore = new RepresentativeTrainScore (ref this.countingCurrent, ref this.countingTotal, this.platform);
		this.gameTimer = TimeSpan.FromSeconds (trainTimeInSeconds);
		this.timingMin.text =  "0" + this.gameTimer.Minutes.ToString();
		this.startTimeText.GetComponent<TextMesh> ().text = this.timeToStart.ToString("F0");
	}

	void Update () 
	{
		this.getReady ();
		this.timing ();
		this.timeOut ();
	}
	#endregion

	#region Properties
	public RepresentativeTrainScore TrainScore
	{
		get { return this.trainScore; }
	}

	public TimeSpan TrainTimer
	{
		get { return this.gameTimer; }
	}
	#endregion
}
