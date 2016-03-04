using System;
using UnityEngine;
using StridersVR.Modules.TrainOfThought.Logic.Contexts;
using StridersVR.Modules.TrainOfThought.Logic.StrategyInterfaces;
using StridersVR.Modules.TrainOfThought.Logic.Strategies;

namespace StridersVR.Modules.TrainOfThought.Logic.Representatives
{
	/// <summary>
	/// Class for all functions that allow all trains rotate and change direction.
	/// </summary>
	public class RepresentativeTrainActions
	{
		private ContextTrainActions contexTrainActions;
		private Vector3 currentDirection;
		private Vector3 newTrainDirection;
		private float currentYAngle;
		private float newRotation;
		private float rotationSpeed;
		private float anglesRotated;
		private bool allowToRotate;
		private bool colliderEnter = false;

		/// <summary>
		/// Initializes the class with the current direction of the train.
		/// </summary>
		/// <param name="currentDirection">Current direction.</param>
		public RepresentativeTrainActions (Vector3 currentDirection)
		{
			this.contexTrainActions = new ContextTrainActions ();
			this.currentDirection = currentDirection;
			this.newTrainDirection = Vector3.zero;
			this.newRotation = 0f;
			this.anglesRotated = 0f;
			this.allowToRotate = false;
		}


		/// <summary>
		/// Seek the tag of the current collider to get the new direction for the train.
		/// </summary>
		/// <param name="collider">Collider.</param>
		public void colliderDetector(Collider collider)
		{
			IStrategyTrainActions<Collider,Vector3> _changeDirection = new StrategyTrainActionsChangeDirection ();
			this.newTrainDirection = this.contexTrainActions.changeDirection (_changeDirection, collider);
		}

		/// <summary>
		/// Attempt to change direction when dot product of the train's current direction and the train's new direction is equals zero.
		/// </summary>
		public Vector3 changeDirection()
		{
			if (Vector3.Dot (this.currentDirection, this.newTrainDirection) == 0) 
			{
				IStrategyTrainActions<float, float> _changeRotation = new StrategyTrainActionsChangeRotation (newTrainDirection, this.currentYAngle);
				this.newRotation = this.contexTrainActions.changeRotation (_changeRotation, this.newRotation);
				this.allowToRotate = true;
			}
			this.currentDirection = this.newTrainDirection;
			return this.currentDirection;
		}

		/// <summary>
		/// Set the values conditions for the train to finish the rotation.
		/// </summary>
		/// <param name="currentYAngle">Current Y angle.</param>
		public void rotationValues(float currentYAngle)
		{
			this.currentYAngle = currentYAngle;
			this.anglesRotated += this.rotationSpeed;
			if (this.anglesRotated >= 90f) 
			{
				this.anglesRotated = 0f;
				this.allowToRotate = false;
			}
		}

		#region Properties
		/// <summary>
		/// Gets or sets the current Y angle of the train.
		/// </summary>
		/// <value>The current Y angle.</value>
		public float CurrentYAngle
		{
			get { return this.currentYAngle; }
			set { this.currentYAngle = value; }
		}

		/// <summary>
		/// Gets the new rotation, values can be 1 or -1.
		/// </summary>
		/// <value>The new rotation.</value>
		public float NewRotation
		{
			get { return this.newRotation; }
		}

		/// <summary>
		/// Gets or sets the rotation speed from the controller.
		/// </summary>
		/// <value>The rotation speed.</value>
		public float RotationSpeed
		{
			get { return this.rotationSpeed; }
			set { this.rotationSpeed = value; }
		}

		/// <summary>
		/// Gets a value indicating whether the color train allow to rotate.
		/// </summary>
		/// <value><c>true</c> if allow to rotate; otherwise, <c>false</c>.</value>
		public bool AllowToRotate
		{
			get { return this.allowToRotate; }
		}

		/// <summary>
		/// Gets or sets a value indicating whether a collider with the specific tag entered. The role is like a semaphore.
		/// </summary>
		/// <value><c>true</c> if collider enter; otherwise, <c>false</c>.</value>
		public bool ColliderEnter
		{
			get { return this.colliderEnter; }
			set { this.colliderEnter = value; }
		}
		#endregion
	}
}

