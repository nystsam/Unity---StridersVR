using UnityEngine;
using System.Collections;
using StridersVR.Domain.Menu;

public class MenuContainerController : MonoBehaviour {

	public GameObject menuMain;
	public GameObject menuTraining;
	public GameObject menuStatistics;

	private bool animationTransition;
	private bool isMainMenu;

	private Animator containerAnimator;

	private int hashVariable;
	private int hashName;

	public void changeMenu(string animName, string variableName)
	{
		if (!this.animationTransition) 
		{
			if (this.isMainMenu) 
			{
				this.hashName = Animator.StringToHash (animName);
				this.hashVariable = Animator.StringToHash (variableName);

				this.containerAnimator.SetBool (this.hashVariable, true);

				this.isMainMenu = false;
			}
			else if(!this.isMainMenu)
			{
				this.hashName = Animator.StringToHash ("AnimMainMenu");
				this.containerAnimator.SetBool (this.hashVariable, false);
				this.isMainMenu = true;
			}

			this.animationTransition = true;
		}
	}

	private void transitionController()
	{
		if (this.animationTransition) 
		{
			if(this.containerAnimator.GetCurrentAnimatorStateInfo(0).shortNameHash == this.hashName &&
			   this.containerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 &&
			   !this.containerAnimator.IsInTransition(0))
			{
				this.animationTransition = false;
				Debug.Log ("Termino");

			}

		}
	}

	#region Script
	void Awake () 
	{
		this.isMainMenu = true;
		this.animationTransition = false;

		this.containerAnimator = this.GetComponent<Animator> ();
		
	}

	void Update () 
	{
		this.transitionController ();
	}
	#endregion
}
