using UnityEngine;
using System.Collections;

public class CheckInController : MonoBehaviour {

	public GameObject firstPart;
	public GameObject secondPart;

	private GameObject dotContainer;

	private Animator anim;

	private int stateFirstPartHash;
	private int stateSecondPartHash;
	private int allowToFirstHash;
	private int allowToSecondHash;

	private float fadeOutTimer;

	private bool firstPartDone;
	private bool secondPartDone;

	private void animateSecondPart ()
	{
		this.secondPart.SetActive (true);
		this.anim.SetBool (this.allowToSecondHash, true);
	}

	private void animateFirstPart()
	{
		if (!firstPartDone) 
		{
			AnimatorStateInfo stateInfo = this.anim.GetCurrentAnimatorStateInfo (0);
			if (stateInfo.shortNameHash == this.stateFirstPartHash) 
			{
				this.firstPartDone = true;
				this.animateSecondPart ();
			}
		} 
		else if (this.firstPartDone && !this.secondPartDone) 
		{
			AnimatorStateInfo stateInfo = this.anim.GetCurrentAnimatorStateInfo (0);
			if (stateInfo.shortNameHash == this.stateSecondPartHash) 
			{
				this.secondPartDone = true;
			}
		} 
		else if (this.secondPartDone) 
		{
			this.fadeOutTimer += Time.deltaTime;
			if(this.fadeOutTimer > 0.5f)
			{
				this.dotContainer.GetComponent<PlatformDotController>().CheckInDone = true;
				GameObject.Destroy(this.gameObject);
			}
		}
	}

	#region Script
	void Awake () 
	{
		this.firstPartDone = false;
		this.secondPartDone = false;
		this.fadeOutTimer = 0;

		this.anim = this.GetComponent<Animator> ();
		this.stateFirstPartHash = Animator.StringToHash("AnimFirstPart");
		this.stateSecondPartHash = Animator.StringToHash("AnimSecondPart");
		this.allowToFirstHash = Animator.StringToHash("AllowToFirst");
		this.allowToSecondHash = Animator.StringToHash("AllowToSecond");

		this.anim.SetBool (this.allowToFirstHash, true);
	}

	void Update () 
	{
		this.animateFirstPart ();
	}
	#endregion

	#region Properties
	public GameObject DotContainer
	{
		set { this.dotContainer = value; }
	}
	#endregion
}
