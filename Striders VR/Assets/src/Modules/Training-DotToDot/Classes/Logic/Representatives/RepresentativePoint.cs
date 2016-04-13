using System.Collections.Generic;
using UnityEngine;
using StridersVR.Domain.DotToDot;

namespace StridersVR.Modules.DotToDot.Logic.Representatives
{
	public class RepresentativePoint
	{
		private int errorCount;

		private List<GameObject> childColliderList;

		private GameObject dotContainer;

		private VertexPoint vertexPointLocal;

		public RepresentativePoint (GameObject dotContainer)
		{
			this.errorCount = 0;
			this.dotContainer = dotContainer;
			this.childColliderList = new List<GameObject> ();
		}


		private GameObject getChildcollider(Vector3 childPosition)
		{
			GameObject childDotcollider = null;
			
			for (int i = 0; i < this.dotContainer.transform.childCount; i++) 
			{
				childDotcollider = this.dotContainer.transform.GetChild(i).FindChild("PointCollider").gameObject;
				if(childDotcollider.GetComponent<PointController>().VertexPointLocal.VertexPointPosition == childPosition)
					break;
			}
			
			return childDotcollider;
		}
		
		public bool setNeighbourDots(VertexPoint vertexPointLocal)
		{
			if (vertexPointLocal != null) 
			{
				this.vertexPointLocal = vertexPointLocal;
				foreach (Vector3 childPosition in this.vertexPointLocal.NeighbourVectorList) 
				{
					GameObject childCollider = this.getChildcollider(childPosition);
					if(childCollider != null)
					{
						this.childColliderList.Add(childCollider);
					}
				}
				return true;
			}

			return false;
		}

		public void validateNeighbour(Vector3 endPoint)
		{
			bool _found = false;

			foreach (GameObject _point in this.childColliderList) 
			{
				if(_point.GetComponent<PointController>().VertexPointLocal.VertexPointPosition == endPoint)
				{
					_found = true;
					break;
				}
			}

			if (!_found) 
			{
				this.errorCount++;
			}
		}

		#region Properties
		public int ErrorCount
		{
			get { return this.errorCount; }
		}
		#endregion
	}
}

