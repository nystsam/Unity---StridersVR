using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScoreDotsController : MonoBehaviour {

	public GameObject modelGenerator;
	public GameObject timingMin;
	public GameObject timingSeg;
	public GameObject countingCurrent;
	public GameObject countingTotal;
	public GameObject exampleRevealed;
	public GameObject startTime;
	
	[SerializeField] private float gameTimeInSeconds;
	[SerializeField] private float timeToStart;
	
	private bool isGameBegin;
	private bool isGameTimerEnd;
	
	private TimeSpan gameTimer;

	private Dictionary<int, int> exampleRevealedList;


	public void setScore(bool isCorrect)
	{
		int _current = int.Parse(this.countingCurrent.GetComponent<Text>().text);
		int _total = int.Parse(this.countingTotal.GetComponent<Text>().text);
		
		if(isCorrect)
		{
			_current ++;
		}
		
		_total ++;
		this.countingCurrent.GetComponent<Text>().text = _current.ToString();
		this.countingTotal.GetComponent<Text>().text = _total.ToString();
	}

	public void addReveal()
	{
		int _count = int.Parse(this.exampleRevealed.GetComponent<Text>().text);
		int _currentModel = int.Parse(this.countingTotal.GetComponent<Text>().text) + 1;

		_count ++;

		this.exampleRevealed.GetComponent<Text>().text = _count.ToString();
		this.exampleRevealedList[_currentModel] = _count;
	}

	public void newModel()
	{
		int _totalModels = int.Parse(this.countingTotal.GetComponent<Text>().text) + 1;

		this.exampleRevealedList.Add(_totalModels, 0);

		this.exampleRevealed.GetComponent<Text>().text = "0";
	}

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
		if(this.isGameBegin && this.gameTimeInSeconds <= 0)
		{
			this.isGameTimerEnd = true;
			int _current = int.Parse(this.countingCurrent.GetComponent<Text>().text);
			int _total = int.Parse(this.countingTotal.GetComponent<Text>().text);

			CameraUITools.Current.ChangePosition(true);
			StatisticsDotToDotController.Current.gameObject.SetActive(true);
			StatisticsDotToDotController.Current.SetResults(_current, _total);
		}	
		else if (this.isGameBegin) 
		{
			if (this.gameTimer.Seconds == 59)
				this.timingMin.GetComponent<Text>().text = "0" + this.gameTimer.Minutes.ToString ();
			
			this.gameTimeInSeconds -= Time.deltaTime;
			this.gameTimer = TimeSpan.FromSeconds (this.gameTimeInSeconds);
			this.timingSeg.GetComponent<Text>().text = this.gameTimer.Seconds.ToString ("00");
		}
	}

	#region Script
	void Awake () 
	{
		this.isGameBegin = false;
		this.isGameTimerEnd = false;
		this.gameTimer = TimeSpan.FromSeconds (this.gameTimeInSeconds);

		this.timingMin.GetComponent<Text>().text = "0" + this.gameTimer.Minutes.ToString ();
		this.timingSeg.GetComponent<Text>().text = this.gameTimer.Seconds.ToString ("00");

		this.exampleRevealedList = new Dictionary<int, int>();
	}

	void Update () 
	{
		if(!this.isGameTimerEnd)
		{
			this.getReady ();
			this.timing ();
		}
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
