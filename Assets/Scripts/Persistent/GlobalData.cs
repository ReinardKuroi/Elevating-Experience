using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Shit to do:
//Remake button generator and apply to login screen and level selection: DONE
//remake loader so it actually works: DONE
//make a game engine
//make loader fancier
//add a delete player button to settings?

public class GlobalData : MonoBehaviour {

	public static GlobalData Instance { get; private set; }

	private List<LevelData> allLevelData;
	private List<AchievementData> allAchievementData;
	public List<PlayerData> allPlayerData;

	private int activePlayer;

	public static string levelDataFilename = "level.data";
	public static string achievementDataFilename = "achievement.data";
	public static string playerDataFilename = "player.data";

	public static string exposedMusicVolume = "MusicVolumeControl";
	public static string exposedSFXVolume = "SFXVolumeControl";

	void Awake () {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
		LoadGameData ();
	}

	//Active level get;set and unlock

	//Returns a new LevelData from a list of all LevelData by index
	//int index is got from all PlayerData's activeLevel using index activeplayer
	public LevelData GetActiveLevelData () {
		return allLevelData [allPlayerData[activePlayer].activeLevel];
	}

	//Sets int activeLevel in list of PlayerData by index activePlayer
	public void SetActiveLevel (int index) {
		allPlayerData [activePlayer].activeLevel = index;
		SaveLoad.SaveFile (ref allPlayerData, playerDataFilename);
	}

	//Sets bool in list unlockedLevels by index
	//in a list of all PlayerData by index activePlayer
	public void UnlockLevel (int index) {
		allPlayerData [activePlayer].unlockedLevels [index].isUnlocked = true;
		SaveLoad.SaveFile (ref allPlayerData, playerDataFilename);
	}

	public void UnlockLevel (string name) {
		UnlockedLevel uLevel = allPlayerData [activePlayer].unlockedLevels.Find (item => item.name == name);
		uLevel.isUnlocked = true;
		SaveLoad.SaveFile (ref allPlayerData, playerDataFilename);
	}

	//Active player get;set and save

	//Returns a new PlayerData from a list of all PlayerData by index
	//int index is activePlayer
	public PlayerData GetActivePlayerData () {
		if (activePlayer == -1)
			return new PlayerData ();
		else
			return allPlayerData [activePlayer];
	}

	//Sets PlayerData in a list of all PlayerData by index to playerData
	//int index is activePlayer
	public void SetActivePlayerData (PlayerData playerData) {
		allPlayerData [activePlayer] = playerData;
		SaveLoad.SaveFile (ref allPlayerData, playerDataFilename);
	}

	//NewPlayer

	public void CreateNewPlayerData (string playerName) {
		PlayerData playerData = new PlayerData ();
		for (int i = 0; i < allLevelData.Count; i++) {
			LevelData levelData = allLevelData [i];
			playerData.unlockedLevels.Add (new UnlockedLevel (levelData) {
				index = i,
				isUnlocked = false
			});
			playerData.highscores.Add (new Highscore (levelData.levelName));
		}
		playerData.name = playerName;
		playerData.isActive = true;
		allPlayerData.Insert (0, playerData);
		SetLastActivePlayer (0);
		UnlockLevel ("Default");
		UnlockLevel ("Foo");
	}

	//LastActivePLayer

	//Returns int index after searching a list of all PlayerdData
	//index is -1 if no active PlayerData.isActive found
	//or index of PlayerData in a list
	public int GetLastActivePlayer () {
		if (allPlayerData.Count != 0) {
			foreach (PlayerData playerData in allPlayerData) {
				if (playerData.isActive)
					return allPlayerData.IndexOf (playerData);
			}
		}
		return -1;
	}

	//Sets PlayerData.isActive to true in a list of all PlayerData by index
	//after resetting every PlayerData.isActive in list to false
	public void SetLastActivePlayer (int index) {
		ResetLastActivePlayer ();
		activePlayer = index;
		allPlayerData [index].isActive = true;
		SaveLoad.SaveFile (ref allPlayerData, playerDataFilename);
	}

	//Sets every PlayerData.isActive in a list of all PlayerData to false
	public void ResetLastActivePlayer () {
		if (allPlayerData.Count != 0) {
			foreach (PlayerData playerData in allPlayerData) {
				playerData.isActive = false;
			}
		}
	}

	//SaveLoad

	public void ResetPlayerData () {
		allPlayerData.Clear ();
		SaveLoad.SaveFile (ref allPlayerData, playerDataFilename);
	}

	public void LoadGameData () {
		SaveLoad.LoadFile (ref allLevelData, levelDataFilename);
		SaveLoad.LoadFile (ref allAchievementData, achievementDataFilename);
		SaveLoad.LoadFile (ref allPlayerData, playerDataFilename);
	}

	public void SaveGameData () {
		SaveLoad.SaveFile (ref allLevelData, levelDataFilename);
		SaveLoad.SaveFile (ref allAchievementData, achievementDataFilename);
		SaveLoad.SaveFile (ref allPlayerData, playerDataFilename);
	}
}

[System.Serializable]
public class PlayerData {
	public string name;
	public bool isActive;
	public int activeLevel;
	public List<UnlockedLevel> unlockedLevels;
	public List<Highscore> highscores;
	public List<AudioSettings> audioSettings;

	public PlayerData () {
		this.name = "";
		this.isActive = false;
		this.activeLevel = 0;
		this.unlockedLevels = new List<UnlockedLevel> ();
		this.highscores = new List<Highscore> ();
		this.audioSettings = new List<AudioSettings> ();

		this.audioSettings.Add (new AudioSettings (GlobalData.exposedMusicVolume));
		this.audioSettings.Add (new AudioSettings (GlobalData.exposedSFXVolume));
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

	public LevelData () {
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

[System.Serializable]
public class AudioSettings {
	public string name;
	public float volume;
	public bool enabled;

	public AudioSettings (string name) {
		this.name = name;
		this.volume = 0;
		this.enabled = true;
	}
}

[System.Serializable]
public class UnlockedLevel {
	public string name;
	public string showName;
	public bool isUnlocked;
	public int index;

	public UnlockedLevel (LevelData levelData) {
		this.name = levelData.levelName;
		this.showName = levelData.levelShowName;
		this.isUnlocked = false;
		this.index = 0;
	}
}

[System.Serializable]
public class Highscore {
	public string levelName;
	public int highscore;

	public Highscore (string name) {
		this.levelName = name;
		this.highscore = 0;
	}
}