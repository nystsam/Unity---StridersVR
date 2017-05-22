using UnityEngine;
using System.Collections;
using StridersVR.Domain;

public class MenuResultController : MonoBehaviour {

	public TextMesh TrainingName;
	public TextMesh Date;
	public TextMesh Difficulty;
	public TextMesh Total;
	public TextMesh Hits;
	public TextMesh Errors;
	public TextMesh AverageTime;
	public TextMesh ExtraCriterion;

	public Transform ActivitiesContainer;

	private Statistic currentStatistic;


	public void SetStatistic(Statistic newStatistic)
	{
		this.currentStatistic = newStatistic;
	}

	public void SetData()
	{
		if(this.currentStatistic != null)
		{
			string _difficulty;
			float _yPos = 0.5f;

			this.AverageTime.transform.parent.localPosition = new Vector3(-1.4f, -0.4f, 0);
			this.ExtraCriterion.transform.parent.gameObject.SetActive(false);

			if(this.currentStatistic.Difficulty.Equals("Easy"))
				_difficulty = "Fácil";
			else if(this.currentStatistic.Difficulty.Equals("Medium"))
				_difficulty = "Normal";
			else
				_difficulty = "Avanzado";

			this.Date.text = this.currentStatistic.CurrentDate;
			this.Difficulty.text = _difficulty;
			this.Total.text = this.currentStatistic.GetTotal().ToString();
			this.Hits.text = this.currentStatistic.Hits.ToString();
			this.Errors.text = this.currentStatistic.Errors.ToString();

			this.clearChilds();

			foreach(Criterion c in this.currentStatistic.CriterionList)
			{
				if(c.IsLevel)
				{
					GameObject _levelIndicator = Resources.Load("Prefabs/Menu/ActivityWithBar", typeof(GameObject)) as GameObject;
					GameObject _clone;

					_clone = (GameObject)GameObject.Instantiate(_levelIndicator);
					_clone.transform.parent = this.ActivitiesContainer;
					_clone.transform.localPosition = new Vector3(0,_yPos, 0);
					_clone.transform.localScale = new Vector3(0.05125f,0.05125f,1);
					_clone.GetComponent<TextMesh>().text = "Nivel de " + c.Description;
					_clone.GetComponent<UIActivityBarController>().begin(c.CriterionValue);

					_yPos -= 0.5f;
				}
				else if(c.IsScore)
				{
					this.AverageTime.transform.parent.localPosition = new Vector3(-1.4f, -0.6f, 0);
					this.ExtraCriterion.transform.parent.gameObject.SetActive(true);
					this.ExtraCriterion.transform.parent.GetComponent<TextMesh>().text = "Punctuación";
					this.ExtraCriterion.text = c.CriterionValue.ToString("F2");
				}
				else if(c.IsAttempt)
				{
					this.AverageTime.transform.parent.localPosition = new Vector3(-1.4f, -0.6f, 0);
					this.ExtraCriterion.transform.parent.gameObject.SetActive(true);
					this.ExtraCriterion.transform.parent.GetComponent<TextMesh>().text = "Revelaciones";
					this.ExtraCriterion.text = c.CriterionValue.ToString("F2");
				}
				else
				{
					this.AverageTime.text = c.CriterionValue.ToString("F2") + " seg.";
				}
			}
		}
	}

	private void clearChilds()
	{
		foreach(Transform child in this.ActivitiesContainer.transform)
		{
			GameObject.Destroy(child.gameObject);
		}
	}

	#region Script
	#endregion
}
