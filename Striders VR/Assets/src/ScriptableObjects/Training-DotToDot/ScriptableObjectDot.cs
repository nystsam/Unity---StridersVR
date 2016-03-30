using UnityEngine;

namespace StridersVR.ScriptableObjects.DotToDot
{
	public class ScriptableObjectDot : ScriptableObject
	{
		[SerializeField] private GameObject dotPrefab;

		#region Properties
		public GameObject DotPrefab
		{
			get { return this.dotPrefab; }
		}
		#endregion
	}
}

