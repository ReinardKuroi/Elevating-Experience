using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour {

	public static LoadManager Instance { get; private set; }

	void Awake () {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
		LoadScene ("Login");
	}

	//overloaded method to load a scene

	public void LoadScene (string name) {
		Debug.Log ("Loading scene: " + name);
		StartCoroutine (LoadNew (name));
	}

	//overloaded coroutine for scene loading
	//IMPLEMENT MORE FANCY STUFF HERE

	IEnumerator LoadNew (string name) {
		yield return new WaitForEndOfFrame ();

		AsyncOperation ao = SceneManager.LoadSceneAsync (name);

		while (!ao.isDone) {
			yield return null;
		}
	}

	IEnumerator LoadNew (int index) {
		yield return new WaitForEndOfFrame ();

		AsyncOperation ao = SceneManager.LoadSceneAsync (index);

		while (!ao.isDone) {
			yield return null;
		}
	}
}