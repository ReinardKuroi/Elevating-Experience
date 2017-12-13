using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class GameDataEditor: EditorWindow {

	public GameData gameData;

	private string gameDataProjectFilepath = "/StreamingAssets/data.json";

	[MenuItem ("Window/Game Data Editor")]
	static void Init () {
		GameDataEditor window = (GameDataEditor)EditorWindow.GetWindow (typeof(GameDataEditor));
		window.Show ();
	}

	void OnGUI () {
		if (gameData != null) {
			SerializedObject serializedObject = new SerializedObject (this);
			SerializedProperty serializedProperty = serializedObject.FindProperty ("gameData");

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
		string filePath = Application.dataPath + gameDataProjectFilepath;
		if (File.Exists (filePath)) {
			string jsonData = File.ReadAllText (filePath);
			gameData = JsonUtility.FromJson<GameData> (jsonData);
		} else {
			gameData = new GameData ();
		}
	}

	private void SaveGameData () {
		string jsonData = JsonUtility.ToJson (gameData);
		string filePath = Application.dataPath + gameDataProjectFilepath;
		File.WriteAllText (filePath, jsonData);
	}
}
