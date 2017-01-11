using UnityEngine;
using System.Collections.Generic;

public class SwitchesPanelController : MonoBehaviour {

	public static SwitchesPanelController Current;

	public GameObject buttonsContainer;


	private List<GameObject> switchList = new List<GameObject>();

	[SerializeField] private bool isMoving = false;


	public SwitchesPanelController()
	{
		Current = this;
	}

	public void AddSwitch(GameObject button)
	{
		this.switchList.Add(button);
	}

	public void CreateButtons()
	{
		int _switchNumber = 1;
		float _zInit = 1, _xInit = -0.9f;
		GameObject _button = Resources.Load("Prefabs/Training-TrainOfThought/SwitchButton", typeof(GameObject)) as GameObject;

		foreach(GameObject _s in this.switchList)
		{
			GameObject _cb = (GameObject)GameObject.Instantiate(_button, Vector3.zero, Quaternion.identity);

			_cb.transform.parent = this.buttonsContainer.transform;
			_cb.transform.localPosition = new Vector3(_xInit, 0, _zInit);
			_cb.transform.localRotation = Quaternion.Euler(Vector3.zero);
			_cb.transform.GetComponentInChildren<SwitchButtonController>().setAttachedSwitch(_s, _switchNumber);
			_xInit += 0.9f;
			if(_xInit > 0.9f)
			{
				_xInit = -0.9f;
				_zInit --;
			}
			_switchNumber++;
		}
	}

	public Vector3 GetPanelPosition()
	{
		return this.transform.position;
	}

	public void SetPanelPosition(Vector3 newPosition)
	{
		this.transform.position = newPosition;
	}

	public void AllowToMove(bool val)
	{
		this.isMoving = val;
	}

	public bool IsControlMoving()
	{
		return this.isMoving;
	}

	#region Script
	void Start()
	{
		//this.gameObject.SetActive(false);
	}
	#endregion
}
