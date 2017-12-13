using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad {
	public string filePath;

//	public ButtonParams buttonParams;

	public void Serialize () {
//		string jsonString = JsonUtility.ToJson (buttonParams, true);
//		File.WriteAllText (filePath, jsonString);
	}

	public void Deserialize () {
		string jsonString = File.ReadAllText (filePath);
//		buttonParams = JsonUtility.FromJson<ButtonParams> (jsonString);
	}
		
	public void LoadGameData (string path) {
		string jsonString = File.ReadAllText (path);
	}
}