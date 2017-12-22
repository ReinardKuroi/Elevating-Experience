using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEditor;
using UnityEngine.Serialization;

namespace test {

	public class GameDataEditor: EditorWindow {

		private string levelDataFilename = GlobalData.levelDataFilename;
		private string playerDataFilename = GlobalData.playerDataFilename;
		private List<string> allDataList = new List<string> { "allLevelData", "allPlayerData" };

		public Dictionary<string, LevelData> allLevelData;
		public Dictionary<string, PlayerData> allPlayerData;

		[MenuItem ("Window/GameObject Data Editor")]
		public static void ShowWindow () {
			EditorWindow.GetWindow<GameDataEditor> ();
		}

		void OnGUI () {
			if ((allLevelData != null) && (allPlayerData != null)) {
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
			SaveLoad.LoadFile (ref allLevelData, levelDataFilename);
			SaveLoad.LoadFile (ref allPlayerData, playerDataFilename);
		}

		private void SaveGameData () {
			SaveLoad.SaveFile (ref allLevelData, levelDataFilename);
			SaveLoad.SaveFile (ref allPlayerData, playerDataFilename);
		}
	}
}