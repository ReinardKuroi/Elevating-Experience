﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	public GameObject buttonPrefab;

	public void Quit () {
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
