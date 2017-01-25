using UnityEngine;
using System;

namespace StridersVR.Buttons
{
	public class ButtonLastGames : StatisticsPanelButton
	{
		protected override void PanelAction ()
		{
			if(!this.isSelected)
			{
				MenuStatisticsController.Current.NewButtonActive(this);
				MenuStatisticsController.Current.GetLastPlays();
			}
		}

		#region Script
		void Start()
		{
			this.isSelected = true;
			this.SetValues();

			MenuStatisticsController.Current.SetDefaultButton(this);
		}
		#endregion
	}
}

