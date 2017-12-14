using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Serialization;


public class GlobalData : MonoBehaviour {

	public static GlobalData Instance { get; private set; }

	public int highscore = 0;

	private List<LevelData> allLevelData;
	private List<AchievementData> allAchievementData;
	private List<HighscoreData> allHighscoreData;
	private List<PlayerData> allPlayerData;

	private Dictionary<string, int> sceneDict = new Dictionary<string, int> ();

	private LevelData activeLevel = new LevelData ();
	private HighscoreData activeHighscore = new HighscoreData ();

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
		Initialize ();
	}
		
	public void SetActivelevel (string name) {
		int i;
		if (sceneDict.TryGetValue (name, out i)) {
			activeLevel = allLevelData [i];
			Debug.Log ("Active level " + name + ", index " + i.ToString ());
		} else
			Debug.LogError ("No level in database! Loading base.");
	}

	public LevelData GetActiveLevel () {
		return activeLevel;
	}

	public void Initialize () {
		for (int i = 0; i < allLevelData.Count; i++) {
			sceneDict.Add (allLevelData [i].levelName, i);
		}
		foreach (KeyValuePair<string, int> pair in sceneDict) {
			Debug.Log ("Key = " + pair.Key + ", Value = " + pair.Value.ToString());
		}
	}

	public void LoadGameData () {
		SaveLoad.LoadFile (ref allLevelData, levelDataFilename);
		SaveLoad.LoadFile (ref allAchievementData, achievementDataFilename);
		SaveLoad.LoadFile (ref allHighscoreData, highscoreDataFilename);
		SaveLoad.LoadFile (ref allPlayerData, playerDataFilename);
	}

	private void SaveGameData () {
		SaveLoad.SaveFile (ref allLevelData, levelDataFilename);
		SaveLoad.SaveFile (ref allAchievementData, achievementDataFilename);
		SaveLoad.SaveFile (ref allHighscoreData, highscoreDataFilename);
		SaveLoad.SaveFile (ref allPlayerData, playerDataFilename);
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