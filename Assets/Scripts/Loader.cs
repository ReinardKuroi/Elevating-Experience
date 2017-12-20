using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour {
	void Awake () {
		Time.timeScale = 1f;
		string name;
		if (GlobalData.Instance == null) {
			Debug.Log ("GlobalData is null");
			name = "MainMenu";
		} else {
			if (GlobalData.Instance.allPlayerData [GlobalData.Instance.activePlayer] == null) {
				Debug.Log ("No active player");
				name = "MainMenu";
			}
			else {
				name = GlobalData.Instance.loadNext;
			}
		}
		Debug.Log ("Loading level " + name);
		StartCoroutine (LoadNew (name));
	}

	IEnumerator LoadNew (string name) {
		yield return new WaitForSeconds (2);

		AsyncOperation ao = SceneManager.LoadSceneAsync (name);

		while (!ao.isDone) {
			yield return null;
		}
	}
}
