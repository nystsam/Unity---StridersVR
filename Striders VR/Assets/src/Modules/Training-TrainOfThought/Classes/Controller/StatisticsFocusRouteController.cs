using UnityEngine;
using System.Collections.Generic;
using StridersVR.Domain.TrainOfThought;

public class StatisticsFocusRouteController : MonoBehaviour {

	public static StatisticsFocusRouteController Current;

	private List<ActivityFocusRoute> trainResults;

	private StatisticsFocusRoute statistics;

	public StatisticsFocusRouteController()
	{
		Current = this;
	}

	public void addNewResult(ActivityFocusRoute result)
	{
		this.trainResults.Add(result);
	}

	public void calculateResults()
	{
		foreach(ActivityFocusRoute afr in this.trainResults)
		{
			this.statistics.SetDataPerTrain(afr);
		}
	}

	public void SetResults(int success, int total)
	{
		Debug.Log ("Tiempo promedio de reaccion: " + this.statistics.GetAverageReactionTime().ToString("F2") + " segundos");
		Debug.Log ("Atencion focalizada: " + this.statistics.GetFocusedAttention().ToString("F2") + "%");
		Debug.Log ("Atencion sostenida: " + this.statistics.GetSustainedAttention(success, total).ToString("F2") + "%");
	}

	#region Script
	void Start () 
	{
		this.trainResults = new List<ActivityFocusRoute>();
		this.statistics = new StatisticsFocusRoute();
	}	
	#endregion
}
