using UnityEngine;
using System.Collections;
using StridersVR.Domain.DotToDot;

public class FigureModelController : MonoBehaviour {

	private FigureModel figureModel = null;

	#region Properties
	public FigureModel FigureModel
	{
		get { return this.figureModel; }
		set { this.figureModel = value; }
	}
	#endregion
}
