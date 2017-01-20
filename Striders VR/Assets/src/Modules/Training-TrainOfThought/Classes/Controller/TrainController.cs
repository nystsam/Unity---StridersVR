using UnityEngine;
using System.Collections;
using StridersVR.Domain.TrainOfThought;
using StridersVR.Modules.TrainOfThought.Logic.Representatives;

public class TrainController : MonoBehaviour {

	public GameObject frontTrain;

	public float speed = 2f;
	public float rotationSpeed = 9;
	public float railroadSwitchInView;

	private Vector3 direction;
	private RepresentativeTrainActions trainActions;
	private RepresentativeTrainScore trainScore;
	private ColorTrain colorTrain;

	private ActivityFocusRoute currentActivity;

	private bool countBegin = false;
	private bool alloToDetect = true;

	private float hitRange = 4.6f;
	private float timing = 0;

	private int trainCount = 0;
	private int buttonCount = 0;

	private GameObject nearierSwitch;

	private void setRayCast()
	{
		RaycastHit hit;
		Vector3 myDirection = this.frontTrain.transform.right;
		Ray myRay = new Ray (this.frontTrain.transform.position, myDirection * this.hitRange);
		Debug.DrawRay (this.frontTrain.transform.position, myDirection * this.hitRange);
		if(!this.countBegin && this.nearierSwitch == null)
		{
			if (Physics.Raycast (myRay, out hit, this.hitRange) && hit.collider.tag.Equals ("RailroadSwitch")) 
			{
				this.nearierSwitch = hit.collider.gameObject;
				this.nearierSwitch.GetComponent<RailroadSwitchController>().newTrainApproaching();
				this.countBegin = true;			
			}
		}
	}

	private IEnumerator waitToRotate(Collider other)
	{
		yield return new WaitForSeconds(0.5f);
		this.direction = this.trainActions.changeDirection ();
		if(this.nearierSwitch != null)
		{
			this.trainCount = PlatformController.Current.TrainsInPlatform();
			this.buttonCount = this.nearierSwitch.GetComponent<RailroadSwitchController>().GetDirectionChangedCount();
			this.countBegin = false;
			this.nearierSwitch = null;

			this.currentActivity.AddResult(this.timing, this.trainCount, this.buttonCount);
			this.timing = 0;
			this.trainCount = 0;
			this.buttonCount = 0;
		}

		yield return new WaitForSeconds(0.4f);
		this.alloToDetect = true;
	}

	#region Script
	void Awake () 
	{
		this.direction = new Vector3 (1, 0, 0);
		this.trainActions = new RepresentativeTrainActions (this.direction);
		this.trainActions.CurrentYAngle = transform.rotation.eulerAngles.y;
		this.trainActions.RotationSpeed = this.rotationSpeed;
		this.trainScore = GameObject.FindGameObjectWithTag ("TrainScore").GetComponent<ScoresController> ().TrainScore;
		this.currentActivity = new ActivityFocusRoute();
	}

	void Update () 
	{
		this.setRayCast();

		if(this.countBegin)
		{
			this.timing += Time.deltaTime;
		}

		if(this.countBegin && this.nearierSwitch != null)
		{
			if(this.nearierSwitch.GetComponent<RailroadSwitchController>().FirstChanged())
			{
				this.countBegin = false;
			}
		}

		if (this.trainActions.AllowToRotate) 
		{
			transform.RotateAround (transform.position, Vector3.up, 9 * this.trainActions.NewRotation);
			this.trainActions.rotationValues (transform.rotation.eulerAngles.y);
		}

//		if(this.waitSeconds)
//		{
//			this.waitSeconds -= Time.deltaTime;
//			if(this.waitSeconds <= 0)
//			{
//				this.waitSeconds = false;
//
//			}
//		}

		transform.Translate(speed * direction.x * Time.deltaTime, speed * direction.y * Time.deltaTime , speed * direction.z * Time.deltaTime, Space.World);

	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.tag.Equals ("Curve")) 
		{
			this.trainActions.ColliderEnter = true;
			this.trainActions.colliderDetector (other);
			this.direction = this.trainActions.changeDirection ();
		}
		else if(other.tag.Equals ("RailroadSwitch") && !this.trainActions.ColliderEnter && this.alloToDetect)
		{
			this.alloToDetect = false;
			this.trainActions.ColliderEnter = true;
			this.trainActions.colliderDetector (other);
			StartCoroutine(this.waitToRotate(other));
		} 
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag.Equals ("Curve")) 
		{
			this.trainActions.ColliderEnter = false;
		}
		else if(other.tag.Equals ("RailroadSwitch") && this.alloToDetect)
		{
			this.trainActions.ColliderEnter = false;
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.tag.Equals ("ColorStation")) 
		{
			this.trainScore.trainArrival (other, this.gameObject, this.colorTrain);
			this.trainScore.destroyTrain(this.gameObject, this.currentActivity);
		}
	}

//	void OnDestroy()
//	{
//		if(StatisticsFocusRouteController.Current != null)
//		{
//			this.currentActivity.IsTrainSucceeded = this.trainScore.IsSucceeded;
//			StatisticsFocusRouteController.Current.addNewResult(this.currentActivity);
//		}
//	}
	#endregion

	#region Properties
	public ColorTrain ColorTrain
	{
		set { this.colorTrain = value; }
	}
	#endregion

}
