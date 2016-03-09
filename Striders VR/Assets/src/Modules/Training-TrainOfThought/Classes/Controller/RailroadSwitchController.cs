using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RailroadSwitchController : MonoBehaviour {

	public List<Vector3> directions;
	public int selectedDirectionIndex;

	private float rotationSpeed = 30f;
	private GameObject arrowDirection;
	private float rotationLimit;
	private float anglesRotated;
	private float firstDirectionAngle;
	private float secondDirectionAngle;
	private bool firstSwitchOpen = false;
	private bool secondSwitchOpen = false;
	private bool checkOrientation = false;
	private float orientation = 1;

	public Vector3 getSelectedDitecion
	{
		get { return this.directions [this.selectedDirectionIndex]; }
	}
	
	public void changeDirectionIndex()
	{
		if (this.selectedDirectionIndex == 1)
			this.selectedDirectionIndex = 0;
		else
			this.selectedDirectionIndex = 1;
	}
	
	private void setRotationAngle(ref float rotationAngle, Vector3 direction)
	{
		if (direction.x > 0) {
			rotationAngle = 180f;
			this.arrowDirection.transform.localEulerAngles = new Vector3 (-90, 180, 0);
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
		
		if (this.arrowDirection.transform.localEulerAngles.y >= _maxAngle && (_maxAngle != 270 || Mathf.Min (this.firstDirectionAngle, this.secondDirectionAngle) != 0)) {
			if (!this.checkOrientation) {
				this.orientation = -this.orientation;
				this.checkOrientation = true;
				
			}	
		} else if (_maxAngle == 270 && Mathf.Min (this.firstDirectionAngle, this.secondDirectionAngle) == 0) {
			if (!this.checkOrientation) {
				if (Mathf.Round(this.arrowDirection.transform.localEulerAngles.y) == 0) {
					this.orientation = -1f;
				} else {
					this.orientation = 1f;
				}
				this.checkOrientation = true;
				this.rotationLimit = 90f;
				
			}
		}
		this.arrowDirection.transform.localEulerAngles = this.arrowDirection.transform.localEulerAngles + new Vector3(0, this.orientation * this.rotationSpeed, 0);
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


	#region Script
	void Start () 
	{
		this.arrowDirection = this.transform.FindChild ("RailroadSwitchArrowDirection").gameObject;
		this.selectedDirectionIndex = Random.Range (0,2);
		this.setRotationAngle (ref this.firstDirectionAngle, this.directions [0]);
		this.setRotationAngle (ref this.secondDirectionAngle, this.directions [1]);

		if (this.selectedDirectionIndex == 0) {
			this.arrowDirection.transform.localEulerAngles = new Vector3 (-90, this.firstDirectionAngle, 0);
			this.firstSwitchOpen = true;
		} else {
			this.arrowDirection.transform.localEulerAngles = new Vector3 (-90, this.secondDirectionAngle, 0);
			this.secondSwitchOpen = true;
		}
		this.rotationLimit = Mathf.Abs (this.firstDirectionAngle - this.secondDirectionAngle);
	}

	void Update()
	{
		if (this.arrowDirection != null) 
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
