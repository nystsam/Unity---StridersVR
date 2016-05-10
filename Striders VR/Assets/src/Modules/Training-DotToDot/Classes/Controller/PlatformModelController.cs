using UnityEngine;
using StridersVR.ScriptableObjects.DotToDot;
using StridersVR.Modules.DotToDot.Logic.Representatives;
using StridersVR.Domain.DotToDot;

public class PlatformModelController : MonoBehaviour 
{
	public ScriptableObjectFigureModel figureModelData;

	public GameObject dotPlatform;
	public GameObject dotReferee;
	public GameObject playerModelFigureContainer;

	private bool allowToCreateModel;

	private RepresentativeModelFigure modelFigure;

	private void createModelFigure()
	{
		if (this.allowToCreateModel) 
		{
			this.allowToCreateModel = false;
			this.modelFigure.createModel();
			this.dotReferee.GetComponent<RefereeController> ().setNumberOfStripes(this.modelFigure.NumberOfStripesAssigned); 
		}

	}


	#region Script
	void Awake () 
	{
		this.allowToCreateModel = false;

		this.modelFigure = new RepresentativeModelFigure (this.gameObject, this.playerModelFigureContainer);
//		this.modelFigure.createFigure ();
//		this.dotPlatform.GetComponent<PlatformDotController> ().VertexPointList = this.modelFigure.VertexPointList;
//		this.dotPlatform.GetComponent<PlatformDotController> ().AllowToDrawDots = true;
//		this.dotReferee.GetComponent<RefereeController> ().setNumberOfPoitns (this.modelFigure.NumberOfPoints);

	}

	void Update()
	{
		this.createModelFigure ();	
	}
	#endregion

	#region Properties
	public bool AllowToCreateModel
	{
		set { this.allowToCreateModel = value; }
	}

	public PointsContainer SetPoints
	{
		set { this.modelFigure.PointsFromController = value; }
	}
	#endregion
}
