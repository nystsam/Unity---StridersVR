using UnityEngine;
using StridersVR.Modules.DotToDot.Logic.Representatives;

public class PlatformModelController : MonoBehaviour 
{
	private RepresentativeModelFigure modelFigure;

	private string gameDificulty;

	private GameObject pointManager;

	private void modelCreation()
	{
		this.modelFigure.createModel ();
		this.pointManager.GetComponent<PointManagerController> ().setModel (this.modelFigure.CurrentModel);
	}

	#region Script
	void Awake()
	{
		this.gameDificulty = GameObject.FindGameObjectWithTag ("StaticUser").GetComponent<StaticUserController> ().Training.Difficulty;
		this.pointManager = GameObject.FindGameObjectWithTag("GameController");
		this.modelFigure = new RepresentativeModelFigure (this.gameObject, this.gameDificulty);
		this.modelCreation ();
	}

	void Update()
	{
	
	}
	#endregion
//	public ScriptableObjectFigureModel figureModelData;
//
//	public GameObject dotPlatform;
//	public GameObject dotReferee;
//	public GameObject playerModelFigureContainer;
//
//	private bool allowToCreateModel;
//
//	private RepresentativeModelFigure modelFigure;
//
//	private void createModelFigure()
//	{
//		if (this.allowToCreateModel) 
//		{
//			this.allowToCreateModel = false;
//			//this.modelFigure.createModel();
//			//this.dotReferee.GetComponent<RefereeController> ().setNumberOfStripes(this.modelFigure.NumberOfStripesAssigned); 
//		}
//
//	}
//
//
//	#region Script
//	void Awake () 
//	{
//		this.allowToCreateModel = false;
//
//		this.modelFigure = new RepresentativeModelFigure (this.gameObject, this.playerModelFigureContainer);
////		this.modelFigure.createFigure ();
////		this.dotPlatform.GetComponent<PlatformDotController> ().VertexPointList = this.modelFigure.VertexPointList;
////		this.dotPlatform.GetComponent<PlatformDotController> ().AllowToDrawDots = true;
////		this.dotReferee.GetComponent<RefereeController> ().setNumberOfPoitns (this.modelFigure.NumberOfPoints);
//
//	}
//
//	void Update()
//	{
//		this.createModelFigure ();	
//	}
//	#endregion
//
//	#region Properties
//	public bool AllowToCreateModel
//	{
//		set { this.allowToCreateModel = value; }
//	}
//
//	public PointsContainer SetPoints
//	{
//		set { this.modelFigure.PointsFromController = value; }
//	}
//	#endregion
}
