using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad {
	
	public static void LoadFile<T> (ref T obj, string fileName) where T : new () {
		string filePath;
		if (Application.platform == RuntimePlatform.Android) {
			filePath = "jar:file://" + Application.streamingAssetsPath + "!/assets/" + fileName;
		} else {
			filePath = Path.Combine (Application.streamingAssetsPath, fileName);
		}
		if (File.Exists (filePath)) {
			BinaryFormatter bFormatter = new BinaryFormatter ();
			FileStream fileStream = File.Open (filePath, FileMode.Open);
			obj = (T)bFormatter.Deserialize (fileStream);
			fileStream.Close ();
		} else {
			obj = new T ();
		}
	}

	public static void SaveFile<T> (ref T obj, string fileName) where T : class {
		string filePath;
		if (Application.platform == RuntimePlatform.Android) {
			filePath = "jar:file://" + Application.streamingAssetsPath + "!/assets/" + fileName;
		} else {
			filePath = Path.Combine (Application.streamingAssetsPath, fileName);
		}
		BinaryFormatter bFormatter = new BinaryFormatter ();
		FileStream fileStream = File.Create (filePath);
		bFormatter.Serialize (fileStream, obj);
		fileStream.Close ();
	}
}