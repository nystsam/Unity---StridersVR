using UnityEngine;
using System.Collections.Generic;
using StridersVR.Domain.TrainOfThought;

public class StatisticsFocusRouteController : MonoBehaviour {

	public static StatisticsFocusRouteController Current;

	public TextMesh Total;
	public TextMesh Hits;
	public TextMesh Errors;
	public TextMesh AverageTime;

	private List<ActivityFocusRoute> trainResults;

	private List<GameObject> activityBars;

	private StatisticsFocusRoute statistics;

	public StatisticsFocusRouteController()
	{
		Current = this;
	}

	public void addNewResult(ActivityFocusRoute result)
	{
		this.trainResults.Add(result);
	}

	public void SetResults(int success, int total)
	{
		float _art;
		this.calculateResults();
		this.Total.text = total.ToString();
		this.Hits.text = success.ToString();
		this.Errors.text = (total - success).ToString();

		_art = this.statistics.GetAverageReactionTime();
		if(_art <= 0)
			this.AverageTime.text = "Óptimo";
		else
			this.AverageTime.text = _art.ToString("F2") + " seg.";

		this.activityBars[0].GetComponent<UIActivityBarController>().begin(this.statistics.GetAttention(success, total));
		this.activityBars[1].GetComponent<UIActivityBarController>().begin(this.statistics.GetConcentration());

		// insertar a base de datos...
	}

	private void calculateResults()
	{
		foreach(ActivityFocusRoute afr in this.trainResults)
		{
			this.statistics.SetDataPerTrain(afr);
		}
	}

	private void loadActivities()
	{
		GameObject _prefab = Resources.Load("Prefabs/Menu/ActivityWithBar", typeof(GameObject)) as GameObject;
		float y = 0.7f;
		foreach(string ac in this.statistics.Activities)
		{
			GameObject _clone;
			
			_clone = GameObject.Instantiate(_prefab);
			_clone.transform.parent = this.transform.GetChild(0);
			_clone.transform.localPosition = new Vector3(-0.1f,y,0);
			_clone.GetComponent<TextMesh>().text = "Nivel de " + ac;

			this.activityBars.Add(_clone);
			y -= 0.5f;
		}
	}

	#region Script
	void Start () 
	{
		this.trainResults = new List<ActivityFocusRoute>();
		this.activityBars = new List<GameObject>();
		this.statistics = new StatisticsFocusRoute();
		this.loadActivities();
		this.gameObject.SetActive(false);
	}	
	#endregion
}
