using UnityEngine;
using System.Collections;

public class RotationController : MonoBehaviour {

	public GameObject dotsContainer;
	public GameObject endPointsContainer;
	public int direction;
	public int speed;

	private bool isRotating;

	private void rotatePlatform()
	{
		if (this.isRotating) 
		{
			Vector3 _rotation = this.dotsContainer.transform.localRotation.eulerAngles;

			_rotation.y +=  this.speed * this.direction * Time.deltaTime;
			this.dotsContainer.transform.localRotation = Quaternion.Euler(_rotation);
			this.endPointsContainer.transform.localRotation = Quaternion.Euler(_rotation);
		}
	}


	#region Script
	void Start () 
	{
		this.isRotating = false;
	}

	void Update()
	{
		this.rotatePlatform ();
	}

	void OnTriggerEnter()
	{
		this.isRotating = true;
	}

	void OnTriggerExit()
	{
		this.isRotating = false;
	}
	#endregion
}
