using System.Collections.Generic;
using UnityEngine;

namespace StridersVR.Domain.DotToDot
{
	public class Point
	{
		private int pointId;

		private Dictionary<int, Point> neighbors;

		private Vector3 position;

		private bool isSelectedPoint;

		private GameObject pointLight = null;
		private GameObject pointAura = null;
		private GameObject pointPrefab;		

		public Point (int id)
		{
			this.pointId = id;
			this.neighbors = new Dictionary<int, Point> ();
			this.pointPrefab = Resources.Load ("Prefabs/Training-DotToDot/Point", typeof(GameObject)) as GameObject;
		}

		public void addNeighbor(Point neighbor)
		{
			this.neighbors.Add (neighbor.pointId, neighbor);
		}

		public bool isAlreadyNeighbor(Point currentPoint)
		{
			if(this.neighbors.ContainsKey(currentPoint.pointId))
			{
				return true;
			}

			return false;
		}

		public void setPosition(Vector3 position)
		{
			this.position = position;
		}

		public void setGameplayValues(GameObject pointLight, GameObject pointAura)
		{
			this.pointLight = pointLight;
			this.pointAura = pointAura;
			this.isSelectedPoint = false;
		}

		public void turnOn()
		{
			if(this.pointLight != null && this.pointAura != null)
			{
				this.pointLight.SetActive (false);
				this.pointAura.GetComponent<ParticleSystem> ().Play ();
			}
		}

		public void turnOff()
		{
			if(this.pointLight != null && this.pointAura != null)
			{
				this.pointLight.SetActive (true);
				this.pointAura.GetComponent<ParticleSystem> ().Stop ();
			}
		}

		#region Properties
		public int PointId
		{
			get { return this.pointId; }
		}

		public Vector3 Position
		{
			get { return this.position; }
		}

		public bool IsSelectedPoint
		{
			get { return this.isSelectedPoint; }
			set { this.isSelectedPoint = value; }
		}

		public GameObject PointPrefab
		{
			get { return this.pointPrefab; }
		}
		#endregion
	}
}

