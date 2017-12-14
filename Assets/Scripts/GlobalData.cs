using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class GlobalData : MonoBehaviour {

	public Dictionary<string, int> dictS;

	public static GlobalData Instance { get; private set; }
	public int score = 0, highscore = 0;

	private List<LevelData> allLevelData;
	private List<AchievementData> allAchievementData;
	private List<HighscoreData> allHighscoreData;
	private PlayerData playerData;

	public static string levelDataFilename = "level.data";
	public static string achievementDataFilename = "achievement.data";
	public static string highscoreDataFilename = "highscore.data";
	public static string playerDataFilename = "player.data";

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
		
		string filePath;

		filePath = Path.Combine (Application.streamingAssetsPath, levelDataFilename);
		if (File.Exists (filePath)) {
			BinaryFormatter bFormatter = new BinaryFormatter ();
			FileStream fileStream = File.Open (filePath, FileMode.Open);
			allLevelData = (List<LevelData>)bFormatter.Deserialize (fileStream);
			fileStream.Close ();
		}
		filePath = Path.Combine (Application.streamingAssetsPath, achievementDataFilename);
		if (File.Exists (filePath)) {
			BinaryFormatter bFormatter = new BinaryFormatter ();
			FileStream fileStream = File.Open (filePath, FileMode.Open);
			allAchievementData = (List<AchievementData>)bFormatter.Deserialize (fileStream);
			fileStream.Close ();
		}
		filePath = Path.Combine (Application.streamingAssetsPath, highscoreDataFilename);
		if (File.Exists (filePath)) {
			BinaryFormatter bFormatter = new BinaryFormatter ();
			FileStream fileStream = File.Open (filePath, FileMode.Open);
			allHighscoreData = (List<HighscoreData>)bFormatter.Deserialize (fileStream);
			fileStream.Close ();
		}
		filePath = Path.Combine (Application.streamingAssetsPath, playerDataFilename);
		if (File.Exists (filePath)) {
			BinaryFormatter bFormatter = new BinaryFormatter ();
			FileStream fileStream = File.Open (filePath, FileMode.Open);
			playerData = (PlayerData)bFormatter.Deserialize (fileStream);
			fileStream.Close ();
		}
	}
}

[System.Serializable]
public class HighscoreData {
	public string level;
	public int highscore;

	public HighscoreData () {
		this.level = "";
		this.highscore = 0;
	}
}

[System.Serializable]
public class PlayerData {
	public string name;
	public float volume;
	public string selectedLevel;
	public string selectedMode;

	public PlayerData () {
		this.name = "";
		this.volume = 1;
		this.selectedLevel = "";
		this.selectedMode = "";
	}
}

[System.Serializable]
public class AchievementData {
	public string achievementName;
	public string levelRestriction;
	public string triggerName;
	public int triggerValue;

	AchievementData () {
		this.achievementName = "";
		this.levelRestriction = "";
		this.triggerName = "";
		this.triggerValue = 0;
	}
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

	public LevelData (){
		this.levelName = "";
		this.transitionSpeed = 0;
		this.multiplierLimit = 0;
		this.multiplierDynamic = 0;
		this.clickDecay = 0;
		this.clickWeight = 0;
		this.critChance = 0;
	}
}