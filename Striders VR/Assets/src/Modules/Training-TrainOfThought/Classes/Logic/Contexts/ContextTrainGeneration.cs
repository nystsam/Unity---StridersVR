using UnityEngine;
using System.Collections.Generic;
using StridersVR.ScriptableObjects.TrainOfThought;
using StridersVR.Modules.TrainOfThought.Logic.StrategyInterfaces;
using StridersVR.Domain.TrainOfThought;

namespace StridersVR.Modules.TrainOfThought.Logic.Contexts
{
	public class ContextTrainGeneration
	{
		private IStrategyTrainGeneration strategyTrainGeneration;

		public ContextTrainGeneration ()
		{
		}


		#region Service methods
		public float selectTrain(ScriptableObject gameColorTrainsData)
		{
			return this.strategyTrainGeneration.selectTrain(gameColorTrainsData);
		}

		public bool instantiateTrain()
		{
			return this.strategyTrainGeneration.instantiateTrain ();
		}
		#endregion

		#region Properties
		public IStrategyTrainGeneration StrategyTrainGeneration
		{
			get { return this.strategyTrainGeneration; }
			set { this.strategyTrainGeneration = value; }
		}
		#endregion
	}
}

