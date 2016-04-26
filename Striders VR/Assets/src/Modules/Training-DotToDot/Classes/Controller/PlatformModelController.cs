using UnityEngine;
using StridersVR.ScriptableObjects.DotToDot;
using StridersVR.Modules.DotToDot.Logic.Representatives;

public class PlatformModelController : MonoBehaviour 
{
	public ScriptableObjectFigureModel figureModelData;

	public GameObject dotPlatform;
	public GameObject dotReferee;
	public GameObject playerModelFigureContainer;
	public GameObject checkInPrefab;

	private bool checkInInstatiated;
	private bool checkInDone;

	private RepresentativeModelFigure modelFigure;

	private void checkIn()
	{
		if (!this.checkInInstatiated) 
		{
			GameObject _checkinClone; 

			_checkinClone = (GameObject)GameObject.Instantiate (this.checkInPrefab, Vector3.zero, Quaternion.Euler (new Vector3(350,270,0)));
			_checkinClone.transform.parent = this.playerModelFigureContainer.transform;
			_checkinClone.transform.localPosition = new Vector3 (3, -0.5f, 3);
			_checkinClone.GetComponent<CheckInController>().FigureContainer = this.gameObject;
			this.checkInInstatiated = true;
		} 
		else if (this.checkInInstatiated && this.checkInDone) 
		{
			this.modelFigure.DotContainer = this.dotReferee.GetComponent<RefereeController> ().dotContainer;
			this.modelFigure.EndPointContainer = this.dotReferee.GetComponent<RefereeController> ().endPointsContainer;
			this.modelFigure.removeCurrentFigureModel();

			this.dotReferee.GetComponent<RefereeController> ().ChangeFigureModel = false;
			this.dotReferee.GetComponent<RefereeController> ().IsHoldingDot = false;
			this.checkInDone = false;
			this.checkInInstatiated = false;
		}
	}


	#region Script
	void Awake () 
	{
		this.checkInInstatiated = false;
		this.checkInDone = false;

		this.modelFigure = new RepresentativeModelFigure (this.gameObject, this.playerModelFigureContainer);
		this.modelFigure.createFigure ();
		this.dotPlatform.GetComponent<PlatformDotController> ().VertexPointList = this.modelFigure.VertexPointList;
		this.dotPlatform.GetComponent<PlatformDotController> ().AllowToDrawDots = true;
		this.dotReferee.GetComponent<RefereeController> ().setNumberOfPoitns (this.modelFigure.NumberOfPoints);

	}

	void Update()
	{
		if (this.dotReferee.GetComponent<RefereeController> ().ChangeFigureModel) 
		{
			this.checkIn();
		}
	}
	#endregion

	#region Properties
	public bool CheckInDone
	{
		set { this.checkInDone = value; }
	}
	#endregion
}
