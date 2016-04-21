using UnityEngine;
using System.Collections.Generic;
using StridersVR.Domain.DotToDot;

namespace StridersVR.ScriptableObjects.DotToDot
{
	public class ScriptableObjectFigureModel : ScriptableObject
	{
		[SerializeField] private ModelTriangleEquilateral triangleEquilateralModel;
		[SerializeField] private ModelHourglass210 hourglass210Model;
		[SerializeField] private ModelLine lineModel;
		[SerializeField] private ModelLineWithTwo lineWithTwoModel;
		[SerializeField] private ModelThickArrow thickArrowModel;
		[SerializeField] private ModelRay rayModel;

		public List<FigureModel> obtainModels()
		{
			List<FigureModel> _list = new List<FigureModel>();

			_list.Add (new ModelTriangleEquilateral (this.triangleEquilateralModel.FigureName, this.triangleEquilateralModel.Prefab));
			_list.Add (new ModelHourglass210 (this.hourglass210Model.FigureName, this.hourglass210Model.Prefab));
			_list.Add (new ModelLine (this.lineModel.FigureName, this.lineModel.Prefab));
			_list.Add (new ModelLineWithTwo (this.lineWithTwoModel.FigureName, this.lineWithTwoModel.Prefab));
			_list.Add (new ModelThickArrow (this.thickArrowModel.FigureName, this.thickArrowModel.Prefab));
			_list.Add (new ModelRay (this.rayModel.FigureName, this.rayModel.Prefab));

			return _list;
		}

		public FigureModel getTriangleEquilateral()
		{
			return new ModelTriangleEquilateral (this.triangleEquilateralModel.FigureName, this.triangleEquilateralModel.Prefab);
		}

		public FigureModel getHourglass210()
		{
			return new ModelHourglass210 (this.hourglass210Model.FigureName, this.hourglass210Model.Prefab);
		}

		public FigureModel getLine()
		{
			return new ModelLine (this.lineModel.FigureName, this.lineModel.Prefab);
		}

		public FigureModel getLineWithTwo()
		{
			return new ModelLineWithTwo (this.lineWithTwoModel.FigureName, this.lineWithTwoModel.Prefab);
		}

		public FigureModel getThickArrow()
		{
			return new ModelThickArrow (this.thickArrowModel.FigureName, this.thickArrowModel.Prefab);
		}

		public FigureModel getRay()
		{
			return new ModelRay (this.rayModel.FigureName, this.rayModel.Prefab);
		}
	}
}

