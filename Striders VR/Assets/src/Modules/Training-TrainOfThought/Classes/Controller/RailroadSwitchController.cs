using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RailroadSwitchController : MonoBehaviour {

	public GameObject switchDirection;
	public GameObject switchPointer;

	public ParticleSystem switchAura;

	public TextMesh switchNumber;

	public List<Vector3> directions;

	public int selectedDirectionIndex;


	private float rotationSpeed = 30f;
	private float rotationLimit;
	private float anglesRotated;
	private float firstDirectionAngle;
	private float secondDirectionAngle;
	private float orientation = 1;

	private bool firstSwitchOpen = false;
	private bool secondSwitchOpen = false;
	private bool checkOrientation = false;
	private bool isFirstChange = false;

	private int directionChanged = 0;


	public void newTrainApproaching()
	{
		this.directionChanged = 0;
		this.isFirstChange = false;
	}

	public int GetDirectionChangedCount()
	{
		return this.directionChanged;
	}

	public bool FirstChanged()
	{
		return this.isFirstChange;
	}

	public void setSwitchNumber(int number)
	{
		string _text = number.ToString();

		this.switchNumber.text = _text;
		this.switchNumber.gameObject.SetActive(false);
		this.switchPointer.transform.GetChild(0).GetComponent<TextMesh>().text = _text;
	}

	public void activateIndicators(bool val)
	{
		if(val)
		{
			this.switchAura.Play();

		}
		else
		{
			this.switchAura.Clear();
			this.switchAura.Stop();
		}
		this.switchNumber.gameObject.SetActive(!val);
		this.switchPointer.SetActive(val);
	}

	public Vector3 getSelectedDitecion
	{
		get { return this.directions [this.selectedDirectionIndex]; }
	}

	public void changeDirectionIndex()
	{
		if(!this.isFirstChange)
			this.isFirstChange = true;

		if (this.selectedDirectionIndex == 1)
			this.selectedDirectionIndex = 0;
		else
			this.selectedDirectionIndex = 1;

		this.directionChanged ++;
	}


	#region Switch Rotation Features
	private void setRotationAngle(ref float rotationAngle, Vector3 direction)
	{
		if (direction.x > 0) {
			rotationAngle = 180f;
			this.switchDirection.transform.localEulerAngles = new Vector3 (-90, 180, 0);
		} else if (direction.x < 0) {
			rotationAngle = 0f;
		} else if( direction.z > 0){
			rotationAngle = 90f;
		} else if(direction.z < 0){
			rotationAngle = 270f;
		}
	}
	
	private void rotateSwitch(ref bool isOpen)
	{
		float _maxAngle = Mathf.Max (this.firstDirectionAngle, this.secondDirectionAngle);
		
		if (this.switchDirection.transform.localEulerAngles.y >= _maxAngle && (_maxAngle != 270 || Mathf.Min (this.firstDirectionAngle, this.secondDirectionAngle) != 0)) {
			if (!this.checkOrientation) {
				this.orientation = -this.orientation;
				this.checkOrientation = true;
				
			}	
		} else if (_maxAngle == 270 && Mathf.Min (this.firstDirectionAngle, this.secondDirectionAngle) == 0) {
			if (!this.checkOrientation) {
				if (Mathf.Round(this.switchDirection.transform.localEulerAngles.y) == 0) {
					this.orientation = -1f;
				} else {
					this.orientation = 1f;
				}
				this.checkOrientation = true;
				this.rotationLimit = 90f;
				
			}
		}
		this.switchDirection.transform.localEulerAngles = this.switchDirection.transform.localEulerAngles + new Vector3(0, this.orientation * this.rotationSpeed, 0);
		this.anglesRotated += Mathf.Abs(rotationSpeed);
		if (anglesRotated >= this.rotationLimit) {
			this.anglesRotated = 0f;
			isOpen = true;
			if (this.checkOrientation) {
				this.orientation = 1f;
				this.checkOrientation = false;
			}
		}
	}
	#endregion

	#region Script
	void Start () 
	{
		this.switchPointer.SetActive(false);
		this.selectedDirectionIndex = Random.Range (0,2);
		this.setRotationAngle (ref this.firstDirectionAngle, this.directions [0]);
		this.setRotationAngle (ref this.secondDirectionAngle, this.directions [1]);

		if (this.selectedDirectionIndex == 0) {
			this.switchDirection.transform.localEulerAngles = new Vector3 (-90, this.firstDirectionAngle, 0);
			this.firstSwitchOpen = true;
		} else {
			this.switchDirection.transform.localEulerAngles = new Vector3 (-90, this.secondDirectionAngle, 0);
			this.secondSwitchOpen = true;
		}
		this.rotationLimit = Mathf.Abs (this.firstDirectionAngle - this.secondDirectionAngle);
	}

	void Update()
	{
		if (this.switchDirection != null) 
		{
			if (this.selectedDirectionIndex == 0 && !this.firstSwitchOpen) {
				this.rotateSwitch (ref this.firstSwitchOpen);
				this.secondSwitchOpen = false;
			} 
			else if(this.selectedDirectionIndex == 1 && !this.secondSwitchOpen) {
				this.rotateSwitch (ref this.secondSwitchOpen);
				this.firstSwitchOpen = false;
			}
		}
	}
	#endregion
}
