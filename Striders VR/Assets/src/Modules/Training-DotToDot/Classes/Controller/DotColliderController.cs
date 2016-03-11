using UnityEngine;
using System.Collections;

public class DotColliderController : MonoBehaviour {

	public Light colliderLight;

	private bool turnOn = false;
	private Vector3 parenCurrentRotation;

	void Start()
	{
		this.parenCurrentRotation = transform.parent.localEulerAngles;
	}

	void Update()
	{
		if (turnOn && this.colliderLight.intensity < 2) 
		{
			this.colliderLight.intensity += 0.1f;
		} 
		else if (!turnOn && this.colliderLight.intensity > 0.2f) 
		{
			this.colliderLight.intensity -= 0.1f;
		}
	}

	void OnTriggerEnter(Collider other)
	{
		this.turnOn = true;
	}

	void OnTriggerExit()
	{
		this.turnOn = false;
		transform.parent.localEulerAngles = this.parenCurrentRotation;
		transform.parent.localScale = new Vector3 (1, 1, 1);
	}
}
