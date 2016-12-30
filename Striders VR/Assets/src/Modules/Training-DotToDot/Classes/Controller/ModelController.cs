using System.Collections;
using UnityEngine;
using StridersVR.Modules.DotToDot.Logic.Representatives;

public class ModelController : MonoBehaviour 
{
	public GameObject modelVisualEnding;


	private RepresentativeModelFigure modelFigure;

	private string gameDificulty;

	private GameObject pointManager;
	private GameObject scoreController;

	private bool allowToCreate;
	private bool isGameOver;

	public void revealOriginalModel()
	{
		this.modelVisualEnding.SetActive(true);
		StartCoroutine(this.revealingModel());
	}

	public void clearCurrentModel()
	{
		this.modelFigure.clearContainer(this.gameObject);
		this.modelFigure.clearContainer(this.modelVisualEnding);
		this.allowToCreate = true;
	}

	private IEnumerator revealingModel()
	{
		yield return new WaitForSeconds(1.5f);
		this.pointManager.GetComponent<PointManagerController> ().finishModel();
	}

	private void gmaeOver()
	{
		this.isGameOver = true;
		this.modelFigure.clearContainer(this.gameObject);
		this.modelFigure.clearContainer(this.modelVisualEnding);
		this.pointManager.GetComponent<PointManagerController> ().gameOver();

		//Indicar algo
	}

	private void modelCreation()
	{
		this.allowToCreate = false;
		this.modelVisualEnding.SetActive(false);
		this.modelFigure.createModel ();
		this.modelFigure.replicateModel(this.modelVisualEnding);
		this.pointManager.GetComponent<PointManagerController> ().setModel (this.modelFigure.CurrentModel);
	}

	#region Script
	void Awake()
	{
		this.gameDificulty = GameObject.FindGameObjectWithTag ("StaticUser").GetComponent<StaticUserController> ().Training.Difficulty;
		this.pointManager = GameObject.FindGameObjectWithTag("GameController");
		this.scoreController = GameObject.FindGameObjectWithTag("Finish");
		this.modelFigure = new RepresentativeModelFigure (this.gameObject, this.gameDificulty);
		this.allowToCreate = true;
	}

	void Update()
	{
		if(this.allowToCreate && this.scoreController.GetComponent<ScoreDotsController>().IsGameBegin)
		{
			this.modelCreation ();
		}

		if(!this.isGameOver && this.scoreController.GetComponent<ScoreDotsController>().IsGameTimerEnd)
		{
			this.gmaeOver();
		}
	}
	#endregion
}
