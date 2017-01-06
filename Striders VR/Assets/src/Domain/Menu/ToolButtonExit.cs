using UnityEngine;
using StridersVR.Domain;

namespace StridersVR.Domain.Menu
{
	public class ToolButtonExit : ToolButton
	{
		public ToolButtonExit ()
		{
		}
		
		public override void toolAction()
		{
			Application.LoadLevel ("Menu");	
		}

		public override string getActionName()
		{
			return "salir";
		}
	}
}

