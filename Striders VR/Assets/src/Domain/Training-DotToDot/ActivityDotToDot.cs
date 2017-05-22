using System.Collections.Generic;
using UnityEngine;
using System;

namespace StridersVR.Domain.DotToDot
{
	public class ActivityDotToDot
	{
		private int revelations = 0;
		public int Revelations {
			get { return revelations; }
			set { revelations = value; }
		}

		private float timeComplete = 0;
		public float TimeComplete {
			get { return timeComplete; }
		}

		private List<float> timeRevelationList;
		public List<float> TimeRevelationList {
			get { return timeRevelationList; }
		}
		
		private bool isCorrect = false;
		public bool IsCorrect {
			get { return isCorrect; }
			set { isCorrect = value; }
		}

		public ActivityDotToDot ()
		{
			this.timeRevelationList = new List<float>();
		}

		public void addTimeRevelation(float _time)
		{
			if(_time > 0.1f)
			{
				_time = (float)Math.Round(_time, 2);
				this.timeRevelationList.Add(_time);
			}

		}

		public void setTimeComplete(float _time)
		{
			_time = (float)Math.Round(_time, 2);
			this.timeComplete = _time;
		}
		
	}
}

