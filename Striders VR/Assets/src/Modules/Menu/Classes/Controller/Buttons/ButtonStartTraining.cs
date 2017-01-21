using UnityEngine;
using System.Collections;

namespace StridersVR.Buttons
{
	public class ButtonStartTraining : VirtualButtonController
	{
		private GameObject user;

		protected override void ButtonAction ()
		{
			if(this.user.GetComponent<StaticUserController>().isValidGame())
			{
				this.user.GetComponent<StaticUserController>().gameSelected();
				Application.LoadLevel (this.user.GetComponent<StaticUserController>().Training.Name);
			}

		}

		#region Script
		void Start()
		{
			this.user = GameObject.FindGameObjectWithTag("StaticUser");
		}
		#endregion
	}
}

