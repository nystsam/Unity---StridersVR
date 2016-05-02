using UnityEngine;
using System.Collections.Generic;
using StridersVR.Domain.DotToDot;
using StridersVR.Modules.DotToDot.Logic.Representatives;
using StridersVR.ScriptableObjects.DotToDot;

public class PlatformDotController : MonoBehaviour 
{
	public GameObject platformModel;

	public ScriptableObjectDot dotData;

	private bool allowToDrawDots = false;

	private PointsContainer pointsContainer;

	private RepresentativeDotPlatform dotPlatform;


	#region Script
	void Awake()
	{
		this.dotPlatform = new RepresentativeDotPlatform (this.gameObject, this.dotData.DotPrefab);
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
		if (this.allowToDrawDots) 
		{
			this.allowToDrawDots = false;
			//this.dotPlatform.drawDots(this.vertexPointList);

		}
	}
	#endregion

	#region Properties
	public bool AllowToDrawDots
	{
		get { return this.allowToDrawDots; }
		set { this.allowToDrawDots = value; }
	}
	#endregion
}
