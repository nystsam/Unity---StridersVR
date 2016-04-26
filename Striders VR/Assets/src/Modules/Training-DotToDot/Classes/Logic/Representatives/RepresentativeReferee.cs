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

		public bool pointPlaced(GameObject dotContainer)
		{
			bool _foundError = false;

			this.pointsComplete++;
			if (this.pointsComplete == this.numberOfPoints) 
			{
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
					return true;
				}
				else
				{
					Debug.Log ("MAL!");
				}
			}
			return false;
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

