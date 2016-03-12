using UnityEngine;
using System.Collections.Generic;
using StridersVR.Domain.DotToDot;

namespace StridersVR.ScriptableObjects.DotToDot
{
	public class ScriptableObjectFigureModel : ScriptableObject
	{
		[SerializeField] private ModelTriangleEquilateral triangleEquilateralModel;
		[SerializeField] private ModelLine lineModel;
		[SerializeField] private ModelHourglass210 hourglass210Model ;

		public List<FigureModel> obtainModels()
		{
			List<FigureModel> _list = new List<FigureModel>();

			_list.Add (this.triangleEquilateralModel);
			_list.Add (this.lineModel);
			_list.Add (this.hourglass210Model);

			return _list;
		}

	}
}

