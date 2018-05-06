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
	private string playerDataFilename = GlobalData.playerDataFilename;
	private List<string> allDataList = new List<string> { "allLevelData", "allAchievementData", "allPlayerData" };

	public List<LevelData> allLevelData;
	public List<AchievementData> allAchievementData;
	public List<PlayerData> allPlayerData;

	[MenuItem ("Window/GameObject Data Editor")]
	public static void ShowWindow () {
		EditorWindow.GetWindow<GameDataEditor> ();
	}

	void OnGUI () {
		if ((allLevelData != null) && (allAchievementData != null)  && (allPlayerData != null)) {
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

	private void ApplyField (string name) {
		SerializedObject serializedObject = new SerializedObject (this);
		SerializedProperty serializedProperty  = serializedObject.FindProperty (name);
		EditorGUILayout.PropertyField (serializedProperty, true);
		serializedObject.ApplyModifiedProperties ();
	}

	private void LoadGameData () {
		SaveLoad.LoadFromAssets (ref allLevelData, levelDataFilename);
		SaveLoad.LoadFromAssets (ref allAchievementData, achievementDataFilename);
		SaveLoad.LoadFromPersistent (ref allPlayerData, playerDataFilename);
	}

	private void SaveGameData () {
		SaveLoad.SaveToAssets (ref allLevelData, levelDataFilename);
		SaveLoad.SaveToAssets (ref allAchievementData, achievementDataFilename);
		SaveLoad.SaveToPersistent (ref allPlayerData, playerDataFilename);
	}
}
