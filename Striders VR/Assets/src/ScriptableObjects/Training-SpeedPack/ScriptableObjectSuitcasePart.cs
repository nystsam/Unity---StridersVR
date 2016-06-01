using UnityEngine;
using System.Collections.Generic;
using StridersVR.Domain.SpeedPack;

namespace StridersVR.ScriptableObjects.SpeedPack
{
	public class ScriptableObjectSuitcasePart : ScriptableObject
	{		
		[SerializeField]
		private SuitcasePart suitcasePart4x2;

		[SerializeField]
		private SuitcasePart suitcasePart3x3;

		[SerializeField]
		private SuitcasePart suitcasePart2x2;


		public SuitcasePart getSuitcasePart4x2()
		{
			SuitcasePart _newSuitcasePart;

			_newSuitcasePart = new SuitcasePart (this.suitcasePart4x2.SuitcasePartPrefab);
			_newSuitcasePart.setSpotsFromData (4, 2);

			return _newSuitcasePart;
		}

		public SuitcasePart getSuitcasePart3x3()
		{
			SuitcasePart _newSuitcasePart;
			
			_newSuitcasePart = new SuitcasePart (this.suitcasePart4x2.SuitcasePartPrefab);
			_newSuitcasePart.setSpotsFromData (3, 3);
			
			return _newSuitcasePart;
		}

		public SuitcasePart getSuitcasePart2x2()
		{
			SuitcasePart _newSuitcasePart;
			
			_newSuitcasePart = new SuitcasePart (this.suitcasePart4x2.SuitcasePartPrefab);
			_newSuitcasePart.setSpotsFromData (2, 2);
			
			return _newSuitcasePart;
		}
	}
}

