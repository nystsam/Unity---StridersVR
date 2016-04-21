using UnityEngine;
using StridersVR.ScriptableObjects.DotToDot;
using StridersVR.Modules.DotToDot.Logic.Representatives;

public class PlatformModelController : MonoBehaviour 
{
	public ScriptableObjectFigureModel figureModelData;
	public GameObject dotPlatform;
	public GameObject dotReferee;
	public GameObject playerModelFigureContainer;

	private RepresentativeModelFigure modelFigure;


	#region Script
	void Awake () 
	{
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
			Debug.Log ("Cambiar");
			this.dotReferee.GetComponent<RefereeController> ().ChangeFigureModel = false;
		}
	}
	#endregion
}
