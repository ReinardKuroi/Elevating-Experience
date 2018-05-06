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
			Debug.LogError (filePath);
			if (File.Exists (filePath)) {
				byte[] jsonByte;
				if (filePath.Contains ("://") || filePath.Contains (":///")) {
					WWW www = new WWW (filePath);
					jsonByte = www.bytes;
				} else {
					jsonByte = File.ReadAllBytes (filePath);
				}

				string dataAsJson = System.Text.Encoding.ASCII.GetString (jsonByte);
				Debug.LogError (dataAsJson);
				obj = (T)JsonHelper.FromJson<T> (dataAsJson);
			} else {
				obj = new T ();
			}
		} else {
			filePath = Path.Combine (Application.streamingAssetsPath, fileName);

			if (File.Exists (filePath)) {
				string dataAsJson = File.ReadAllText (filePath);
				obj = (T)JsonHelper.FromJson<T> (dataAsJson);
			} else {
				obj = new T ();
			}
		}
	}

	public static void SaveFile<T> (ref T obj, string fileName) where T : class {
		string filePath;
		if (Application.platform == RuntimePlatform.Android) {
			filePath = "jar:file://" + Application.dataPath + "!/assets/" + fileName;

			string dataAsJson = JsonHelper.ToJson (obj, true);
			byte[] jsonByte = System.Text.Encoding.ASCII.GetBytes (dataAsJson);
			File.WriteAllBytes (filePath, jsonByte);
		} else {
			filePath = Path.Combine (Application.streamingAssetsPath, fileName);

			string dataAsJson = JsonHelper.ToJson (obj, true);
			File.WriteAllText (filePath, dataAsJson);
		}
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