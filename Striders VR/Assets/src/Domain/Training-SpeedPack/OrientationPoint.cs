using UnityEngine;

namespace StridersVR.Domain.SpeedPack
{
	public class OrientationPoint
	{
		private Vector3 attachedPosition;
		private Vector3 attachedRotation;

		private bool isActive;

		public OrientationPoint (Vector3 position, Vector3 rotation)
		{
			this.attachedPosition = position;
			this.attachedRotation = rotation;

			this.isActive = false;
		}


		public void activePoint()
		{
			this.isActive = true;
		}

		#region Properties
		public Vector3 AttachedRotation 
		{
			get { return attachedRotation; }
			set { attachedRotation = value; }
		}
		
		public Vector3 AttachedPosition 
		{
			get { return attachedPosition; }
			set { attachedPosition = value; }
		}

		public bool IsActive
		{
			get { return this.isActive; }
		}
		#endregion
	}
}

