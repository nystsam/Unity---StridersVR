using UnityEngine;
using System.Collections.Generic;
using StridersVR.Domain.DotToDot;

namespace StridersVR.ScriptableObjects.DotToDot
{
	public class ScriptableObjectFigureModel : ScriptableObject
	{
		[SerializeField] private ModelTriangleEquilateral triangleEquilateralModel;
		[SerializeField] private ModelHourglass210 hourglass210Model ;

		public List<FigureModel> obtainModels()
		{
			List<FigureModel> _list = new List<FigureModel>();

			_list.Add (new ModelTriangleEquilateral (this.triangleEquilateralModel.FigureName, this.triangleEquilateralModel.Prefab));
			_list.Add (new ModelHourglass210 (this.hourglass210Model.FigureName, this.hourglass210Model.Prefab));

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
	}
}

