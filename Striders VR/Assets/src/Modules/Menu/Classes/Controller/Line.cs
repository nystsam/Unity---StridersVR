using UnityEngine;
using System.Collections;

public class Line : MonoBehaviour {

	public Vector3 destination;

	private Vector3 origin;

	private LineRenderer lineRenderer;

	void Start () 
	{
		this.origin = this.transform.position;
		this.lineRenderer = this.GetComponent<LineRenderer>();

		this.lineRenderer.SetWidth(.02f, .02f);
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.origin = this.transform.position;
		this.lineRenderer.SetPosition(0,this.origin);
		this.lineRenderer.SetPosition(1,this.destination);
	}
}
