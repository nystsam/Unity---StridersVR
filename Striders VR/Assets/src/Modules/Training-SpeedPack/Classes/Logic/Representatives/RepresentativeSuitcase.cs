using UnityEngine;
using System.Collections.Generic;
using StridersVR.Domain.SpeedPack;
using StridersVR.Modules.SpeedPack.Logic.Contexts;
using StridersVR.Modules.SpeedPack.Logic.Strategies;

namespace StridersVR.Modules.SpeedPack.Logic.Representatives
{
	public class RepresentativeSuitcase 
	{
		private GameObject suitcaseContainer;

		private ScriptableObject suitcasePartData;

		private ContextSuitcaseCreation ContextSuitcaseCreation;

		public RepresentativeSuitcase(GameObject suitcaseContainer)
		{
			this.suitcaseContainer = suitcaseContainer;
			this.ContextSuitcaseCreation = new ContextSuitcaseCreation ();

			this.ContextSuitcaseCreation.StrategyGenerateObjects = new StrategySuitcaseCreation4x2 (this.suitcaseContainer);
		}


		public Suitcase getSuitcase()
		{
			return this.ContextSuitcaseCreation.createSuitcase (suitcasePartData);
		}

		public void spawnItems()
		{
			
		}

		#region Properties
		public ScriptableObject SetData
		{
			set { this.suitcasePartData = value; }
		}
		#endregion
	}
}
