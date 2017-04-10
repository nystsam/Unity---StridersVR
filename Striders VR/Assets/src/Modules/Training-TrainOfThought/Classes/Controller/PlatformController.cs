using UnityEngine;
using System.Collections;
using StridersVR.Modules.TrainOfThought.Logic.Representatives;
using StridersVR.ScriptableObjects.TrainOfThought;

public class PlatformController : MonoBehaviour {

	public static PlatformController Current;

	public ScriptableObjectColorStation gameColorStationsData;
	public ScriptableObjectCurveDirection gameCurvesDirectionData;
	public ScriptableObjectRailroadSwitch gameRailroadSwitchData;
	public ScriptableObjectColorTrain gameColorTrainsData;

	private string gameDifficulty = "";
	private RepresentativeTrainPlatform trainPlatform;
	private bool allowToSpawn = false;
	private float timeToSpawnTrain = 0;


	public PlatformController()
	{
		Current = this;
	}

	public int TrainsInPlatform()
	{
		return this.transform.GetChild(2).childCount;
	}

	#region script
	void Awake () 
	{
		this.gameDifficulty = GameObject.FindGameObjectWithTag ("StaticUser").GetComponent<StaticUserController> ().Training.Difficulty;
		this.trainPlatform = new RepresentativeTrainPlatform (this.gameDifficulty, this.transform.gameObject);
		this.trainPlatform.instantiateObjects();
		this.trainPlatform.createTrainGenerationStrategies ();
		this.timeToSpawnTrain = this.trainPlatform.SpawnTrainTimer;
	}

	void Start()
	{
		SwitchesPanelController.Current.SetButtons();
	}

	void Update () 
	{
		if (this.allowToSpawn)
		{
			if (this.timeToSpawnTrain <= 0) 
			{
				this.trainPlatform.setTrainGenerationStrategy ();
				this.trainPlatform.instantiateTrain ();
				this.timeToSpawnTrain = this.trainPlatform.SpawnTrainTimer;
			}
			else 
			{
				this.timeToSpawnTrain -= Time.deltaTime;
			}
		}

	}
	#endregion

	#region Properties
	/// <summary>
	/// Sets a value indicating whether this <see cref="PlatformController"/> allow to spawn train. The Score platform's panel set
	/// this value.
	/// </summary>
	/// <value><c>true</c> if allow to spawn train; otherwise, <c>false</c>.</value>
	public bool AllowToSpawnTrain
	{
		set { this.allowToSpawn = value; }
	}
	#endregion
}
