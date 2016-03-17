using UnityEngine;
using System.Collections;
using StridersVR.Domain.DotToDot;

public class FigureModelController : MonoBehaviour {

	private FigureModel figureModel = null;
	private bool isBase = false;
	private int figureVertexCount;
	private int numberFiguresSupported = 0;

	public int neighbourVertexCount(int index)
	{
		return this.figureModel.VertexPointList [index].NeighbourVectorList.Count;
	}

	public bool freeVertexAvailable()
	{
		if (this.numberFiguresSupported < (this.figureVertexCount - 1)) 
			return true;
		return false;
	}

	public void addNewNeighbour(int vertexPointListIndex, FigureModel figureModelNeighbour)
	{
		this.figureModel.VertexPointList [vertexPointListIndex].addNewNeighbour (figureModelNeighbour.VertexPointList[0].NeighbourVectorList);
	}

	public void setFigureVertexCount()
	{
		this.figureVertexCount = this.figureModel.VertexPointList.Count;
	}

	#region Properties
	public FigureModel FigureModel
	{
		get { return this.figureModel; }
		set { this.figureModel = value; }
	}

	public bool IsBase
	{
		get { return this.isBase; }
		set { this.isBase = value; }
	}

	public int NumberFiguresSupported
	{
		get { return this.numberFiguresSupported; }
		set { this.numberFiguresSupported = value; }
	}
	#endregion
}
