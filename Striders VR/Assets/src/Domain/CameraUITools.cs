using UnityEngine;
using System.Collections;

public class CameraUITools : MonoBehaviour {

	public static CameraUITools Current;

	public Vector3 toolsUpPos;
	public Vector3 toolsOutPos;
	public Vector3 toolsUpRotation;
	public Vector3 toolsOutRotation;

	private Vector3 velocity;

	[SerializeField]
	private bool isToolsUp = false;
	[SerializeField]
	private bool reachedPos = true;

	public CameraUITools ()
	{
		Current = this;
	}

	public void ChangePosition(bool val)
	{
		this.reachedPos = false;
		this.isToolsUp = val;
	}

	private void setNewPosition(Vector3 pos)
	{
		if(Vector3.Distance(this.transform.position, pos) < 0.02f)
		{
			this.reachedPos = true;
			this.transform.position = pos;
		}
	}

	void Start () 
	{
		this.velocity = new Vector3(6,6,6);
	}

	void Update () 
	{
		if(!reachedPos)
		{
			if(this.isToolsUp)
			{
				this.transform.position = Vector3.SmoothDamp(this.transform.position,
				                                             this.toolsUpPos,
				                                             ref this.velocity,
				                                             0.3f);
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
				                                           Quaternion.Euler(this.toolsUpRotation),
				                                           Time.deltaTime * 5);

				this.setNewPosition(this.toolsUpPos);
			}
			else
			{
				this.transform.position = Vector3.SmoothDamp(this.transform.position,
				                                             this.toolsOutPos,
				                                             ref this.velocity,
				                                             0.3f);
				this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
				                                           Quaternion.Euler(this.toolsOutRotation),
				                                           Time.deltaTime * 5);
				this.setNewPosition(this.toolsOutPos);
			}
		}
	}
}
