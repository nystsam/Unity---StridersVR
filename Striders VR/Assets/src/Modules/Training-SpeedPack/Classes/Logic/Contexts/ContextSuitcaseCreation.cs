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

		public void assignItemsMain(ScriptableObject genericItemsData, Suitcase currentSuitcase)
		{
			this.strategySuitcaseCreation.assignItemsMain (genericItemsData, currentSuitcase);
		}

		public void createItem (Suitcase currentSuitcase)
		{
			this.strategySuitcaseCreation.createItem (currentSuitcase);
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

