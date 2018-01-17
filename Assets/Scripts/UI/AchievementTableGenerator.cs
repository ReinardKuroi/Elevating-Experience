using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementTableGenerator : MonoBehaviour {
	public GameObject achievementPrefab;

	private delegate void DelegateVoid (GameObject g);

	void Start () {
		PlayerData playerData = GlobalData.Instance.ActivePlayerData;

		foreach (UnlockedAchievement unlocked in playerData.unlockedAchievements) {
			AchievementData data = GlobalData.Instance.Achievements.Find (item => item.achievementName == unlocked.achievementName);

			NewAchievement (achievementPrefab, data, unlocked.isUnlocked);
		}
	}

	GameObject NewAchievement (GameObject prefab, AchievementData data, bool isUnlocked) {
		GameObject achievement = (GameObject)GameObject.Instantiate (prefab);
		GameObject g;

		achievement.transform.SetParent (gameObject.transform, false);
		achievement.SetActive (true);
		achievement.name = data.achievementName;

		g = achievement.transform.Find ("NameText").gameObject;
		if (g)
			g.GetComponent<Text> ().text = data.achievementName;
		g = achievement.transform.Find ("StateText").gameObject;
		if (g)
			g.GetComponent<Text> ().text = isUnlocked ? "UNLOCKED" : "LOCKED";
		g = achievement.transform.Find ("DescriptionText").gameObject;
		if (g)
			g.GetComponent<Text> ().text = "Get this by achieving " + data.triggerValue.ToString () + " " + data.triggerName + " on " + data.levelRestriction + " theme.";
		g = achievement.transform.Find ("Image").gameObject;
		if (g)
			g.GetComponent<Image> ().color = isUnlocked ? Color.yellow : Color.gray;

		return achievement;
	}
}