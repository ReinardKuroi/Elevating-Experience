using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace test {

	public class GlobalData : MonoBehaviour {

		public static string levelDataFilename = "testlevel.data";
		public static string playerDataFilename = "testplayer.data";

		public static GlobalData Instance { get; private set; }
		void Awake () {
			if (Instance == null) {
				Instance = this;
				DontDestroyOnLoad (gameObject);
			} else {
				Destroy (gameObject);
			}
		}

		private Dictionary<string, LevelData> allLevelData = new Dictionary<string, LevelData> ();
		private Dictionary<string, PlayerData> allPlayerData = new Dictionary<string, PlayerData> ();

		private string activePlayer;



		//classes

		[System.Serializable]
		public class PlayerData {
			public string name;
			public bool lastActive;

			public float musicVolume;
			public bool musicEnabled;
			public float sfxVolume;
			public bool sfxEnabled;

			public string selectedLevel;
			public Dictionary<string, bool> unlockedLevels;

			public PlayerData () {
				this.name = "";
				this.lastActive = true;
				this.musicVolume = 0;
				this.musicEnabled = true;
				this.sfxVolume = 0;
				this.sfxEnabled = true;
				this.selectedLevel = "";
				this.unlockedLevels = new Dictionary<string, bool> ();
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
	}
}