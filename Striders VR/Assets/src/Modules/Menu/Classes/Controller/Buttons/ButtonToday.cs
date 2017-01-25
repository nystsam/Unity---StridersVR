using UnityEngine;
using System;

namespace StridersVR.Buttons
{
	public class ButtonToday : StatisticsPanelButton
	{
		protected override void PanelAction ()
		{
			if(!this.isSelected)
			{
				MenuStatisticsController.Current.NewButtonActive(this);
			}
		}

		#region Script
		void Start()
		{
			this.SetValues();
		}
		#endregion
	}
}

