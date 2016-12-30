using System.Collections.Generic;
using UnityEngine;
using StridersVR.Domain.DotToDot;
using StridersVR.Modules.DotToDot.Logic.Contexts;
using StridersVR.Modules.DotToDot.Logic.Strategies;

namespace StridersVR.Modules.DotToDot.Logic.Representatives
{
	public class RepresentativeModelFigure
	{
		private ContextCreateModel contextCreateModel;

		private GameObject modelContainer;

		private Model currentModel;

		private StrategyCreateModelComposite strategyComposite;
		
		public RepresentativeModelFigure(GameObject modelContainer, string difficulty)
		{
			this.contextCreateModel = new ContextCreateModel ();
			this.modelContainer = modelContainer;
			this.createStrategies (difficulty);

		}

		public void createModel()
		{
			this.strategyComposite.StrategyIndex = Random.Range (0, this.strategyComposite.StrategyCount);

			this.currentModel = this.contextCreateModel.createModel ();
		}

		public void clearContainer(GameObject container)
		{
			foreach(Transform child in container.transform)
			{
				GameObject.Destroy(child.gameObject);
			}
		}

		public void replicateModel(GameObject container)
		{
			GameObject _clone;
			Material _newMaterial = Resources.Load("Materials/Training-DotToDot/MatEndiPointVisual", typeof(Material)) as Material;


			foreach(Transform child in this.modelContainer.transform)
			{
				_clone = (GameObject)GameObject.Instantiate(child.gameObject, Vector3.zero, Quaternion.Euler(Vector3.zero));
				_clone.transform.parent = container.transform;
				_clone.transform.localPosition = child.localPosition;

				if(_clone.transform.childCount > 0)
				{
					if(_clone.transform.GetChild(0).GetComponent<StripeController>() != null)
					{
						_clone.transform.localRotation = child.localRotation;
						_clone.transform.localScale = child.localScale;
						_clone.transform.GetChild(0).GetComponent<StripeController>().setUndraggable();
					}
				}
				else
				{
					_clone.transform.localScale = new Vector3(0.3f,0.3f,0.3f);
					_clone.GetComponent<MeshRenderer>().material = _newMaterial;
				}
			}
		}

		private void createStrategies(string difficulty)
		{
			this.strategyComposite = new StrategyCreateModelComposite ();

			if(difficulty.Equals("Easy"))
			{
				this.strategyComposite.addStrategy(new StrategyCreateModelEasy(this.modelContainer));
			}
			else if(difficulty.Equals("Medium"))
			{
				this.strategyComposite.addStrategy(new StrategyCreateModelMedium(this.modelContainer));
			}

			this.contextCreateModel.StrategyCreateModel = this.strategyComposite;
		}

		#region Properties
		public Model CurrentModel
		{
			get { return this.currentModel; }
		}
		#endregion	
	}
}