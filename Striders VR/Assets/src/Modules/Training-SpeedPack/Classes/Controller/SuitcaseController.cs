using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StridersVR.Domain.SpeedPack;
using StridersVR.Modules.SpeedPack.Logic.Representatives;

public class SuitcaseController : MonoBehaviour {

	public ScriptableObject suitcasePartData;
	public ScriptableObject itemData;

	public GameObject scoreContainer;

	private GameObject currentPartAnimating;
	private GameObject verifier;

	private bool allowStartAnimation = false;
	private bool partSelected = false;
	private bool allowToCreate = false;
	private bool gameStarted = false;

	private int currentPartIndex;

	private string gameDificulty;

	private Suitcase currentSuitcase;

	private Spot playerSpot;

	private RepresentativeSuitcase suitcaseLogic;

	private void instatiateVerifier(bool isCorrect)
	{
		GameObject _verifierPrefab = Resources.Load("Prefabs/Training-SpeedPack/Verifier", typeof(GameObject)) as GameObject;
		Vector3 _position = this.playerSpot.SpotPosition;

		_position.y = 0.3f;
		this.verifier = (GameObject)GameObject.Instantiate (_verifierPrefab, Vector3.zero, _verifierPrefab.transform.rotation);
		this.verifier.transform.parent = this.transform.GetChild(0).Find ("SuitcasePart").Find ("Items");
		this.verifier.transform.localPosition = _position;
		this.verifier.GetComponent<VerifierController> ().setAnimation (isCorrect);
	}

	private void animateParts()
	{
		if (!this.partSelected) 
		{
			this.currentPartAnimating = this.transform.GetChild(this.currentPartIndex).gameObject;
			this.partSelected = true;
			this.currentPartAnimating.GetComponent<SuitcasePartController>().allowAnimation();
		}

		if (this.currentPartAnimating != null && this.currentPartAnimating.GetComponent<SuitcasePartController>().IsAnimationDone) 
		{
			this.currentPartIndex --;
			this.partSelected = false;
			if(this.currentPartIndex < 1)
			{
				this.allowStartAnimation = false;
				this.allowToCreate = true;

				if(this.playerSpot.IsAvailableSpot)
				{
					this.instatiateVerifier(true);
					this.scoreContainer.GetComponent<ScorePackController>().setScore(true, this.currentSuitcase.SuitcaseScore);
				}
				else
				{
					this.instatiateVerifier(false);
					this.scoreContainer.GetComponent<ScorePackController>().setScore(false, 0);
				}

				this.currentSuitcase = null;
			}

			if(this.currentPartAnimating.GetComponent<SuitcasePartController>().LocalPart.AttachedPart.IsMainPart)
				this.currentPartAnimating.GetComponent<SuitcasePartController>().reflectItems(this.transform.GetChild(0).gameObject);
			else
				this.currentPartAnimating.GetComponent<SuitcasePartController>().reflectItems(this.transform.GetChild(this.currentPartIndex).gameObject);

			this.transform.GetChild(this.currentPartIndex + 1).gameObject.SetActive(false);
		}
	}

	private void createParts()
	{
		this.currentSuitcase = this.suitcaseLogic.getSuitcase ();
		this.suitcaseLogic.spawnItems (this.currentSuitcase);
		this.suitcaseLogic.spawnPlayerItem ();
	}

	private IEnumerator resetTableboard()
	{
		yield return new WaitForSeconds(1.5f);
		foreach(Transform child in this.transform)
		{
			GameObject.Destroy(child.gameObject);
		}

		this.createParts ();
	}
	
	public void placePlayerItem(Spot currentSpot)
	{
		GameObject _draggableItem = GameObject.FindGameObjectWithTag ("DraggableItem");

		_draggableItem.GetComponent<ItemDraggableController> ().stopDragging ();
		_draggableItem.GetComponent<BoxCollider> ().enabled = false;
		_draggableItem.transform.parent = this.transform.GetChild(0).Find ("SuitcasePart").Find ("Items");
		_draggableItem.transform.localPosition = currentSpot.SpotPosition;

		this.currentPartIndex = this.transform.childCount - 1;
		this.allowStartAnimation = true;
		this.playerSpot = currentSpot;		
	}


	#region Script
	void Awake () 
	{
		this.gameDificulty = GameObject.FindGameObjectWithTag ("StaticUser").GetComponent<StaticUserController> ().Training.Difficulty;
		this.suitcaseLogic = new RepresentativeSuitcase (this.gameObject, this.gameDificulty);
		this.suitcaseLogic.SetPartData = this.suitcasePartData;
		this.suitcaseLogic.SetItemData = this.itemData;
	}

	void Update()
	{
		if (!this.gameStarted) 
		{
			if(this.scoreContainer.GetComponent<ScorePackController>().IsGameBegin)
			{
				this.gameStarted = true;
				this.createParts();
			}
		}

		if (this.allowStartAnimation) 
		{
			this.animateParts ();
		} 
		else if (this.allowToCreate && !this.scoreContainer.GetComponent<ScorePackController>().IsGameTimerEnd) 
		{
			this.allowToCreate = false;
			StartCoroutine(this.resetTableboard());
		}
	}
	#endregion
}
