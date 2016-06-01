using UnityEngine;
using System.Collections;

public class VerifierController : MonoBehaviour {

	private Animator verifierAnimator;

	private int animCorrectHash;
	private int animIncorrectHash;
	private int animHashName = 0;

	private bool animationDone = false;
	private bool startAnimation = false;

	public void setAnimation(bool isCorrect)
	{
		if (isCorrect) 
		{
			this.animHashName = Animator.StringToHash("AnimVerifyCorrect");
			this.verifierAnimator.SetBool (this.animCorrectHash, true);
		}
		else
		{
			this.animHashName = Animator.StringToHash("AnimVerifyIncorrect");
			this.verifierAnimator.SetBool (this.animIncorrectHash, true);
		}

		this.startAnimation = true;
	}


	#region Script
	void Awake () 
	{
		this.verifierAnimator = this.GetComponent<Animator> ();
		this.animCorrectHash = Animator.StringToHash("IsCorrect");
		this.animIncorrectHash = Animator.StringToHash("IsIncorrect");
	}

	void Update () 
	{
		if (this.startAnimation) 
		{
			if(this.verifierAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash == this.animHashName
			   && this.verifierAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 
			   && !this.verifierAnimator.IsInTransition(0))
			{
				this.animationDone = true;
				this.startAnimation = false;
			}
		}
	}
	#endregion

	#region Properties
	public bool IsVerificationDone
	{
		get { return this.animationDone; }
	}
	#endregion
}
