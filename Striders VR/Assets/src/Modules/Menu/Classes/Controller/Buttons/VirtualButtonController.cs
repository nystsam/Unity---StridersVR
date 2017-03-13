using UnityEngine;
using System.Collections;
using StridersVR.Domain;

namespace StridersVR.Buttons
{
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(SpringJoint))]
	public abstract class VirtualButtonController : MonoBehaviour {

		public Vector3 direction;

		public float triggerDistance;
		public float spring;
		public float min;
		public float max;

		private bool isPressed = false;

		private VirtualButton virtualButton;

		protected abstract void ButtonAction();

		private void buttonPressed ()
		{
			if (!this.isPressed && this.virtualButton.IsButtonPressed (this.transform.localPosition, this.triggerDistance)) 
			{
				this.isPressed = true;
				this.ButtonAction();
			} 
			else if (this.isPressed && this.virtualButton.IsButtonReleased (this.transform.localPosition, this.triggerDistance)) 
			{	
				this.isPressed = false;
			}
		}

		protected void ResetVirtualButton()
		{
			this.virtualButton = new VirtualButton (this.transform.localPosition, spring, direction);
		}

		protected void CancelForce()
		{
			this.transform.localPosition = this.virtualButton.RestingPosition;
			this.GetComponent<Rigidbody>().velocity = Vector3.zero;
			this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		}

		#region Script
		void Awake () 
		{
			this.virtualButton = new VirtualButton (this.transform.localPosition, spring, direction);
		}

		void Update () 
		{
			this.transform.localPosition = this.virtualButton.ConstraintMovement (this.transform.localPosition, min, max);
			this.GetComponent<Rigidbody> ().AddRelativeForce(this.virtualButton.ApplyRelativeSpring (this.transform.localPosition));
			
			this.buttonPressed ();
			this.GetComponent<SpringJoint>().connectedAnchor = this.transform.position;
		}

		void OnDisable()
		{
			this.transform.localPosition = this.virtualButton.RestingPosition;
			this.GetComponent<Rigidbody>().velocity = Vector3.zero;
			this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		}
		#endregion
	}
}


