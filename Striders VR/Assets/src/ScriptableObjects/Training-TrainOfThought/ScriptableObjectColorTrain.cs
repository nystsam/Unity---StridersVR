using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StridersVR.Domain.TrainOfThought;

namespace StridersVR.ScriptableObjects.TrainOfThought
{
	public class ScriptableObjectColorTrain : ScriptableObject
	{
		[SerializeField] private List<ColorTrain> colorTrainsList;


		#region Properties
		public List<ColorTrain> TrainsList
		{
			get { return this.colorTrainsList; }
		}
		#endregion
	}
}

