using UnityEngine;
using System.Collections;
using StridersVR.Modules.TrainOfThought.Logic.Representatives;
using StridersVR.ScriptableObjects.TrainOfThought;

public class PlatformController : MonoBehaviour {

	public ScriptableObjectColorStation gameColorStationsData;
	public ScriptableObjectCurveDirection gameCurvesDirectionData;
	public ScriptableObjectRailroadSwitch gameRailroadSwitchData;
	public ScriptableObjectColorTrain gameColorTrainsData;

	private string gameDifficulty = "";
	private RepresentativeTrainPlatform trainPlatform;
	private float timeToBegin = 0;
	private float timeToSpawnTrain = 0;

	#region script
	void Awake () 
	{
		this.gameDifficulty = "Easy";
		this.trainPlatform = new RepresentativeTrainPlatform (this.gameDifficulty, this.transform.gameObject);
		this.trainPlatform.instantiateObjects();
		this.trainPlatform.createTrainGenerationStrategies ();
		this.timeToSpawnTrain = this.trainPlatform.SpawnTrainTimer;
//		this.trainPlatform.setTrainGenerationStrategy ();
//		this.trainPlatform.instantiateTrain ();

	}

	void Update () 
	{
		if (this.timeToBegin < 4) 
		{
			this.timeToBegin += Time.deltaTime;
		} 
		else 
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
//		if(this.contextTrainsFunctions.AllowStartTrain)
//			StartCoroutine (respawnTimer ());

	}

//	IEnumerator respawnTimer()
//	{
//		this.contextTrainsFunctions.selectTrain ();
//		yield return new WaitForSeconds (3.0f);
//		this.contextTrainsFunctions.startTrain ();
//	}
	#endregion
}
