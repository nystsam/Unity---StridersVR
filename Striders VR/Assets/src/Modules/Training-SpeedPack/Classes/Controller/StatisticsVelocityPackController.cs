using UnityEngine;
using System.Collections.Generic;
using StridersVR.Domain.SpeedPack;

public class StatisticsVelocityPackController : MonoBehaviour {

	public static StatisticsVelocityPackController Current;

	public TextMesh Total;
	public TextMesh Hits;
	public TextMesh Errors;
	public TextMesh AverageTime;
	public TextMesh Score;

	private List<GameObject> activityBars;

	private StatisticsVelocityPack statistics;

	private List<ActivityVelocityPack> packageResult;

	public StatisticsVelocityPackController()
	{
		Current = this;
	}

	public void addNewResult(ActivityVelocityPack result)
	{
		this.packageResult.Add(result);
	}

	public void SetResults(int success, int total)
	{
		float _art;

		/*
		success = 7;
		total = 10;
		this.packageResult = new List<ActivityVelocityPack>();
		ActivityVelocityPack r1 = new ActivityVelocityPack();
		r1.setTimeComplete(3.43223f);
		r1.Score = 350;
		r1.IsCorrect = true;
		this.packageResult.Add(r1);

		ActivityVelocityPack r2 = new ActivityVelocityPack();
		r2.setTimeComplete(8.43223f);
		r2.Score = 175;
		r2.IsCorrect = true;
		this.packageResult.Add(r2);

		ActivityVelocityPack r3 = new ActivityVelocityPack();
		r3.setTimeComplete(5.123f);
		r3.Score = 150;
		r3.IsCorrect = true;
		this.packageResult.Add(r3);

		ActivityVelocityPack r4 = new ActivityVelocityPack();
		r4.setTimeComplete(5.123f);
		r4.Score = 150;
		r4.IsCorrect = false;
		this.packageResult.Add(r4);

		ActivityVelocityPack r5 = new ActivityVelocityPack();
		r5.setTimeComplete(2.523f);
		r5.Score = 250;
		r5.IsCorrect = false;
		this.packageResult.Add(r5);

		ActivityVelocityPack r6 = new ActivityVelocityPack();
		r6.setTimeComplete(7.123f);
		r6.Score = 250;
		r6.IsCorrect = true;
		this.packageResult.Add(r6);

		ActivityVelocityPack r7 = new ActivityVelocityPack();
		r7.setTimeComplete(3.75f);
		r7.Score = 175;
		r7.IsCorrect = true;
		this.packageResult.Add(r7);

		ActivityVelocityPack r8 = new ActivityVelocityPack();
		r8.setTimeComplete(6.1f);
		r8.Score = 150;
		r8.IsCorrect = true;
		this.packageResult.Add(r8);

		ActivityVelocityPack r9 = new ActivityVelocityPack();
		r9.setTimeComplete(9.81f);
		r9.Score = 250;
		r9.IsCorrect = true;
		this.packageResult.Add(r9);

		ActivityVelocityPack r0 = new ActivityVelocityPack();
		r0.setTimeComplete(4.81f);
		r0.Score = 150;
		r0.IsCorrect = true;
		this.packageResult.Add(r0);
		*/

		this.calculateResults();
		
		this.Total.text = total.ToString();
		this.Hits.text = success.ToString();
		this.Errors.text = (total - success).ToString();
		
		this.statistics.calculateResults(success, total);
		_art = this.statistics.GetAverageTime();
		this.AverageTime.text = _art.ToString("F2") + " seg.";
		
		this.activityBars[0].GetComponent<UIActivityBarController>().begin(this.statistics.GetAgility());
		this.activityBars[1].GetComponent<UIActivityBarController>().begin(this.statistics.GetPerception());
		this.Score.text = this.statistics.GetScore().ToString();
		
		this.statistics.saveStatistics();

	}

	private void calculateResults()
	{
		foreach(ActivityVelocityPack adtd in this.packageResult)
		{
			this.statistics.SetDataPerModel(adtd);
		}
	}


	private void loadActivities()
	{
		GameObject _prefab = Resources.Load("Prefabs/Menu/ActivityWithBar", typeof(GameObject)) as GameObject;
		float y = 0.2f;
		int _index = 0;
		foreach(string ac in this.statistics.Activities)
		{
			GameObject _clone;
			
			_clone = GameObject.Instantiate(_prefab);
			_clone.transform.parent = this.transform.GetChild(0);
			_clone.transform.localPosition = new Vector3(-0.1f,y,0);
			_clone.transform.rotation = Quaternion.Euler(0,180,0);
			if(_index == 0)
			{
				_clone.GetComponent<TextMesh>().text = ac;
			}
			else
			{
				_clone.GetComponent<TextMesh>().text = "Nivel de " + ac;
			}

			
			this.activityBars.Add(_clone);
			y -= 0.5f;
		}
	}

	#region Script
	void Start () 
	{
		this.packageResult = new List<ActivityVelocityPack>();
		this.activityBars = new List<GameObject>();
		this.statistics = new StatisticsVelocityPack();
		this.loadActivities();
		this.gameObject.SetActive(false);
	}
	#endregion
}
