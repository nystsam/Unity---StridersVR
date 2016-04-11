using System.Collections.Generic;
using UnityEngine;
using StridersVR.Domain.DotToDot;
using StridersVR.ScriptableObjects.DotToDot;

namespace StridersVR.Modules.DotToDot.Logic.Representatives
{
	public class RepresentativeDotPlatform
	{
		private GameObject dotContainer;
		private ScriptableObject genericDotData;

		public RepresentativeDotPlatform (GameObject dotContainer)
		{
			this.dotContainer = dotContainer;
		}


		public void drawDots(List<VertexPoint> vertexPointList)
		{
			GameObject _newDot, _dotCollider;
			ScriptableObjectDot _dotData = (ScriptableObjectDot)this.genericDotData;

			foreach (VertexPoint _vertexPoint in vertexPointList) 
			{
				_newDot = (GameObject)GameObject.Instantiate(_dotData.DotPrefab, Vector3.zero, Quaternion.Euler(Vector3.zero));
				_newDot.transform.parent = this.dotContainer.transform;
				_newDot.transform.localPosition = _vertexPoint.VertexPointPosition;

				_dotCollider = _newDot.transform.FindChild("PointCollider").gameObject;
				_dotCollider.GetComponent<PointController>().VertexPointLocal = _vertexPoint;
			}
		}

		public void setDotData(ScriptableObject genericDotData)
		{
			this.genericDotData = genericDotData;
		}
	}
}

