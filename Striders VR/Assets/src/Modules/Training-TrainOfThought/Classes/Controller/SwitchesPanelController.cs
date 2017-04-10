using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SwitchesPanelController : MonoBehaviour {

	public static SwitchesPanelController Current;

	public GameObject buttonsContainer;
	public GameObject MediumTracks;
	public GameObject HardTracks;
	public GameObject MediumButtons;
	public GameObject HardButtons;

	[SerializeField] private List<GameObject> easyList = new List<GameObject>();
	[SerializeField] private List<GameObject> mediumList = new List<GameObject>();
	[SerializeField] private List<GameObject> hardList = new List<GameObject>();

	private List<GameObject> switchList = new List<GameObject>();

	private bool isMoving = false;

	public SwitchesPanelController()
	{
		Current = this;
	}

	public void AddSwitch(GameObject button)
	{
		this.switchList.Add(button);
	}

	public void SetButtons()
	{
		string _diff = GameObject.FindGameObjectWithTag ("StaticUser").GetComponent<StaticUserController> ().Training.Difficulty;;

		if(_diff.Equals("Easy"))
		{
			this.easySwitchs();
		}
		else if(_diff.Equals("Medium"))
		{
			this.mediumSwitchs();
		}
		else
		{
			this.hardSwitchs();
		}
		/*
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
		*/
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

	private void easySwitchs()
	{
		int _index = 0;
		foreach(GameObject _s in this.switchList)
		{
			SwitchButtonController _button = this.easyList.ElementAt(_index).GetComponentInChildren<SwitchButtonController>();
			_button.setAttachedSwitch(_s, _index + 1);
			_index ++;
		}
	}

	private void mediumSwitchs()
	{
		int _index = 0;
		foreach(GameObject _s in this.switchList)
		{
			SwitchButtonController _button = this.mediumList.ElementAt(_index).GetComponentInChildren<SwitchButtonController>();
			_button.setAttachedSwitch(_s, _index + 1);
			_index ++;
		}
	}

	private void hardSwitchs()
	{
		int _index = 0;
		foreach(GameObject _s in this.switchList)
		{
			SwitchButtonController _button = this.hardList.ElementAt(_index).GetComponentInChildren<SwitchButtonController>();
			_button.setAttachedSwitch(_s, _index + 1);
			_index ++;
		}
	}

	#region Script
	void Start()
	{

		string _diff = GameObject.FindGameObjectWithTag ("StaticUser").GetComponent<StaticUserController> ().Training.Difficulty;;
		if(_diff.Equals("Easy"))
	   	{
			this.MediumButtons.SetActive(false);
			this.HardButtons.SetActive(false);
		}
		else if(_diff.Equals("Medium"))
		{
			this.MediumTracks.SetActive(true);
			this.HardButtons.SetActive(false);
		}
		else
		{
			this.HardTracks.SetActive(true);
		}
		//this.gameObject.SetActive(false);
	}
	#endregion
}
