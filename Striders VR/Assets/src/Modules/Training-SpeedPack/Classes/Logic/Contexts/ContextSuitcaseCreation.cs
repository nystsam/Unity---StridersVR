using UnityEngine;
using StridersVR.Domain.SpeedPack;
using StridersVR.Modules.SpeedPack.Logic.StrategyInterfaces;

namespace StridersVR.Modules.SpeedPack.Logic.Contexts
{
	public class ContextSuitcaseCreation
	{
		private IStrategySuitcaseCreation strategySuitcaseCreation;

		public ContextSuitcaseCreation ()
		{
		}

	
		#region Service methods
		public Suitcase createSuitcase(ScriptableObject genericSuitcasePartData)
		{
			return this.strategySuitcaseCreation.createSuitcase(genericSuitcasePartData);
		}
		#endregion
		
		#region Properties
		public IStrategySuitcaseCreation StrategyGenerateObjects
		{
			get { return this.strategySuitcaseCreation; }
			set { this.strategySuitcaseCreation = value; }
		}
		#endregion
	}
}

