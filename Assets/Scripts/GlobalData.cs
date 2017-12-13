using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class GlobalData : MonoBehaviour {

	public Dictionary<string, int> dictS;

	public static GlobalData Instance { get; private set; }
	public int score = 0, highscore = 0;

	private LevelData[] allLevelData;
	private AchievementData[] allAchievementData;
	private Highscore[] allHighscoreData;
	private PlayerSettings playerSettings;
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
		
	public void LoadGameData () {
		string filePath = Path.Combine (Application.streamingAssetsPath, levelDataFilename);

		if (File.Exists (filePath)) {
			BinaryFormatter bFormatter = new BinaryFormatter ();
			FileStream fileStream = File.Open (filePath, FileMode.Open);
			GameData loadedData = (GameData)bFormatter.Deserialize (fileStream);
			allLevelData = loadedData.allLevelData;
			allAchievementData = loadedData.allAchievementData;
			allHighscoreData = loadedData.allHighscoreData;
			playerSettings = loadedData.playerSettings;
			fileStream.Close ();
		} else {
			Debug.LogError ("Cannot load game data!");
		}
	}

	public void SaveGameData () {
		string filePath = Path.Combine (Application.streamingAssetsPath, levelDataFilename);


	}
}

[System.Serializable]
public class GameData {
	public LevelData[] allLevelData;
	public AchievementData[] allAchievementData;
	public Highscore[] allHighscoreData;
	public PlayerSettings playerSettings;
}

[System.Serializable]
public class Highscore {
	public string level;
	public int highscore;
}

[System.Serializable]
public class PlayerSettings {
	public float volume;
	public string selectedLevel;
	public string selectedMode;
}

[System.Serializable]
public class AchievementData {
	public string achievementName;
	public string levelRestriction;
	public string triggerName;
	public int triggerValue;
}

[System.Serializable]
public class LevelData {
	public string levelName;
	public int transitionSpeed;

	public int multiplierLimit;
	public int multiplierDynamic;
	public int clickDecay;
	public int clickWeight;
	public int critChance;
}