using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameDataEditor: EditorWindow {

	public GameData gameData;

	private string gameDataProjectFilepath = "/StreamingAssets/save.dat";

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
			BinaryFormatter bFormatter = new BinaryFormatter ();
			FileStream fileStream = File.Open (filePath, FileMode.Open);
			gameData = (GameData)bFormatter.Deserialize (fileStream);
			fileStream.Close ();
		} else {
			gameData = new GameData ();
		}
	}

	private void SaveGameData () {
		string filePath = Application.dataPath + gameDataProjectFilepath;
		BinaryFormatter bFormatter = new BinaryFormatter ();
		FileStream fileStream = File.Create (filePath);
		bFormatter.Serialize (fileStream, gameData);
		fileStream.Close ();
	}
}
