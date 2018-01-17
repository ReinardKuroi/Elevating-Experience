using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Shit to do:
//Remake button generator and apply to login screen and level selection: DONE
//remake loader so it actually works: DONE
//make a game engine : DONE
//add a delete player button to settings? : DONE
//add more states, e.g. Transition and Loading : No need
//Quit button for login screen? : DONE
//Make login screen fancier : DONE

//implement highscore system
//make loader fancier
//Add "delete player" button to login screen
//Start working on achievements system
//Modify level data to include music
//Modify player data to include selected music
//Add cool looking effects with wumbers on click

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

	public LevelData ActiveLevelData {
		get {
			return allLevelData [allPlayerData[activePlayer].activeLevel];
		}
	}

	public int ActiveLevelIndex {
		get {
			return allPlayerData [activePlayer].activeLevel;
		}
		set {
			allPlayerData [activePlayer].activeLevel = value;
			SaveLoad.SaveFile (ref allPlayerData, playerDataFilename);			
		}
	}

	//Sets bool in list unlockedLevels by index
	//in a list of all PlayerData by index activePlayer
	public void UnlockLevel (int index) {
		allPlayerData [activePlayer].unlockedLevels [index].isUnlocked = true;
		SaveLoad.SaveFile (ref allPlayerData, playerDataFilename);
	}

	public void UnlockLevel (string name) {
		UnlockedLevel uLevel = allPlayerData [activePlayer].unlockedLevels.Find (item => item.levelName == name);
		uLevel.isUnlocked = true;
		SaveLoad.SaveFile (ref allPlayerData, playerDataFilename);
	}

	//Active player get;set and save

	public PlayerData ActivePlayerData {
		get {
			if (activePlayer == -1)
				return new PlayerData ();
			else
				return allPlayerData [activePlayer];
		}
		set {
			allPlayerData [activePlayer] = value;
			SaveLoad.SaveFile (ref allPlayerData, playerDataFilename);
		}
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
		foreach (AchievementData data in allAchievementData) {
			playerData.unlockedAchievements.Add (new UnlockedAchievement (data.achievementName));
		}
		playerData.name = playerName;
		allPlayerData.Insert (0, playerData);
		LastActivePlayer = 0;
		UnlockLevel ("Default");
		UnlockLevel ("Foo"); // fix later
	}

	//LastActivePLayer

	public int LastActivePlayer {
		get {
			if (allPlayerData.Count != 0)
				return allPlayerData.FindIndex (item => item.isActive);
			else
				return -1;
		}
		set {
			if (allPlayerData.Count != 0) {
				foreach (PlayerData playerData in allPlayerData) {
					playerData.isActive = false;
				}
			}
			activePlayer = value;
			allPlayerData [value].isActive = true;
			SaveLoad.SaveFile (ref allPlayerData, playerDataFilename);
		}
	}

	//Hghscore

	public int Highscore {
		get {
			int index = ActivePlayerData.highscores.FindIndex (item => item.levelName == ActiveLevelData.levelName);
			int highscore = ActivePlayerData.highscores [index].highscore;
			return highscore;
		}
		set {
			int index = ActivePlayerData.highscores.FindIndex (item => item.levelName == ActiveLevelData.levelName);
			ActivePlayerData.highscores [index].highscore = value;
		}
	}

	//Achievement

	public List<AchievementData> Achievements {
		get {
			return allAchievementData;
		}
	}

	//SaveLoad

	public void ResetPlayerData () {
		allPlayerData.Clear ();
		allPlayerData = new List<PlayerData> ();
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
	public List<UnlockedAchievement> unlockedAchievements;
	public List<AudioSettings> audioSettings;

	public PlayerData () {
		this.name = "";
		this.isActive = false;
		this.activeLevel = 0;
		this.unlockedLevels = new List<UnlockedLevel> ();
		this.highscores = new List<Highscore> ();
		this.unlockedAchievements = new List<UnlockedAchievement> ();
		this.audioSettings = new List<AudioSettings> {
			new AudioSettings (GlobalData.exposedMusicVolume),
			new AudioSettings (GlobalData.exposedSFXVolume)
		};
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
	public string levelName;
	public string showName;
	public bool isUnlocked;
	public int index;

	public UnlockedLevel (LevelData levelData) {
		this.levelName = levelData.levelName;
		this.showName = levelData.levelShowName;
		this.isUnlocked = false;
		this.index = 0;
	}
}

[System.Serializable]
public class UnlockedAchievement {
	public string achievementName;
	public bool isUnlocked;

	public UnlockedAchievement (string name) {
		this.achievementName = name;
		this.isUnlocked = false;
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