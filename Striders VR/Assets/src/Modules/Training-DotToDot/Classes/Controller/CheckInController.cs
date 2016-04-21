using UnityEngine;
using System.Collections;

public class CheckInController : MonoBehaviour {

	public GameObject firstPart;
	public GameObject secondPart;

	private Animator anim;

	private int stateFirstPartHash;
	private int allowToFirstHash;
	private int allowToSecondHash;

	private bool alreadySized;

	private void animateSecondPart ()
	{
		this.secondPart.SetActive (true);
		this.anim.SetBool (this.allowToSecondHash, true);
	}

	private void animateFirstPart()
	{
		if (!alreadySized) 
		{
			AnimatorStateInfo stateInfo = this.anim.GetCurrentAnimatorStateInfo (0);
			if (stateInfo.shortNameHash == this.stateFirstPartHash) 
			{
				this.alreadySized = true;
				this.animateSecondPart();
			}
		}
	}

	#region Script
	void Awake () 
	{
		this.alreadySized = false;

		this.anim = this.GetComponent<Animator> ();
		this.stateFirstPartHash = Animator.StringToHash("AnimFirstPart");
		this.allowToFirstHash = Animator.StringToHash("AllowToFirst");
		this.allowToSecondHash = Animator.StringToHash("AllowToSecond");

		this.anim.SetBool (this.allowToFirstHash, true);
	}

	void Update () 
	{
		this.animateFirstPart ();
	}
	#endregion
}
