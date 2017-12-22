using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//REWRITE THIS SHITTY MESS

/*
public class Loader : MonoBehaviour {
	void Awake () {
		Time.timeScale = 1f;
		string name;
		if (GlobalData.Instance == null) {
			Debug.Log ("GlobalData is null");
			name = "MainMenu";
		} else {
			if (GlobalData.Instance.getlast) {
				Debug.Log ("No active player");
				name = "MainMenu";
			}
			else {
				name = GlobalData.Instance.loadNext;
			}
		}
		Debug.Log ("Loading level " + name);
		StartCoroutine (LoadNew (name));
		SoundManager.Instance.Init ();
		GlobalData.Instance.Reset ();
	}

	IEnumerator LoadNew (string name) {
		yield return new WaitForSeconds (2);

		AsyncOperation ao = SceneManager.LoadSceneAsync (name);

		while (!ao.isDone) {
			yield return null;
		}
	}
}*/
