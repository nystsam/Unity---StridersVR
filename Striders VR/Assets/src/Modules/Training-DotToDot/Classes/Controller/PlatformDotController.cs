using UnityEngine;
using System.Collections.Generic;
using StridersVR.Domain.DotToDot;
using StridersVR.Modules.DotToDot.Logic.Representatives;
using StridersVR.ScriptableObjects.DotToDot;

public class PlatformDotController : MonoBehaviour 
{
	public ScriptableObjectDot dotData;

	private bool allowToDrawDots = false;
	private List<VertexPoint> vertexPointList;
	private RepresentativeDotPlatform dotPlatform;


	#region Script
	void Awake()
	{
		this.dotPlatform = new RepresentativeDotPlatform (this.gameObject);
		this.dotPlatform.setDotData (this.dotData);
	}

	void Update()
	{
		if (this.allowToDrawDots) 
		{
			this.allowToDrawDots = false;
			this.dotPlatform.drawDots(this.vertexPointList);
		}
	}
	#endregion

	#region Properties
	public bool AllowToDrawDots
	{
		get { return this.allowToDrawDots; }
		set { this.allowToDrawDots = value; }
	}

	public List<VertexPoint> VertexPointList
	{
		set { this.vertexPointList = value; }
	}
	#endregion
}
