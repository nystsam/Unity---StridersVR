using System.Collections.Generic;
using UnityEngine;
using StridersVR.Domain.DotToDot;

namespace StridersVR.Modules.DotToDot.Logic.Representatives
{
	public class RepresentativeReferee
	{
		private bool changeFigureModel;
		private int numberOfPoints;
		private int pointsComplete;

		public RepresentativeReferee ()
		{
			this.changeFigureModel = false;
			this.numberOfPoints = 0;
			this.pointsComplete = 0;
		}


		public void setNumberOfPoints(int number)
		{
			this.numberOfPoints = number;
		}

		public bool pointPlaced()
		{
			this.pointsComplete++;

			if (this.pointsComplete == this.numberOfPoints) 
			{
				return true;

			}
			return false;
		}

		public void validateErrors(GameObject dotContainer)
		{
			bool _foundError = false;

			Debug.Log ("Completo todos los puntos, se procede a verificar validez...");
			for(int i = 0; i < dotContainer.transform.childCount; i++)
			{
				Transform _child = dotContainer.transform.GetChild(i);

				if(_child.GetComponentInChildren<PointController>().numberOfErrors() > 0)
				{
					_foundError = true;
					break;
				}
			}

			if(!_foundError)
			{
				Debug.Log ("No consiguio errores");
			}
			else
			{
				Debug.Log ("MAL!");
			}
		}


		#region Properties
		public bool ChangeFigureModel
		{
			get { return this.changeFigureModel; }
			set { this.changeFigureModel = value; }
		}
		#endregion
	
	}
}

