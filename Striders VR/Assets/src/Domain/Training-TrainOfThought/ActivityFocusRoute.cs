using System.Collections.Generic;
using UnityEngine;

namespace StridersVR.Domain.TrainOfThought
{
	public class ActivityFocusRoute
	{
		private List<float> timeList;
		public List<float> TimeList 
		{
			get { return timeList; }
		}
		
		private List<int> buttonCountList;
		public List<int> ButtonCountList 
		{
			get { return buttonCountList; }
		}
		
		private List<int> trainCountList;
		public List<int> TrainCountList 
		{
			get { return trainCountList; }
		}
		
		private bool isTrainSucceeded;
		public bool IsTrainSucceeded 
		{
			get { return isTrainSucceeded; }
			set { isTrainSucceeded = value; }
		}
		
		private int totalActivity = 0;
		public int TotalActivity
		{
			get { return totalActivity; }
		}
		
		public ActivityFocusRoute ()
		{
			this.timeList = new List<float>();
			this.buttonCountList = new List<int>();
			this.trainCountList = new List<int>();
		}
		
		
		public void AddResult(float sec, int num, int count)
		{
			this.timeList.Add(sec);
			this.trainCountList.Add(num);
			this.buttonCountList.Add(count);
			
			this.totalActivity = this.timeList.Count;
		}
	}
}

