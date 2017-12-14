using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEditor;
using UnityEngine.Serialization;

public class GameDataEditor: EditorWindow {

	private string levelDataFilename = GlobalData.levelDataFilename;
	private string achievementDataFilename = GlobalData.achievementDataFilename;
	private string highscoreDataFilename = GlobalData.highscoreDataFilename;
	private string playerDataFilename = GlobalData.playerDataFilename;
	private List<string> allDataList = { "allLevelData", "allAchievementData", "allHighscoreData", "allPlayerData" };

	public List<LevelData> allLevelData;
	public List<AchievementData> allAchievementData;
	public List<HighscoreData> allHighscoreData;
	public List<PlayerData> allPlayerData;

	[MenuItem ("Window/GameObject Data Editor")]
	public static void ShowWindow () {
		EditorWindow.GetWindow<GameDataEditor> ();
	}

	void OnGUI () {
		if ((allLevelData != null) && (allAchievementData != null) && (allHighscoreData != null) && (allPlayerData != null)) {
			Debug.Log ("Serializing...");
			foreach (string name in allDataList) {
				ApplyField (name);
			}
			if (GUILayout.Button ("Save Data")) {
				SaveGameData ();
			} 
		}
		if (GUILayout.Button ("Load Data")) {
			LoadGameData ();
		} 
	} 

	static void ApplyField (string name) {
		SerializedObject serializedObject = new SerializedObject (this);
		SerializedProperty serializedProperty  = serializedObject.FindProperty (name);
		EditorGUILayout.PropertyField (serializedProperty, true);
		serializedObject.ApplyModifiedProperties ();
	}

	static void LoadFile<T> (ref T obj, string fileName) where T : new () {
		string filePath = Path.Combine (Application.streamingAssetsPath, fileName);
		if (File.Exists (filePath)) {
			BinaryFormatter bFormatter = new BinaryFormatter ();
			FileStream fileStream = File.Open (filePath, FileMode.Open);
			obj = (T)bFormatter.Deserialize (fileStream);
			fileStream.Close ();
			Debug.Log ("Loaded file " + fileName);
		} else {
			obj = new T ();
			Debug.Log ("Created file");
		}
	}

	static void SaveFile<T> (ref T obj, string fileName) where T : class {
		string filePath = Path.Combine (Application.streamingAssetsPath, fileName);
		BinaryFormatter bFormatter = new BinaryFormatter ();
		FileStream fileStream = File.Create (filePath);
		bFormatter.Serialize (fileStream, obj);
		fileStream.Close ();
	}

	private void LoadGameData () {
		LoadFile (ref allLevelData, levelDataFilename);
		LoadFile (ref allAchievementData, achievementDataFilename);
		LoadFile (ref allHighscoreData, highscoreDataFilename);
		LoadFile (ref allPlayerData, playerDataFilename);
	}

	private void SaveGameData () {
		SaveFile (ref allLevelData, levelDataFilename);
		SaveFile (ref allAchievementData, achievementDataFilename);
		SaveFile (ref allHighscoreData, highscoreDataFilename);
		SaveFile (ref allPlayerData, playerDataFilename);
	}
}
