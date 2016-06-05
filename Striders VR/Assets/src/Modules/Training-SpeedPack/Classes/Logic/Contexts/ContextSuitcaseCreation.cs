using UnityEngine;
using StridersVR.Domain.SpeedPack;
using StridersVR.Modules.SpeedPack.Logic.StrategyInterfaces;
using StridersVR.Modules.SpeedPack.Logic.Strategies;

namespace StridersVR.Modules.SpeedPack.Logic.Contexts
{
	public class ContextSuitcaseCreation
	{
		private IStrategySuitcaseCreation strategySuitcaseCreation;
		private StrategySuitcaseCreationComposite compositeStrategy;

		public ContextSuitcaseCreation ()
		{
		}


		public void assignStrategy(IStrategySuitcaseCreation iStrategy)
		{
			this.strategySuitcaseCreation = iStrategy;
			this.compositeStrategy = (StrategySuitcaseCreationComposite)iStrategy;
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

		public void strategyCompositeIndex(int index)
		{
			this.compositeStrategy.StrategyIndex = index;
		}

		public int strategyCompositeCount()
		{
			return this.compositeStrategy.StrategyCount;
		}
		#endregion
		
		#region Properties
		public IStrategySuitcaseCreation StrategyGenerateObjects
		{
			get { return this.strategySuitcaseCreation; }
		}
		#endregion
	}
}

