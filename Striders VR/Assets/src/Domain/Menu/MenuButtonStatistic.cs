using System;
using UnityEngine;

namespace StridersVR.Domain.Menu
{
	public class MenuButtonStatistic : MenuButton
	{
		private string animName;
		private string animVariable;
		
		public MenuButtonStatistic ()
		{
			this.menuContainer = GameObject.FindGameObjectWithTag("GameController");

			this.animName = "AnimStatisticsMenu";
			this.animVariable = "IsStatistics";
		}
		
		public override void buttonAction()
		{
			this.menuContainer.GetComponent<MenuContainerController> ().menuMain.SetActive(false);
			this.menuContainer.GetComponent<MenuContainerController> ().changeMenu (this.animName, this.animVariable);
			this.menuContainer.GetComponent<MenuContainerController> ().menuTraining.SetActive(true);
		}
	}
}

