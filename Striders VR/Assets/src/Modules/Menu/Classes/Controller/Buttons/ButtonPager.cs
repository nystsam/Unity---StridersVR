using UnityEngine;
using System.Collections.Generic;

namespace StridersVR.Buttons
{
	public class ButtonPager : VirtualButtonController
	{
		[SerializeField] private ButtonTrainingList list;
		[SerializeField] private bool isNext;

		protected override void ButtonAction ()
		{
			if(this.isNext)
				list.NextTraining();
			else
				list.PreviousTraining();
		}
	}
}

