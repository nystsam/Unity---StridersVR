using System;
using UnityEngine;

namespace StridersVR.Domain
{
	/// <summary>
	/// Class that control the UX/UI button for virtual reality
	/// </summary>
	public class VirtualButton
	{
		/// <summary>
		/// The resting position in local space of the current button.
		/// </summary>
		private Vector3 restingPosition;
		/// <summary>
		/// The spring force.
		/// </summary>
		private float spring;
		/// <summary>
		/// The direction where the vector spring.
		/// </summary>
		private Vector3 direction;

		/// <summary>
		/// Initializes a new instance of the <see cref="StridersVR.Domain.VirtualButton"/> class that control the UX/UI 
		/// button for virtual reality.
		/// </summary>
		/// <param name="restingPosition">The resting position in local space of the current button.</param>
		/// <param name="spring">Spring force.</param>
		/// <param name="direction">The direction where the vector spring: (1,0,0), (0,1,0), (0,0,1).</para>
		public VirtualButton (Vector3 restingPosition, float spring, Vector3 direction)
		{
			this.restingPosition = restingPosition;
			this.spring = spring;
			this.direction = direction;
		}


		/// <summary>
		/// Applies the relative spring force to the button.
		/// </summary>
		/// <returns>The relative force apply to the local position.</returns>
		/// <param name="rigidbody">Rigidbody of the current button.</param>
		/// <param name="currentPosition">Current position in local space.</param>
		public Vector3 ApplyRelativeSpring(Vector3 currentPosition)
		{
			return (-this.spring * (currentPosition - this.restingPosition));
		}

		/// <summary>
		/// Constraints the movement (bouncing or spring) of the current button.
		/// </summary>
		/// <returns>The new position of the button.</returns>
		/// <param name="currentPosition">Current position in local space.</param>
		/// <param name="minDistance">Minimum distance to constraint.</param>
		/// <param name="maxDistance">Max distance to constraint.</param>
		public Vector3 ConstraintMovement(Vector3 currentPosition, float minDistance, float maxDistance)
		{
			Vector3 _localPosition = currentPosition;

			if (this.direction == Vector3.right)
				_localPosition.x = Mathf.Clamp (_localPosition.x, minDistance, maxDistance);
			else if (this.direction == Vector3.up)
				_localPosition.y = Mathf.Clamp (_localPosition.y, minDistance, maxDistance);
			else if (this.direction == Vector3.forward)
				_localPosition.z = Mathf.Clamp (_localPosition.z, minDistance, maxDistance);

			return _localPosition;
		}

		/// <summary>
		/// Is the button the pressed?.
		/// </summary>
		/// <returns><c>true</c>, if button was pressed, <c>false</c> otherwise.</returns>
		/// <param name="currentPosition">Current position in local space.</param>
		/// <param name="triggerDistance">Trigger distance to press.</param>
		public bool IsButtonPressed(Vector3 currentPosition, float triggerDistance)
		{
			bool _isPressed = false;

			if (this.direction == Vector3.right) 
			{
				if(currentPosition.x > triggerDistance)
					_isPressed = true;
			} 
			else if (this.direction == Vector3.up) 
			{
				if(currentPosition.y > triggerDistance)
					_isPressed = true;
			} 
			else if (this.direction == Vector3.forward) 
			{
				if(currentPosition.z > triggerDistance)
					_isPressed = true;
			}

			return _isPressed;
		}

		/// <summary>
		/// Is the button the released?.
		/// </summary>
		/// <returns><c>true</c>, if button was released, <c>false</c> otherwise.</returns>
		/// <param name="currentPosition">Current position.</param>
		/// <param name="triggerDistance">Trigger distance.</param>
		public bool IsButtonReleased(Vector3 currentPosition, float triggerDistance)
		{
			bool _isPressed = false;
			
			if (this.direction == Vector3.right) 
			{
				if(currentPosition.x < triggerDistance)
					_isPressed = true;
			} 
			else if (this.direction == Vector3.up) 
			{
				if(currentPosition.y < triggerDistance)
					_isPressed = true;
			} 
			else if (this.direction == Vector3.forward) 
			{
				if(currentPosition.z < triggerDistance)
					_isPressed = true;
			}
			
			return _isPressed;
		}
	}
}

