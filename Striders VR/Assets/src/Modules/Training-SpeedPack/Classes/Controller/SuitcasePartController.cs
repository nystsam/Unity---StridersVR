using UnityEngine;
using System.Collections;
using StridersVR.Domain.SpeedPack;

public class SuitcasePartController : MonoBehaviour {

	public GameObject spotsContainer;
	public GameObject bgPart;

	private Animator partAnimation;

	private int hashAnim;

	private bool allowToAnimate = false;
	private bool isAnimationDone = false;

	private SuitcasePart localPart; 

	private void displayAnimation()
	{
		if(!this.partAnimation.GetBool(this.hashAnim))
			this.partAnimation.SetBool (this.hashAnim, true);

		if (this.partAnimation.GetCurrentAnimatorStateInfo (0).normalizedTime > 1.3f) 
		{
			this.allowToAnimate = false;
			this.isAnimationDone = true;
		}
	}

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
			this.hashAnim = _myNewOrientation.getAnimHash();
		}
	}

	public void reflectItems(GameObject previousPart)
	{
//		SuitcasePart _previousSuitcasePart = previousPart.GetComponent<SuitcasePartController> ().localPart;
//		Spot _previousSpot;
//		int _localX = 0, _localY = 0;
//
//		for (int index = 0; index < this.spotsContainer.transform.childCount; index ++) 
//		{
//			if(this.spotsContainer.transform.GetChild(index).GetComponent<SpotController>().LocalSpot.CurrentItem != null)
//			{
//				this.localPart.findSpotMatrixIndex(this.spotsContainer.transform.GetChild(index).GetComponent<SpotController>().LocalSpot,
//				                                   _localX, _localY);
//				_previousSpot = _previousSuitcasePart.getOppositeSpot(_localX, _localY);
//			}
//		}
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

	public void assignSpots()
	{
		for (int index = 0; index < this.spotsContainer.transform.childCount; index ++) 
		{
			this.spotsContainer.transform.GetChild(index).GetComponent<SpotController>().LocalSpot = this.localPart.getSpotAtIndex(index);
		}
	}

	public void allowAnimation()
	{
		this.allowToAnimate = true;
	}


	#region Script
	void Awake()
	{
		this.partAnimation = this.GetComponent<Animator> ();
	}

	void Start () 
	{
		this.attachPart ();
	}

	void Update()
	{
		if (this.allowToAnimate) 
		{
			this.displayAnimation ();
		}
	}
	#endregion

	#region Properties
	public SuitcasePart LocalPart 
	{
		get { return this.localPart; }
		set { this.localPart = value; }
	}
	public bool IsAnimationDone
	{
		get { return this.isAnimationDone; }
	}
	#endregion
}
