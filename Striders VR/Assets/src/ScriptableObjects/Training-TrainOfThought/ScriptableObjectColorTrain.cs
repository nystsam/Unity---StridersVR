using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StridersVR.Domain.TrainOfThought;

namespace StridersVR.ScriptableObjects.TrainOfThought
{
	public class ScriptableObjectColorTrain : ScriptableObject
	{
		[SerializeField] private Vector3 startPoint;
		[SerializeField] private List<ColorTrain> colorTrainsList;


		#region Properties
		public Vector3 StartPoint
		{
			get { return this.startPoint; }
		}

		public List<ColorTrain> TrainsList
		{
			get { return this.colorTrainsList; }
		}
		#endregion
	}
}

