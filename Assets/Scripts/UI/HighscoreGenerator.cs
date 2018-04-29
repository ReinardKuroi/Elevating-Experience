using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HighscoreGenerator : MonoBehaviour {

	public GameObject textPrefab;

	private PlayerData playerData;

	private List<string> displayStrings = new List<string> ();
	private static readonly string template = "{0}\n\nHighscore: {1}\nPlay time: {2}\nPlay count: {3}\nAchievements: {4}/{5}";

	private int pointer = 0;
	private int listSize;

	//">:/{0}\n
	//Highscore: {1}\n
	//Play time: {2}\n
	//Play count: {3}\n
	//Achievements: {4}/{5}"
	void Start () {
		playerData = GlobalData.Instance.ActivePlayerData;

		displayStrings = GenerateTable ();

		listSize = displayStrings.Count;

		DrawScreen (displayStrings [pointer]);
	}

	List<string> GenerateTable () {
		List<string> list = new List<string> ();

		int total = 0;
		int unlocked = 0;
		foreach (UnlockedAchievement achievement in playerData.unlockedAchievements) {
			total++;
			if (achievement.isUnlocked) {
				unlocked++;
			}
		}

		TimeSpan timeSpan = TimeSpan.FromSeconds (Mathf.RoundToInt (playerData.PlayTime));
		string timespan = string.Format ("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);

		list.Add (string.Format(template, "\tTotal: ", playerData.TotalScore, timespan, playerData.PlayCount, unlocked, total));

		foreach (ScoreData data in playerData.scoreData) {
			total = 0;
			unlocked = 0;
			foreach (AchievementData aData in GlobalData.Instance.Achievements) {
				if (aData.levelRestriction == data.levelName) {
					total++;
					if (playerData.unlockedAchievements.Find(item => item.achievementName == aData.achievementName).isUnlocked) {
						unlocked++;
					}
				}
			}
			list.Add (string.Format (template, "//: " + data.showName, data.highscore, Mathf.RoundToInt (data.playTime) + "s", data.playCount, unlocked, total));
		}

		return list;
	}

	void DrawScreen (string tableContents) {
		GameObject g;
		Transform t = gameObject.transform.Find ("Text");
		if (t == null) {
			g = GameObject.Instantiate (textPrefab, gameObject.transform);
		} else {
			g = t.gameObject;
		}
		TextMeshProUGUI tmpro = g.GetComponent<TextMeshProUGUI> ();
		RectTransform rT = g.GetComponent<RectTransform> ();

		g.name = "Text";
		rT.sizeDelta = new Vector2 (290, 240);
		tmpro.alignment = TMPro.TextAlignmentOptions.TopLeft;
		tmpro.enableAutoSizing = false;
		tmpro.fontSize = 28;
		tmpro.text = tableContents;

	}

	public void Next () {
		pointer++;
		if (pointer >= listSize) {
			pointer = 0;
		}
		DrawScreen (displayStrings [pointer]);
	}

	public void Prev () {
		if (pointer == 0) {
			pointer = listSize;
		}
		pointer--;
		DrawScreen (displayStrings [pointer]);
	}
}