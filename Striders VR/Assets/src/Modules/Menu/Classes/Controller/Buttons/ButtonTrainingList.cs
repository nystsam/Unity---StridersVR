using UnityEngine;
using System.Collections.Generic;
using StridersVR.Modules.Menu.Logic;
using StridersVR.Domain;

namespace StridersVR.Buttons
{
	public class ButtonTrainingList : MenuButton
	{
		public static ButtonTrainingList Current;

		[SerializeField] private TextMesh trainingName;
		[SerializeField] private MeshRenderer trainingImage;

		private Vector3 targetPosition;

		private MenuTrainingList trainingList;

		private Training currentTrain;
		public Training CurrentTrain
		{
			get { return this.currentTrain; }
		}

		public ButtonTrainingList()
		{
			Current = this;
		}

		public void NextTraining()
		{
			this.currentTrain = this.trainingList.getNextTraining ();
			this.setValues();
		}

		public void PreviousTraining()
		{
			this.currentTrain = this.trainingList.getPreviousTraining ();
			this.setValues();
		}

		protected override void MenuButtonAction ()
		{
			this.CurrentMenu.SetActive(false);
			this.CurrentMenu.transform.localPosition = this.targetPosition;
			this.TargetMenu.transform.localPosition = Vector3.zero;
			this.TargetMenu.SetActive(true);
			GameObject.FindGameObjectWithTag ("StaticUser").GetComponent<StaticUserController> ().setTraining (this.currentTrain);
		}

		private void setValues()
		{
			this.trainingName.text = this.currentTrain.Name;
			this.trainingImage.material = Resources.Load(this.currentTrain.Image, typeof(Material)) as Material;
		}

		#region Script
		void Start()
		{
			this.trainingList = new MenuTrainingList ();
			this.currentTrain = this.trainingList.getCurrentTraining ();
			this.setValues();

			this.targetPosition = this.TargetMenu.transform.localPosition;
			this.TargetMenu.SetActive(false);
		}
		#endregion
	}
}

