using UnityEngine;
using System.Collections;
using StridersVR.Domain.SpeedPack;

public class SuitcasePartController : MonoBehaviour {

	public GameObject spotsContainer;
	public GameObject bgPart;

	private Animator partAnimator;

	private int hashParam;
	private int hashAnim = 0;

	private bool allowToAnimate = false;
	private bool isAnimationDone = false;

	private SuitcasePart localPart; 

	private void displayAnimation()
	{
		if(!this.partAnimator.GetBool(this.hashParam))
			this.partAnimator.SetBool (this.hashParam, true);

		if (this.partAnimator.GetCurrentAnimatorStateInfo (0).shortNameHash == this.hashAnim) 
		{
			if (this.partAnimator.GetCurrentAnimatorStateInfo (0).normalizedTime > 1 && !this.partAnimator.IsInTransition (0)) 
			{
				this.allowToAnimate = false;
				this.isAnimationDone = true;
			}
		}
	}

	private void attachPart()
	{
		if (localPart != null && localPart.AttachedPart != null) 
		{
			OrientationPoint _myNewOrientation;

			this.transform.localPosition = localPart.AttachedPart.getGamePosition(this.transform.localPosition);

			_myNewOrientation = localPart.AttachedOrientation;

			this.transform.localPosition += -_myNewOrientation.AttachedPosition;
			this.transform.Find("SuitcasePart").localPosition += -_myNewOrientation.AttachedPosition; 
			this.hashParam = _myNewOrientation.getAnimHash(ref this.hashAnim);

			this.localPart.GamePosition = this.transform.localPosition * 2;
		}
		else if(localPart != null && localPart.AttachedPart == null)
		{
			this.localPart.GamePosition = this.transform.localPosition;
		}
	}

	public void reflectItems(GameObject nextPart)
	{
		GameObject _clone;
		Vector3 _nextItemPosition;
		SuitcasePart _nextSuitcasePart = nextPart.GetComponent<SuitcasePartController> ().localPart;
		Spot _previousSpot;
		int _localX = 0, _localY = 0;

		for (int index = 0; index < this.spotsContainer.transform.childCount; index ++) 
		{
			if(this.spotsContainer.transform.GetChild(index).GetComponent<SpotController>().LocalSpot.CurrentItem != null)
			{
				_previousSpot = this.spotsContainer.transform.GetChild(index).GetComponent<SpotController>().LocalSpot;

				if(_nextSuitcasePart.IsMainPart)
				{
					this.localPart.findSpotMatrixIndex(_previousSpot, ref _localX, ref _localY);

					_nextSuitcasePart.getOppositeIndex(this.localPart.AttachedOrientation, ref _localX, ref _localY);
					_nextItemPosition = _nextSuitcasePart.getSpotAtIndex(_localX,_localY).SpotPosition;
				}
				else
				{
					this.localPart.findSpotMatrixIndex(_previousSpot, ref _localX, ref _localY);

					_nextSuitcasePart.calculateIndex(this.localPart.AttachedOrientation, ref _localX, ref _localY);
					_nextItemPosition = _nextSuitcasePart.getSpotAtIndex(_localX,_localY).SpotPosition;
					_nextSuitcasePart.getSpotAtIndex(_localX,_localY).setItem(_previousSpot.CurrentItem); 
				}
				_clone = (GameObject)GameObject.Instantiate (_previousSpot.CurrentItem.ItemPrefab,
				                                             Vector3.zero,
				                                             Quaternion.Euler (Vector3.zero));
				_clone.transform.parent = nextPart.transform.Find ("SuitcasePart").Find ("Items");
				_clone.transform.localPosition = _nextItemPosition;
			}
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
		this.partAnimator = this.GetComponent<Animator> ();
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
