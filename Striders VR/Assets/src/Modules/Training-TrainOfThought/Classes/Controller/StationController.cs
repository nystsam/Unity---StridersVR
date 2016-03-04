using UnityEngine;
using System.Collections;
using StridersVR.Domain.TrainOfThought;

public class StationController : MonoBehaviour {

	public ColorStation colorStation;

	#region Properties
	public ColorStation ColorStation
	{
		get { return this.colorStation; }
		set { this.colorStation = value; }
	}
	#endregion

}
