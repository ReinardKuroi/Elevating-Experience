using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameDataEditor: EditorWindow {

	private string levelDataFilename = GlobalData.levelDataFilename;
	private string achievementDataFilename = GlobalData.achievementDataFilename;
	private string highscoreDataFilename = GlobalData.highscoreDataFilename;
	private string playerDataFilename = GlobalData.playerDataFilename;

	public List<LevelData> allLevelData = null;
	public List<AchievementData> allAchievementData = null;
	public List<HighscoreData> allHighscoreData = null;
	public PlayerData playerData = null;

	[MenuItem ("Window/Game Data Editor")]
	static void Init () {
		GameDataEditor window = (GameDataEditor)EditorWindow.GetWindow (typeof(GameDataEditor));
		window.Show ();
	}

	void OnGUI () {
		if ((allLevelData.Count != 0) && (allAchievementData.Count != 0) && (allHighscoreData.Count != 0) && (playerData != null)) {
			SerializedObject serializedObject = new SerializedObject (this);
			SerializedProperty serializedProperty;
			serializedProperty = serializedObject.FindProperty ("allLevelData");
			EditorGUILayout.PropertyField (serializedProperty, true);
			serializedObject.ApplyModifiedProperties ();

			serializedProperty = serializedObject.FindProperty ("allAchievementData");
			EditorGUILayout.PropertyField (serializedProperty, true);
			serializedObject.ApplyModifiedProperties ();

			serializedProperty = serializedObject.FindProperty ("allHighscoreData");
			EditorGUILayout.PropertyField (serializedProperty, true);
			serializedObject.ApplyModifiedProperties ();

			serializedProperty = serializedObject.FindProperty ("playerData");
			EditorGUILayout.PropertyField (serializedProperty, true);
			serializedObject.ApplyModifiedProperties ();

			if (GUILayout.Button ("Save Data")) {
				SaveGameData ();
			}
		}
		if (GUILayout.Button ("Load Data")) {
			LoadGameData ();
		}
	}

	private void LoadGameData () {
		string filePath;
		BinaryFormatter bFormatter = new BinaryFormatter ();

		filePath = Path.Combine (Application.streamingAssetsPath, levelDataFilename);
		if (File.Exists (filePath)) {
			FileStream fileStream = File.Open (filePath, FileMode.Open);
			allLevelData = (List<LevelData>)bFormatter.Deserialize (fileStream);
			fileStream.Close ();
			Debug.Log ("level load");
		} else
			allLevelData = new List<LevelData>();
		
		filePath = Path.Combine (Application.streamingAssetsPath, achievementDataFilename);
		if (File.Exists (filePath)) {
			FileStream fileStream = File.Open (filePath, FileMode.Open);
			allAchievementData = (List<AchievementData>)bFormatter.Deserialize (fileStream);
			fileStream.Close ();
			Debug.Log ("achievement load");
		} else
			allAchievementData = new List<AchievementData>();
		
		filePath = Path.Combine (Application.streamingAssetsPath, highscoreDataFilename);
		if (File.Exists (filePath)) {
			FileStream fileStream = File.Open (filePath, FileMode.Open);
			allHighscoreData = (List<HighscoreData>)bFormatter.Deserialize (fileStream);
			fileStream.Close ();
			Debug.Log ("highscore load");
		} else
			allHighscoreData = new List<HighscoreData> ();
		
		filePath = Path.Combine (Application.streamingAssetsPath, playerDataFilename);
		if (File.Exists (filePath)) {
			FileStream fileStream = File.Open (filePath, FileMode.Open);
			playerData = (PlayerData)bFormatter.Deserialize (fileStream);
			fileStream.Close ();
			Debug.Log ("player load");
		} else
			playerData = new PlayerData ();
	}

	private void SaveGameData () {
		string filePath;
		BinaryFormatter bFormatter = new BinaryFormatter ();

		filePath = Path.Combine (Application.streamingAssetsPath, levelDataFilename);
		FileStream fileStream = File.Create (filePath);
		bFormatter.Serialize (fileStream, allLevelData);
		fileStream.Close ();

		filePath = Path.Combine (Application.streamingAssetsPath, achievementDataFilename);
		fileStream = File.Create (filePath);
		bFormatter.Serialize (fileStream, allAchievementData);
		fileStream.Close ();

		filePath = Path.Combine (Application.streamingAssetsPath, highscoreDataFilename);
		fileStream = File.Create (filePath);
		bFormatter.Serialize (fileStream, allHighscoreData);
		fileStream.Close ();

		filePath = Path.Combine (Application.streamingAssetsPath, playerDataFilename);
		fileStream = File.Create (filePath);
		bFormatter.Serialize (fileStream, playerData);
		fileStream.Close ();
	}
}
