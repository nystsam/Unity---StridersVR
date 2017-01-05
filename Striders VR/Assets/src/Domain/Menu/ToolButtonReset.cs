using UnityEngine;
using StridersVR.Domain;

namespace StridersVR.Domain.Menu
{
	public class ToolButtonReset : ToolButton
	{
		public ToolButtonReset ()
		{
		}

		public override void toolAction()
		{
			Training _currentTraining;

			_currentTraining = GameObject.FindGameObjectWithTag("StaticUser").GetComponent<StaticUserController>().Training;
			Application.LoadLevel (_currentTraining.Name);	
		}
	}
}

