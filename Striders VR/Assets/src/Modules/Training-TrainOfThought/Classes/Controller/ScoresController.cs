using UnityEngine;
using UnityEngine.UI;
using System;
using StridersVR.Modules.TrainOfThought.Logic.Representatives;

public class ScoresController : MonoBehaviour {

	public Text timingMin;
	public Text timingSeg;
	public Text countingCurrent;
	public Text countingTotal;
	public float trainTimeInSeconds;

	private TimeSpan trainTimer;
	private RepresentativeTrainScore trainScore;

	#region Script
	void Awake () 
	{
		this.trainScore = new RepresentativeTrainScore (ref this.countingCurrent, ref this.countingTotal);
		this.trainTimer = TimeSpan.FromSeconds (trainTimeInSeconds);
		this.timingMin.text = this.trainTimer.Minutes.ToString();
	}

	void Update () 
	{
		if(this.trainTimer.Seconds == 59)
			this.timingMin.text = this.trainTimer.Minutes.ToString();
		
		this.trainTimeInSeconds -= Time.deltaTime;
		this.trainTimer = TimeSpan.FromSeconds (trainTimeInSeconds);
		this.timingSeg.text = this.trainTimer.Seconds.ToString ("00");
	}
	#endregion

	#region Properties
	public RepresentativeTrainScore TrainScore
	{
		get { return this.trainScore; }
	}

	public TimeSpan TrainTimer
	{
		get { return this.trainTimer; }
	}
	#endregion
}
