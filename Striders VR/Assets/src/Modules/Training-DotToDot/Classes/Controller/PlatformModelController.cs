using UnityEngine;
using StridersVR.ScriptableObjects.DotToDot;
using StridersVR.Modules.DotToDot.Logic.Representatives;

public class PlatformModelController : MonoBehaviour 
{
	public ScriptableObjectFigureModel figureModelData;

	private RepresentativeModelFigure modelFigure;


	#region Script
	void Awake () 
	{
		this.modelFigure = new RepresentativeModelFigure (this.gameObject);
		this.modelFigure.createFigure ();
	}
	
	void Update () 
	{
		
	}
	#endregion
}
