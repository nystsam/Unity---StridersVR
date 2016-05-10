using UnityEngine;
using System.Collections.Generic;
using StridersVR.Domain.DotToDot;
using StridersVR.Modules.DotToDot.Logic.Representatives;

public class PlatformDotController : MonoBehaviour 
{
	public GameObject referee;
	public GameObject platformModel;


	private GameObject checkInPrefab;
	private GameObject pointDotPrefab;

	private bool checkInInstatiated;
	private bool checkInDone;

	private PointsContainer pointsContainer;

	private RepresentativeDotPlatform dotPlatform;

	private void checkIn()
	{
		if (!this.checkInInstatiated) 
		{
			GameObject _checkinClone; 
			
			_checkinClone = (GameObject)GameObject.Instantiate (this.checkInPrefab, Vector3.zero, Quaternion.Euler (new Vector3(350,270,0)));
			_checkinClone.transform.parent = this.transform.parent;
			_checkinClone.transform.localPosition = new Vector3 (0, 25, 2);
			_checkinClone.GetComponent<CheckInController>().DotContainer = this.gameObject;
			this.checkInInstatiated = true;
		} 
		else if (this.checkInInstatiated && this.checkInDone) 
		{
			this.referee.GetComponent<RefereeController> ().ChangeFigureModel = false;
			this.checkInDone = false;
			this.checkInInstatiated = false;

			this.dotPlatform.removeCurrentFigureModel(this.referee.GetComponent<RefereeController> ().endPointsContainer);
			this.pointsContainer = this.dotPlatform.drawDots();;
			this.platformModel.GetComponent<PlatformModelController> ().AllowToCreateModel = true;
			this.platformModel.GetComponent<PlatformModelController> ().SetPoints = this.pointsContainer;
		}
	}


	#region Script
	void Awake()
	{
		this.checkInPrefab = Resources.Load ("Prefabs/Training-DotToDot/Check", typeof(GameObject)) as GameObject;
		this.pointDotPrefab = Resources.Load ("Prefabs/Training-DotToDot/GamePoint", typeof(GameObject)) as GameObject;

		this.dotPlatform = new RepresentativeDotPlatform (this.gameObject, this.pointDotPrefab);
//		this.dotPlatform.setDotData (this.dotData);
		this.pointsContainer = this.dotPlatform.drawDots();
	}

	void Start()
	{
		this.platformModel.GetComponent<PlatformModelController> ().AllowToCreateModel = true;
		this.platformModel.GetComponent<PlatformModelController> ().SetPoints = this.pointsContainer;
	}

	void Update()
	{
		if (this.referee.GetComponent<RefereeController> ().ChangeFigureModel) 
		{
			this.checkIn();
		}
//		if (this.allowToDrawDots) 
//		{
//			this.allowToDrawDots = false;
//			this.dotPlatform.drawDots(this.vertexPointList);
//
//		}
	}
	#endregion

	#region Properties
	public bool CheckInDone
	{
		set { this.checkInDone = value; }
	}
	#endregion
}
