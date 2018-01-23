using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTableGenerator : MonoBehaviour {

	public GameObject textPrefab;

	void Start () {
		PlayerData playerData = GlobalData.Instance.ActivePlayerData;

		foreach (ScoreData data in playerData.scoreData) {
			ScoreData scoreData = playerData.scoreData.Find (item => item.levelName == data.levelName);
			NewText (textPrefab, scoreData.levelName, scoreData.highscore);
		}
	}

	GameObject NewText (GameObject prefab, string level, int highscore) {
		GameObject text = (GameObject)GameObject.Instantiate (prefab);

		text.transform.SetParent (gameObject.transform, false);
		text.SetActive (true);
		text.name = level;
		text.GetComponent<Text> ().alignment = TextAnchor.MiddleLeft;
		text.GetComponent<Text> ().text = " " + level + ": #" + highscore.ToString ();

		return text;
	}
}