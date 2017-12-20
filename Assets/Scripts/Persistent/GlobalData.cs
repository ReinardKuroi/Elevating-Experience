using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Serialization;

public class GlobalData : MonoBehaviour {

	public static GlobalData Instance { get; private set; }

	public int highscore;
	public int multiplier;
	public int score;

	public List<LevelData> allLevelData;
	public List<AchievementData> allAchievementData;
	public List<HighscoreData> allHighscoreData;
	public List<PlayerData> allPlayerData;

	public Dictionary<string, int> sceneDict = new Dictionary<string, int> ();
	public Dictionary<string, int> playerDict = new Dictionary<string, int> ();

	public int activeLevel;
	public int activePlayer;
	public string loadNext;

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
		Debug.Log ("Active player " + allPlayerData [activePlayer].name + ", active level " + allLevelData [activeLevel].levelName);
		Debug.Log ("Score: " + score + ", multi: " + multiplier + ", loadNext: " + loadNext);
	}

	public void SetActivePlayer () {
		activePlayer = 0;
	}

	public void SetActivelevel () {
		int i;
		if (sceneDict.TryGetValue (allPlayerData [activePlayer].selectedLevel, out i)) {
			activeLevel = i;
			Debug.Log ("Active level " + allPlayerData [activePlayer].selectedLevel + ", index " + i.ToString ());
		} else {
			activeLevel = 0;
			Debug.LogError ("No level in database! Loading base.");
		}
	}

	public void Initialize () {
		for (int i = 0; i < allLevelData.Count; i++)
			if (Application.CanStreamedLevelBeLoaded (allLevelData [i].levelName))
				sceneDict.Add (allLevelData [i].levelName, i);
		
		for (int i = 0; i < allPlayerData.Count; i++)
			playerDict.Add (allPlayerData [i].name, i);

		Reset ();

		int k;
		if (sceneDict.TryGetValue (allPlayerData [activePlayer].selectedLevel, out k)) {
			if (!allLevelData [k].isUnlocked) {
				Debug.Log ("Level " + allLevelData [k].levelName + " is locked, reset to default.");
				allPlayerData [activePlayer].selectedLevel = allLevelData [sceneDict ["Default"]].levelName;
			}
		} else {
			Debug.Log ("Level " + allPlayerData [activePlayer].selectedLevel + " non-existent, reset to default.");
			allPlayerData [activePlayer].selectedLevel = allLevelData [sceneDict ["Default"]].levelName;
		}
	}

	public void Reset () {
		SetActivePlayer ();
		SetActivelevel ();
		score = 0;
		highscore = 0;
		multiplier = 0;
		loadNext = allPlayerData [activePlayer].selectedLevel;
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
	public string levelShowName;
	public bool isUnlocked;

	public int multiplierLimit;
	public float multiplierDynamic;
	public float clickDecay;
	public float clickWeight;
	public int critChance;
	public int critMultiplier;

	public LevelData (){
		this.levelName = "";
		this.transitionSpeed = 0;
		this.levelShowName = "";
		this.isUnlocked = false;
		this.multiplierLimit = 0;
		this.multiplierDynamic = 0;
		this.clickDecay = 0;
		this.clickWeight = 0;
		this.critChance = 0;
		this.critMultiplier = 0;
	}
}