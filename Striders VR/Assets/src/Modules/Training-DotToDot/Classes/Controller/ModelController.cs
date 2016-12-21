using System.Collections;
using UnityEngine;
using StridersVR.Modules.DotToDot.Logic.Representatives;

public class ModelController : MonoBehaviour 
{
	public GameObject modelVisualEnding;


	private RepresentativeModelFigure modelFigure;

	private string gameDificulty;

	private GameObject pointManager;


	public void revealOriginalModel()
	{
		this.modelVisualEnding.SetActive(true);
		StartCoroutine(this.revealingModel());
	}

	public void clearCurrentModel()
	{
		this.modelFigure.clearContainer(this.gameObject);
		this.modelFigure.clearContainer(this.modelVisualEnding);
		this.modelCreation();
	}

	private IEnumerator revealingModel()
	{
		yield return new WaitForSeconds(1.5f);
		this.pointManager.GetComponent<PointManagerController> ().finishModel();
	}

	private void modelCreation()
	{
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
		this.modelFigure = new RepresentativeModelFigure (this.gameObject, this.gameDificulty);
		this.modelCreation ();
	}

	void Update()
	{
	
	}
	#endregion
}
