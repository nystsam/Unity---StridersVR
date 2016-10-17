using System;
using UnityEngine;

namespace StridersVR.Domain.Menu
{
	public class MenuButtonStatistic : MenuButton
	{
		public MenuButtonStatistic ()
		{
			this.menuContainer = GameObject.FindGameObjectWithTag("GameController");

			this.animName = "AnimStatisticsMenu";
			this.animVariable = "IsStatistics";
		}
		
		public override void buttonAction()
		{
			this.menuContainer.GetComponent<MenuContainerController> ().changeMenu (this.animName, this.animVariable, true);
		}
	}
}

