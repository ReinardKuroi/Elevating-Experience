using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

	void Start () {
		//ADD INITIALIZATION I DONT CARE HOW OR WHY FUCK YOU FUTURE ME

		//greeting text or something
		//like, show player name and stats
		Debug.Log("Player #" + GlobalData.Instance.LastActivePlayer.ToString () + ", name: " + GlobalData.Instance.ActivePlayerData.name);
	}

	public void Quit () {

		GameManager.Instance.Quit ();

		#if UNITY_STANDALONE
		Application.Quit ();
		#endif

		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}

	public void Reset () {
		GlobalData.Instance.ResetPlayerData ();
		LoadManager.Instance.LoadScene ("Login");
	}

	public void Play () {
		LevelData levelData = GlobalData.Instance.ActiveLevelData;
		GameManager.Instance.Play ();
		LoadManager.Instance.LoadScene (levelData.levelName);
	}
}
