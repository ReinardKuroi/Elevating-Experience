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

		List<AchievementData> unlocked = new List<AchievementData> ();
		List<AchievementData> locked = new List<AchievementData> ();

		foreach (UnlockedAchievement data in playerData.unlockedAchievements) {
			if (data.isUnlocked)
				unlocked.Add (achievements.Find (item => item.achievementName == data.achievementName));
			else
				locked.Add (achievements.Find (item => item.achievementName == data.achievementName));
		}

		achievements = new List<AchievementData> ();
		achievements.AddRange (unlocked);
		achievements.AddRange (locked);

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

		TextMeshProUGUI nameText = g.transform.Find ("NameScreen/NameText").gameObject.GetComponent<TextMeshProUGUI> ();
		TextMeshProUGUI flavorText = g.transform.Find ("DescriptionScreen/FlavorText").gameObject.GetComponent<TextMeshProUGUI> ();
		GameObject lockedText = g.transform.Find ("DescriptionScreen/LockedText").gameObject;
		Image image = g.transform.Find ("ImageScreen/Image").gameObject.GetComponent<Image> ();
		Image imageLocked = g.transform.Find ("ImageScreen/ImageLocked").gameObject.GetComponent<Image> ();

		g.name = "AchievementScreen";
		nameText.text = data.achievementName;
		imageLocked.sprite = Resources.Load ("Sprites/AchievementLocked", typeof(Sprite)) as Sprite;
		imageLocked.color = Color.red;
		image.sprite = Resources.Load ("Sprites/" + data.achievementName, typeof(Sprite)) as Sprite;
		imageLocked.preserveAspect = true;
		image.preserveAspect = true;
		if (playerData.unlockedAchievements.Find (item => item.achievementName == data.achievementName).isUnlocked) {
			lockedText.SetActive (false);
			flavorText.gameObject.SetActive (true);
			flavorText.text = data.description;
			imageLocked.gameObject.SetActive (false);
		} else {
			lockedText.SetActive (true);
			flavorText.gameObject.SetActive (false);
			imageLocked.gameObject.SetActive (true);
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