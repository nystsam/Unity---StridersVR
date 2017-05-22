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

	public StatisticsVelocityPackController()
	{
		Current = this;
	}

	public void SetResults(int success, int total)
	{
		float _art;
		/*
		success = 3;
		total = 4;
		this.modelResults = new List<ActivityDotToDot>();
		ActivityDotToDot r1 = new ActivityDotToDot();
		r1.setTimeComplete(16.43223f);
		r1.addTimeRevelation(6.234f);
		r1.addTimeRevelation(4.174f);
		r1.addTimeRevelation(4.834f);
		r1.IsCorrect = true;
		r1.Revelations = 3;
		this.modelResults.Add(r1);

		ActivityDotToDot r2 = new ActivityDotToDot();
		r2.setTimeComplete(21.31223f);
		r2.addTimeRevelation(5.234f);
		r2.addTimeRevelation(4.177f);
		r2.addTimeRevelation(5.834f);
		r2.addTimeRevelation(3.134f);
		r2.IsCorrect = false;
		r2.Revelations = 4;
		this.modelResults.Add(r2);

		ActivityDotToDot r3 = new ActivityDotToDot();
		r3.setTimeComplete(14.43223f);
		r3.addTimeRevelation(2.234f);
		r3.addTimeRevelation(5.174f);
		r3.addTimeRevelation(3.834f);
		r3.IsCorrect = true;
		r3.Revelations = 3;
		this.modelResults.Add(r3);

		ActivityDotToDot r4 = new ActivityDotToDot();
		r4.setTimeComplete(15.43223f);
		r4.addTimeRevelation(3.174f);
		r4.addTimeRevelation(4.8531f);
		r4.IsCorrect = true;
		r4.Revelations = 2;
		this.modelResults.Add(r4);
		*/
		/*
		this.calculateResults();
		
		this.Total.text = total.ToString();
		this.Hits.text = success.ToString();
		this.Errors.text = (total - success).ToString();
		
		this.statistics.calculateResults(success, total);
		_art = this.statistics.GetAverageModelationTime();
		this.AverageTime.text = _art.ToString("F2") + " seg.";
		
		this.activityBars[0].GetComponent<UIActivityBarController>().begin(this.statistics.GetMemory());
		this.activityBars[1].GetComponent<UIActivityBarController>().begin(this.statistics.GetAbstraction());
		this.Revelations.text = this.statistics.GetRevelationsPerModel().ToString();
		
		this.statistics.saveStatistics();
		*/
	}

	private void loadActivities()
	{
		GameObject _prefab = Resources.Load("Prefabs/Menu/ActivityWithBar", typeof(GameObject)) as GameObject;
		float y = 0.2f;
		foreach(string ac in this.statistics.Activities)
		{
			GameObject _clone;
			
			_clone = GameObject.Instantiate(_prefab);
			_clone.transform.parent = this.transform.GetChild(0);
			_clone.transform.localPosition = new Vector3(-0.1f,y,0);
			_clone.transform.rotation = Quaternion.Euler(0,180,0);
			_clone.GetComponent<TextMesh>().text = "Nivel de " + ac;
			
			this.activityBars.Add(_clone);
			y -= 0.5f;
		}
	}

	#region Script
	void Start () 
	{
		//this.modelResults = new List<ActivityDotToDot>();
		this.activityBars = new List<GameObject>();
		this.statistics = new StatisticsVelocityPack();
		this.loadActivities();
		this.gameObject.SetActive(false);
	}
	#endregion
}
