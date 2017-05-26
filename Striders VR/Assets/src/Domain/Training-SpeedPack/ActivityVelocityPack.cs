using System.Collections;
using UnityEngine;
using System;

namespace StridersVR.Domain.SpeedPack
{
	public class ActivityVelocityPack
	{
		private float timeComplete = 0;
		public float TimeComplete {
			get { return timeComplete; }
		}

		private bool isCorrect = false;
		public bool IsCorrect {
			get { return isCorrect; }
			set { isCorrect = value; }
		}

		private int score = 0;
		public int Score {
			get { return score; }
			set { score = value; }
		}

		public ActivityVelocityPack ()
		{
		}

		public void setTimeComplete(float _time)
		{
			_time = (float)Math.Round(_time, 2);
			this.timeComplete = _time;
		}
	}
}

