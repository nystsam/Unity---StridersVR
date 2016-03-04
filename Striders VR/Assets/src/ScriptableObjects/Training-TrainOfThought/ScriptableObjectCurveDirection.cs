using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StridersVR.Domain.TrainOfThought;

namespace StridersVR.ScriptableObjects.TrainOfThought
{
	public class ScriptableObjectCurveDirection : ScriptableObject
	{
		[SerializeField]
		private List<CurveDirection> curveDirectionListEasyMode;


		#region Properties
		public List<CurveDirection> CurveDirectionListEasyMode
		{
			get { return this.curveDirectionListEasyMode; }
		}
		#endregion

	}
}

