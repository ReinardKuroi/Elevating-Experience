using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreTableGenerator : MonoBehaviour {

	public GameObject textPrefab;

	void Start () {
		PlayerData playerData = GlobalData.Instance.ActivePlayerData;

		foreach (UnlockedLevel uLevel in playerData.unlockedLevels) {
			Highscore highscore = playerData.highscores.Find (item => item.levelName == uLevel.levelName);
			NewText (textPrefab, highscore.levelName, highscore.highscore);
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