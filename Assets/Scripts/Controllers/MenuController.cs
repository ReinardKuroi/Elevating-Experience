using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public GameObject pauseUI;
	public GameObject settingsUI;
	public GameObject pauseButton;
	public GameObject settingsButton;

	private bool isPaused = false;
	private Text t;

	void Awake () {
		t = pauseButton.GetComponentInChildren<Text> ();
	}

	void TogglePause () {
		if (isPaused) {
			t.text = "PAUSE";
			Time.timeScale = 1f;
		} else {
			t.text = "PLAY";
			Time.timeScale = 0f;
		}
		isPaused = !isPaused;
	}

	void ToggleUI (GameObject ui) {
		ui.SetActive (!ui.activeSelf);
	}

	public void PauseClick () {
		if (settingsUI.activeSelf)
			ToggleUI (settingsUI);
		TogglePause ();
	}

	public void SettingsClick () {
		if (!isPaused)
			TogglePause ();
		ToggleUI (settingsUI);
	}
}