using UnityEngine;
using System.Collections;

public class SwitchButtonController : MonoBehaviour {

	public float spring;
	public float triggerDistance;
	public float max_distance;
	public float min_distance;
	public GameObject pressedAddon;
	
	private bool is_pressed;
	private Vector3 resting_position;
	private Material childMaterial;

	#region Script
	void Start()
	{
		this.resting_position = transform.localPosition;
		this.is_pressed = false;
		childMaterial = pressedAddon.GetComponent<MeshRenderer>().material;
	}
	
	void Update()
	{
		this.constraintMovement();
		this.appliySpring ();
		this.checkTrigger ();
	}
	#endregion

	private void constraintMovement()
	{
		Vector3 _local_position = transform.localPosition;
		_local_position.y = Mathf.Clamp (_local_position.y, this.min_distance, this.max_distance);
		transform.localPosition = _local_position;
	}
	
	private void appliySpring()
	{
		transform.GetComponent<Rigidbody> ().AddRelativeForce (-this.spring * (transform.localPosition - this.resting_position));
	}

	private void checkTrigger()
	{
		if (!this.is_pressed) {
			if (transform.localPosition.y < this.triggerDistance) {
				this.buttonPressed(1);
				this.is_pressed = true;
			}
		} else if (this.is_pressed) {
			if (transform.localPosition.y > this.triggerDistance) {
				this.buttonPressed(0);
				this.is_pressed = false;
			}
		}
	}

	private void buttonPressed(float alpha)
	{
		childMaterial.color = new Color(childMaterial.color.r,childMaterial.color.g, childMaterial.color.b, alpha);

	}

}
