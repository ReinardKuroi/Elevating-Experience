using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour {
	void Awake () {
		Time.timeScale = 1f;
		string name = ((GlobalData.Instance != null) && (GlobalData.Instance.allPlayerData [GlobalData.Instance.activePlayer] != null))
			? GlobalData.Instance.loadNext : "MainMenu";
		StartCoroutine (LoadNew (name));
		Debug.Log ("Loading level " + name);
	}

	IEnumerator LoadNew (string name) {
		yield return new WaitForSeconds (3);

		AsyncOperation ao = SceneManager.LoadSceneAsync (name);

		while (!ao.isDone) {
			yield return null;
		}
	}
}
