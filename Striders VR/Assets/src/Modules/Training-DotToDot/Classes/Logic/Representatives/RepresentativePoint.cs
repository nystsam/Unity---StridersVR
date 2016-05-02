using System.Collections.Generic;
using UnityEngine;
using StridersVR.Domain.DotToDot;

namespace StridersVR.Modules.DotToDot.Logic.Representatives
{
	public class RepresentativePoint
	{
		private int errorCount = 0;

		private PointDot localPointDot;

		public RepresentativePoint ()
		{
		}

		public void validateNeighbour(PointDot possibleNeighbourPoint)
		{	
			if (!possibleNeighbourPoint.validateNeighbour (localPointDot.PointPosition)) 
			{
				this.errorCount++;
			}
		}

		#region Properties
		public int ErrorCount
		{
			get { return this.errorCount; }
		}

		public PointDot LocalPointDot
		{
			set { this.localPointDot = value; }
		}
		#endregion
	}
}

