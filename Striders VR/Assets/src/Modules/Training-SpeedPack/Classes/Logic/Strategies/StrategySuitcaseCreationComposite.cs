using UnityEngine;
using System;
using System.Collections.Generic;
using StridersVR.Domain.SpeedPack;
using StridersVR.Modules.SpeedPack.Logic.StrategyInterfaces;

namespace StridersVR.Modules.SpeedPack.Logic.Strategies
{
	public class StrategySuitcaseCreationComposite : IStrategySuitcaseCreation
	{
		private int strategyIndex = 0;

		private List<IStrategySuitcaseCreation> childStrategyCreationList;

		public StrategySuitcaseCreationComposite ()
		{
			this.childStrategyCreationList = new List<IStrategySuitcaseCreation> ();
		}


		#region IStrategySuitcaseCreation
		public Suitcase createSuitcase(ScriptableObject genericSuitcasePartData)
		{
			return this.childStrategyCreationList [this.strategyIndex].createSuitcase (genericSuitcasePartData);
		}
		
		public void assignItemsMain(ScriptableObject genericItemsData, Suitcase currentSuitcase)
		{
			this.childStrategyCreationList [this.strategyIndex].assignItemsMain (genericItemsData, currentSuitcase);
		}
		
		public void createItem (Suitcase currentSuitcase)
		{
			this.childStrategyCreationList [this.strategyIndex].createItem (currentSuitcase);
		}
		#endregion

		public void addStrategy(IStrategySuitcaseCreation strategy)
		{
			this.childStrategyCreationList.Add (strategy);
		}

		#region Properties
		public int StrategyIndex
		{
			get { return this.strategyIndex; }
			set { this.strategyIndex = value; }
		}

		public int StrategyCount
		{
			get { return this.childStrategyCreationList.Count; }
		}
		#endregion
	}
}

