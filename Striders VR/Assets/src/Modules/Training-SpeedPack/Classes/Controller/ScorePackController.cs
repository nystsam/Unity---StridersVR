using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ScorePackController : MonoBehaviour {

	public GameObject timingMin;
	public GameObject timingSeg;
	public GameObject countingCurrent;
	public GameObject countingTotal;
	public GameObject totalScore;
	public GameObject startTime;

	[SerializeField] private float gameTimeInSeconds;
	[SerializeField] private float timeToStart;

	private bool isGameBegin;
	private bool isGameTimerEnd;

	private TimeSpan gameTimer;

	private IEnumerator disableStarTime()
	{
		yield return new WaitForSeconds(1f);
		this.startTime.SetActive(false);
	}

	private void getReady()
	{
		if(!this.isGameBegin)
		{
			if (this.timeToStart > 0) 
			{
				this.startTime.GetComponent<TextMesh> ().text = this.timeToStart.ToString ("F0");
				this.timeToStart -= Time.deltaTime;
			} 
			else
			{
				this.startTime.GetComponent<TextMesh> ().text = "...";
				this.isGameBegin = true;
				StartCoroutine(this.disableStarTime());
			} 	
		}
	}

	private void timing()
	{
		if (this.isGameBegin && !this.isGameTimerEnd) 
		{
			if (this.gameTimer.Seconds == 59)
				this.timingMin.GetComponent<Text>().text = "0" + this.gameTimer.Minutes.ToString ();
			
			this.gameTimeInSeconds -= Time.deltaTime;
			this.gameTimer = TimeSpan.FromSeconds (this.gameTimeInSeconds);
			this.timingSeg.GetComponent<Text>().text = this.gameTimer.Seconds.ToString ("00");
		}
	}

	public void setScore(bool isSuccesses, int newScore)
	{
		int _current, _total, _score;

		if (isSuccesses) 
		{
			_current = int.Parse(this.countingCurrent.GetComponent<Text>().text);
			_current ++;

			_score = int.Parse(this.totalScore.GetComponent<Text>().text);
			_score += newScore;

			this.countingCurrent.GetComponent<Text>().text = _current.ToString();
			this.totalScore.GetComponent<Text>().text = _score.ToString();
		}

		_total = int.Parse(this.countingTotal.GetComponent<Text>().text);
		_total ++;

		this.countingTotal.GetComponent<Text> ().text = _total.ToString ();
	}


	#region Script
	void Awake () 
	{
		this.isGameBegin = false;
		this.isGameTimerEnd = false;
		this.gameTimer = TimeSpan.FromSeconds (this.gameTimeInSeconds);

		this.gameTimer = TimeSpan.FromSeconds (this.gameTimeInSeconds);
		this.timingMin.GetComponent<Text>().text = "0" + this.gameTimer.Minutes.ToString ();
	}

	void Update () 
	{
		this.getReady ();
		this.timing ();
	}
	#endregion

	#region Properties
	public bool IsGameBegin
	{
		get { return this.isGameBegin; }
	}

	public bool IsGameTimerEnd
	{
		get { return this.isGameTimerEnd; }
	}
	#endregion
}
