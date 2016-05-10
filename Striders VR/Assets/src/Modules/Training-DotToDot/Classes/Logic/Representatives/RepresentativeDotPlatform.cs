using System.Collections.Generic;
using UnityEngine;
using StridersVR.Domain.DotToDot;
using StridersVR.ScriptableObjects.DotToDot;
using StridersVR.Modules.DotToDot.Logic.Contexts;
using StridersVR.Modules.DotToDot.Logic.Strategies;

namespace StridersVR.Modules.DotToDot.Logic.Representatives
{
	public class RepresentativeDotPlatform
	{
		private GameObject dotContainer;
		private GameObject pointPrefab;

		private ContextPoints ContextPoints;
		
		public RepresentativeDotPlatform (GameObject dotContainer, GameObject pointPrefab)
		{
			this.dotContainer = dotContainer;
			this.pointPrefab = pointPrefab;
			this.ContextPoints = new ContextPoints ();

			this.ContextPoints.StrategyPoints = new StrategyPointsEasy (this.dotContainer, this.pointPrefab);
		}


//		public void drawDots(List<VertexPoint> vertexPointList)
//		{
//			GameObject _newDot, _dotCollider;
//			ScriptableObjectDot _dotData = (ScriptableObjectDot)this.genericDotData;
//
//			foreach (VertexPoint _vertexPoint in vertexPointList) 
//			{
//				_newDot = (GameObject)GameObject.Instantiate(_dotData.DotPrefab, Vector3.zero, Quaternion.Euler(Vector3.zero));
//				_newDot.transform.parent = this.dotContainer.transform;
//				_newDot.transform.localPosition = _vertexPoint.VertexPointPosition;
//
//				_dotCollider = _newDot.transform.FindChild("PointCollider").gameObject;
//				_dotCollider.GetComponent<PointController>().VertexPointLocal = _vertexPoint;
//			}
//		}

//		public void setDotData(ScriptableObject genericDotData)
//		{
//			this.genericDotData = genericDotData;
//		}

		public PointsContainer drawDots()
		{
			return this.ContextPoints.createPoints ();
		}


		public void removeCurrentFigureModel(GameObject endPointsContainer)
		{
			for (int i = 0; i < this.dotContainer.transform.childCount; i++) {
				Transform _child = this.dotContainer.transform.GetChild (i);
				GameObject.Destroy (_child.gameObject);
			}
			
			for (int i = 0; i < endPointsContainer.transform.childCount; i++) {
				Transform _child = endPointsContainer.transform.GetChild (i);
				GameObject.Destroy (_child.gameObject);
			}
		}
	}
}

