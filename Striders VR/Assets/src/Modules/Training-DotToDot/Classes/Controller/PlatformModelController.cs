using UnityEngine;
using StridersVR.ScriptableObjects.DotToDot;
using StridersVR.Modules.DotToDot.Logic.Representatives;

public class PlatformModelController : MonoBehaviour 
{
	public ScriptableObjectFigureModel figureModelData;
	public GameObject dotPlatform;

	private RepresentativeModelFigure modelFigure;


	#region Script
	void Awake () 
	{
		this.modelFigure = new RepresentativeModelFigure (this.gameObject);
		this.modelFigure.createFigure ();
		this.dotPlatform.GetComponent<PlatformDotController> ().VertexPointList = this.modelFigure.VertexPointList;
		this.dotPlatform.GetComponent<PlatformDotController> ().AllowToDrawDots = true;
	}
	#endregion
}
