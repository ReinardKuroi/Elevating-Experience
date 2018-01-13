using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

	void Start () {
		//ADD INITIALIZATION I DONT CARE HOW OR WHY FUCK YOU FUTURE ME

		//greeting text or something
		//like, show player name and stats
		Debug.Log("Player #" + GlobalData.Instance.GetLastActivePlayer ().ToString () + ", name: " + GlobalData.Instance.GetActivePlayerData ().name);
	}

	public void Quit () {

		GlobalData.Instance.SaveGameData ();

		#if UNITY_STANDALONE
		Application.Quit ();
		#endif

		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}

	public void Play () {
		LevelData levelData = GlobalData.Instance.GetActiveLevelData ();
		LoadManager.Instance.LoadScene (levelData.levelName);
		GameManager.Instance.Play ();
	}
}
