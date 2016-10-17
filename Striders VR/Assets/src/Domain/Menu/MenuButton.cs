using System;
using UnityEngine;

namespace StridersVR.Domain.Menu
{
	public abstract class MenuButton
	{
		protected string animName;
		protected string animVariable;
		protected GameObject menuContainer;


		public abstract void buttonAction();


	}
}

