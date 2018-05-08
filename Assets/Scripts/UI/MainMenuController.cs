using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

	public GameObject popupNotification;

	void Awake () {
		popupNotification.SetActive (false);
	}

	void Start () {
		PlayerData playerData = GlobalData.Instance.ActivePlayerData;
		foreach (ScoreData data in playerData.scoreData) {
			if (data.isUnlocked && data.playCount == 0f && data.levelName != "Common") {
				IEnumerator coroutine = ShowNotif (3f);
				StartCoroutine (coroutine);
				break;
			}
		}
		//greeting text or something
		//like, show player name and stats
		Debug.Log("Player #" + GlobalData.Instance.LastActivePlayer.ToString () + ", name: " + GlobalData.Instance.ActivePlayerData.name);
	}

	public void Quit () {

		GameManager.Instance.Quit ();

		Application.Quit ();
	}

	public void Reset () {
		GlobalData.Instance.DeletePlayerData (GlobalData.Instance.LastActivePlayer);
		LoadManager.Instance.LoadScene ("Login");
	}

	public void Play () {
		LevelData levelData = GlobalData.Instance.ActiveLevelData;
		LoadManager.Instance.LoadScene (levelData.levelName);
		GameManager.Instance.Play ();
	}

	public void Logout () {
		LoadManager.Instance.LoadScene ("Login");
	}

	private IEnumerator ShowNotif (float time) {
		yield return new WaitForSeconds (time/2f);
		popupNotification.SetActive (true);
		AnimationController.PlayAnimation (popupNotification, "Open");
		yield return new WaitForSeconds (time);
		AnimationController.PlayAnimation (popupNotification, "Close");
		yield return new WaitForSeconds (0.2f);
		popupNotification.SetActive (false);
	}
}
