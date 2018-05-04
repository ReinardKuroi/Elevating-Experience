using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad {
	
	public static void LoadFile<T> (ref T obj, string fileName) where T : new () {
		string filePath;
		if (Application.platform == RuntimePlatform.Android) {
			filePath = "jar:file://" + Application.dataPath + "!/assets/" + fileName;
		} else {
			filePath = Path.Combine (Application.streamingAssetsPath, fileName);
		}

		if (File.Exists (filePath)) {
			string dataAsJson = File.ReadAllText (filePath);
			obj = (T)JsonHelper.FromJson<T> (dataAsJson);
//			BinaryFormatter bFormatter = new BinaryFormatter ();
//			FileStream fileStream = File.Open (filePath, FileMode.Open);
//			obj = (T)bFormatter.Deserialize (fileStream);
//			fileStream.Close ();
		} else {
			obj = new T ();
		}
	}

	public static void SaveFile<T> (ref T obj, string fileName) where T : class {
		string filePath;
		if (Application.platform == RuntimePlatform.Android) {
			filePath = "jar:file://" + Application.dataPath + "!/assets/" + fileName;
		} else {
			filePath = Path.Combine (Application.streamingAssetsPath, fileName);
		}

		string dataAsJson = JsonHelper.ToJson (obj, true);
//		Debug.Log (dataAsJson);
		File.WriteAllText (filePath, dataAsJson);

//		BinaryFormatter bFormatter = new BinaryFormatter ();
//		FileStream fileStream = File.Create (filePath);
//		bFormatter.Serialize (fileStream, obj);
//		fileStream.Close ();
	}
		
	public static class JsonHelper
	{
		public static T FromJson<T>(string json)
		{
			Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
			return wrapper.Items;
		}

		public static string ToJson<T>(T item)
		{
			Wrapper<T> wrapper = new Wrapper<T>();
			wrapper.Items = item;
			return JsonUtility.ToJson(wrapper);
		}

		public static string ToJson<T>(T item, bool prettyPrint)
		{
			Wrapper<T> wrapper = new Wrapper<T>();
			wrapper.Items = item;
			return JsonUtility.ToJson(wrapper, prettyPrint);
		}

		[System.Serializable]
		private class Wrapper<T>
		{
			public T Items;
		}
	}
}