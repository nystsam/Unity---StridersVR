using UnityEngine;
using System.Collections;
using StridersVR.Domain.SpeedPack;

public class SuitcasePartController : MonoBehaviour {

	public GameObject spotsContainer;
	public GameObject bgPart;

	private SuitcasePart localPart; 

	private void attachPart()
	{
		if (localPart != null && localPart.AttachedPart != null) 
		{
			OrientationPoint _myNewOrientation = localPart.getActivePoint();

			this.transform.localPosition += _myNewOrientation.AttachedPosition;
			this.transform.Find("SuitcasePart").localPosition += _myNewOrientation.AttachedPosition; 
//			if(_myNewOrientation.AttachedRotation.z != 0)
//			{
//				this.transform.Find("SuitcasePart").localPosition += -_myNewOrientation.AttachedPosition;
//
//			}
//			else
//			{
//				this.transform.Find("SuitcasePart").localPosition += _myNewOrientation.AttachedPosition;
//			}
//
//			this.transform.rotation = Quaternion.Euler(_myNewOrientation.AttachedRotation);

			/* COLOCAR LA ANIMACION CORRESPONDIENTE */
		}
	}

	public void changeMainPartColor()
	{
		Material _spotsMaterial = Resources.Load("Materials/Training-SpeedPack/MatMainPartSpots", typeof(Material)) as Material;
		Material _bgMaterial = Resources.Load("Materials/Training-SpeedPack/MatMainPartBg", typeof(Material)) as Material;

		this.bgPart.GetComponent<MeshRenderer> ().material = _bgMaterial;

		for (int index = 0; index < this.spotsContainer.transform.childCount; index ++) 
		{
			this.spotsContainer.transform.GetChild(index).GetComponent<MeshRenderer>().material = _spotsMaterial;
			this.spotsContainer.transform.GetChild(index).GetComponent<SpotController>().LocalSpot = this.localPart.getSpotAtIndex(index);
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
