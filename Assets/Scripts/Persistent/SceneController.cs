using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

	public static SceneController Instance { get; private set; }

	void Awake () {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
		SceneManager.LoadScene ("Default");
	}

	public static string GetSceneName () {
		return SceneManager.GetActiveScene ().name;
	}
}
