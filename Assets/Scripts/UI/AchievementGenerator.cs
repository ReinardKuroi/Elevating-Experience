using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementGenerator : MonoBehaviour {

	public GameObject achievementPrefab;

	private PlayerData playerData;
	private List<AchievementData> achievements;
	private int pointer;
	private int listSize;

	void Start () {
		playerData = GlobalData.Instance.ActivePlayerData;
		achievements = GlobalData.Instance.Achievements;

		listSize = achievements.Count;
		pointer = 0;

		DrawScreen (achievements [pointer]);
	}



	GameObject DrawScreen (AchievementData data) {
		GameObject g;
		Transform t = gameObject.transform.Find ("AchievementScreen");

		if (t == null) {
			g = GameObject.Instantiate (achievementPrefab, gameObject.transform);
		} else {
			g = t.gameObject;
		}

		TextMeshProUGUI nameText = g.transform.Find("MainText").gameObject.GetComponent<TextMeshProUGUI> ();
		TextMeshProUGUI descriptionText = g.transform.Find("InfoText").gameObject.GetComponent<TextMeshProUGUI> ();
		Image image = g.transform.Find("Image").gameObject.GetComponent<Image> ();

		g.name = "AchievementScreen";

		nameText.text = data.achievementName;
		descriptionText.text = data.description;
		if (playerData.unlockedAchievements.Find (item => item.achievementName == data.achievementName).isUnlocked) {
			image.sprite = (Sprite)Resources.Load (data.achievementName + "-unlocked");
		} else {
			image.sprite = (Sprite)Resources.Load (data.achievementName + "-locked");
		}

		return g;
	}

	public void Next () {
		pointer++;
		if (pointer >= listSize) {
			pointer = 0;
		}
		DrawScreen (achievements[pointer]);
	}

	public void Prev () {
		if (pointer == 0) {
			pointer = listSize;
		}
		pointer--;
		DrawScreen (achievements[pointer]);
	}
}