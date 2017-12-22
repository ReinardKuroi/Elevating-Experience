using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	public GameObject buttonPrefab;
	public PlayerData activePlayer;

	void Start () {
		//ADD INITIALIZATION I DONT CARE HOW OR WHY FUCK YOU FUTURE ME

		//greeting text or something
		//like, show player name and stats
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

	public void LoadScene () {
		SceneManager.LoadScene ("LoadingScreen");
	}
}
