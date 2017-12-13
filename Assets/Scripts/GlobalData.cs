using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class GlobalData : MonoBehaviour {

	public static GlobalData Instance { get; private set; }
	private LevelData[] allLevelData;
	private string levelDataFilename = "data.json";

	void Awake () {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
		LoadGameData ();
	}

	public LevelData LoadGameData () {
		string filePath = Path.Combine (Application.streamingAssetsPath, levelDataFilename);

		if (File.Exists (filePath)) {
			string jsonData = File.ReadAllText (filePath);
			GameData loadedData = JsonUtility.FromJson<GameData> (jsonData);
			allLevelData = loadedData.allLevelData;
		} else {
			Debug.LogError ("Cannot load game data!");
		}
	}
}

public class GameData {
	public LevelData[] allLevelData;
}

public class LevelData {
	//general
	public string levelName;
	public string transitionSpeed;
	//button
	public int multiplierLimit;
	public int multiplierDynamic;
	public int clickDecay;
	public int clickWeight;
	public int critChance;
}