using System;
using UnityEngine;

namespace StridersVR.Domain.DotToDot
{
	public class Point
	{
		private Vector3 position;

		private GameObject pointLight;
		private GameObject pointAura;

		private bool isSelectedPoint;

		public Point (Vector3 position)
		{
			this.position = position;
		}

		public void setPointGraphics(GameObject pointLight, GameObject pointAura)
		{
			this.pointLight = pointLight;
			this.pointAura = pointAura;
			this.isSelectedPoint = false;
		}

		public void turnOn()
		{
			this.pointLight.SetActive (false);
			this.pointAura.GetComponent<ParticleSystem> ().Play ();
		}

		public void turnOff()
		{
			this.pointLight.SetActive (true);
			this.pointAura.GetComponent<ParticleSystem> ().Stop ();
		}

		#region Properties
		public Vector3 Position
		{
			get { return this.position; }
		}

		public bool IsSelectedPoint
		{
			get { return this.isSelectedPoint; }
			set { this.isSelectedPoint = value; }
		}
		#endregion
	}
}

