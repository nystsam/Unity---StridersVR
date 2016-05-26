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

		public int getAnimHash()
		{
			int _hash = 0;

			if(this.attachedPosition == new Vector3(1,0,0))
				_hash = Animator.StringToHash("RotateLeft");
			else if(this.attachedPosition == new Vector3(-1,0,0))
				_hash = Animator.StringToHash("RotateRight");
			else if(this.attachedPosition == new Vector3(0,-0.5f,0))
				_hash = Animator.StringToHash("RotateBottom");
			else if(this.attachedPosition == new Vector3(0,0.5f,0))
				_hash = Animator.StringToHash("RotateTop");

			return _hash;
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

