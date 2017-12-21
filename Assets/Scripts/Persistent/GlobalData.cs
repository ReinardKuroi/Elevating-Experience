﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Audio;

public class GlobalData : MonoBehaviour {

	public static GlobalData Instance { get; private set; }

	public int highscore;
	public int multiplier;
	public int score;

	public List<LevelData> allLevelData;
	public List<AchievementData> allAchievementData;
	public List<HighscoreData> allHighscoreData;
	public List<PlayerData> allPlayerData;

	public Dictionary<string, int> levelDict = new Dictionary<string, int> ();
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

	//set

	public void SetActiveLevel () {
		int i;
		if (levelDict.TryGetValue (allPlayerData [activePlayer].selectedLevel, out i)) {
			activeLevel = i;
			Debug.Log ("Active level " + allPlayerData [activePlayer].selectedLevel + ", index " + i.ToString ());
		} else {
			activeLevel = 0;
			Debug.LogError ("No level in database! Loading default.");
		}
	}

	public void SetActivePlayer (PlayerData playerData) {
		allPlayerData [activePlayer] = playerData;
		SaveLoad.SaveFile (ref allPlayerData, playerDataFilename);
	}

	//get

	public LevelData GetActiveLevel () {
		return allLevelData [activeLevel];
	}

	public PlayerData GetActivePlayer () {
		return allPlayerData [activePlayer];
	}

	//NewPlayer

	PlayerData NewPlayer (string playerName) {
		PlayerData playerData = new PlayerData ();
		playerData.unlockedLevels.Add (true);
		playerData.name = playerName;
		for (int i = 1; i < allLevelData.Count; i++) {
			playerData.unlockedLevels.Add (false);
		}
		return playerData;
	}

	//Init

	public void InitActivePlayer () {
		activePlayer = 0;
	}

	public void Initialize () {

		if (allPlayerData.Count == 0) {
			allPlayerData.Add (NewPlayer ("Player"));
		}

		for (int i = 0; i < allLevelData.Count; i++)
			if (Application.CanStreamedLevelBeLoaded (allLevelData [i].levelName))
				levelDict.Add (allLevelData [i].levelName, i);
		
		for (int i = 0; i < allPlayerData.Count; i++)
			playerDict.Add (allPlayerData [i].name, i);

		Reset ();

		int k;
		if (levelDict.TryGetValue (allPlayerData [activePlayer].selectedLevel, out k)) {
			if (!allPlayerData [activePlayer].unlockedLevels [k]) {
				Debug.Log ("Level " + allLevelData [k].levelName + " is locked, reset to default.");
				allPlayerData [activePlayer].selectedLevel = allLevelData [levelDict ["Default"]].levelName;
			}
		} else {
			Debug.Log ("Level " + allPlayerData [activePlayer].selectedLevel + " non-existent, reset to default.");
			allPlayerData [activePlayer].selectedLevel = allLevelData [levelDict ["Default"]].levelName;
		}
	}

	public void Reset () {
		InitActivePlayer ();
		SetActiveLevel ();
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

	public void SaveGameData () {
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
	public float musicVolume;
	public bool musicEnabled;
	public float sfxVolume;
	public bool sfxEnabled;

	public string selectedLevel;
	public string selectedMode;
	public List<bool> unlockedLevels;

	public PlayerData () {
		this.name = "";
		this.musicVolume = 0;
		this.musicEnabled = true;
		this.sfxVolume = 0;
		this.sfxEnabled = true;
		this.selectedLevel = "";
		this.selectedMode = "";
		this.unlockedLevels = new List<bool> ();
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
		this.multiplierLimit = 0;
		this.multiplierDynamic = 0;
		this.clickDecay = 0;
		this.clickWeight = 0;
		this.critChance = 0;
		this.critMultiplier = 0;
	}
}