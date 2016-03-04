using UnityEngine;
using System.Collections.Generic;
using StridersVR.Domain.TrainOfThought;
using StridersVR.ScriptableObjects.TrainOfThought;
using StridersVR.Modules.TrainOfThought.Logic.StrategyInterfaces;

namespace StridersVR.Modules.TrainOfThought.Logic.Strategies
{
	public class StrategyTrainGenerationComposite : IStrategyTrainGeneration
	{
		private int selectedStrategyIndex = 0;
		private List<IStrategyTrainGeneration> childStrategyTrainGenerationList;

		public StrategyTrainGenerationComposite ()
		{
			this.childStrategyTrainGenerationList = new List<IStrategyTrainGeneration> ();
		}
			

		#region IStrategyTrainGeneration
		public float selectTrain(ScriptableObject genericColorTrainData)
		{
			return this.childStrategyTrainGenerationList [this.selectedStrategyIndex].selectTrain (genericColorTrainData);
		}

		public bool instantiateTrain ()
		{
			return this.childStrategyTrainGenerationList [this.selectedStrategyIndex].instantiateTrain ();
		}
		#endregion

		public void addStrategy(IStrategyTrainGeneration strategy)
		{
			this.childStrategyTrainGenerationList.Add (strategy);
		}

		#region Properties
		public int SelectedStrategyIndex
		{
			get { return this.selectedStrategyIndex; }
			set { this.selectedStrategyIndex = value; }
		}

		public int StrategyTrainGenerationListCount
		{
			get { return this.childStrategyTrainGenerationList.Count; }
		}
		#endregion
	}
}

