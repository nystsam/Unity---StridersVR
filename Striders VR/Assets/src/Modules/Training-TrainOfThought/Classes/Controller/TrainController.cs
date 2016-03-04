using UnityEngine;
using System.Collections;
using StridersVR.Domain.TrainOfThought;
using StridersVR.Modules.TrainOfThought.Logic.Representatives;

public class TrainController : MonoBehaviour {

	public float speed = 2f;
	public float rotationSpeed = 9;
	public float railroadSwitchInView;

	private Vector3 direction;
	private RepresentativeTrainActions trainActions;
	private RepresentativeTrainScore trainScore;
	private ColorTrain colorTrain;


	#region Script
	void Awake () {
		this.direction = new Vector3 (1, 0, 0);
		this.trainActions = new RepresentativeTrainActions (this.direction);
		this.trainActions.CurrentYAngle = transform.rotation.eulerAngles.y;
		this.trainActions.RotationSpeed = this.rotationSpeed;
		this.trainScore = GameObject.FindGameObjectWithTag ("TrainScore").GetComponent<ScoresController> ().TrainScore;
	}

	void Update () {
		/*
		RaycastHit hit;
		Ray detectionRay = new Ray (transform.position + transform.right, transform.right);
		Debug.DrawRay (transform.position + transform.right, transform.right * this.curveDetectionDistance);

		if (Physics.Raycast (detectionRay, out hit, this.curveDetectionDistance)) 
		{
			if (hit.collider.tag.Equals ("Curve")) 
			{
				this.changeDirection (hit.collider);
			}
		}
		*/
		if (this.trainActions.AllowToRotate) 
		{
			transform.RotateAround (transform.position, Vector3.up, 9 * this.trainActions.NewRotation);
			this.trainActions.rotationValues (transform.rotation.eulerAngles.y);
		}
			
		transform.Translate(speed * direction.x * Time.deltaTime, speed * direction.y * Time.deltaTime , speed * direction.z * Time.deltaTime, Space.World);

	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.tag.Equals ("Curve") || other.tag.Equals ("RailroadSwitch")) 
		{
			if (!this.trainActions.ColliderEnter) 
			{
				this.trainActions.ColliderEnter = true;
				this.trainActions.colliderDetector (other);
				this.direction = this.trainActions.changeDirection ();
			}
		} 
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag.Equals ("Curve") || other.tag.Equals ("RailroadSwitch")) 
		{
			this.trainActions.ColliderEnter = false;
		}	
	}

	void OnTriggerStay(Collider other)
	{
		if (other.tag.Equals ("ColorStation")) 
		{
			this.trainScore.trainArrival (other, this.transform.gameObject, this.colorTrain);
		}
	}
	#endregion

	#region Properties
	public ColorTrain ColorTrain
	{
		set { this.colorTrain = value; }
	}
	#endregion

}
