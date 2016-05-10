using UnityEngine;
using System.Collections;
using StridersVR.Domain.SpeedPack;

public class SuitcasePartController : MonoBehaviour {

	private SuitcasePart localPart; 

	private void attachPart()
	{
		if (localPart != null && localPart.AttachedPart != null) 
		{
			OrientationPoint _myNewOrientation = localPart.getActivePoint();

			this.transform.localPosition += _myNewOrientation.AttachedPosition;

			if(_myNewOrientation.AttachedRotation.z != 0)
			{
				this.transform.Find("SuitcasePart").localPosition += -_myNewOrientation.AttachedPosition;

			}
			else
			{
				this.transform.Find("SuitcasePart").localPosition += _myNewOrientation.AttachedPosition;
			}

			this.transform.rotation = Quaternion.Euler(_myNewOrientation.AttachedRotation);
			/* COLOCAR LA ANIMACION CORRESPONDIENTE */
		}
	}


	#region Script
	void Start () 
	{
		this.attachPart ();
	}

	void Update () 
	{
	
	}
	#endregion

	#region Properties
	public SuitcasePart LocalPart 
	{
		get { return this.localPart; }
		set { this.localPart = value; }
	}
	#endregion
}
