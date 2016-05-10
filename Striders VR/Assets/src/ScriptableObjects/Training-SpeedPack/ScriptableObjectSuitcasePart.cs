using UnityEngine;
using System.Collections.Generic;
using StridersVR.Domain.SpeedPack;

namespace StridersVR.ScriptableObjects.SpeedPack
{
	public class ScriptableObjectSuitcasePart : ScriptableObject
	{		
		[SerializeField]
		private SuitcasePart suitcasePart4x2;


		public SuitcasePart getSuitcasePart4x2()
		{
			SuitcasePart _newSuitcasePart;

			_newSuitcasePart = new SuitcasePart (this.suitcasePart4x2.SuitcasePartPrefab);
			_newSuitcasePart.setSpotsFromData (4, 2);

			return _newSuitcasePart;
		}
	}
}

