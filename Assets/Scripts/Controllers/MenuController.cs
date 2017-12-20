using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public GameObject pauseCanvas;
	public GameObject pauseUI;
	public GameObject settingsUI;
	public GameObject pauseButton;
	public GameObject settingsButton;

	bool TogglePause () {
		pauseCanvas.SetActive (!pauseCanvas.activeSelf);
		if (pauseCanvas.activeSelf) {
			Time.timeScale = 0f;
			pauseButton.GetComponentInChildren<Text> ().text = "PLAY";
		} else {
			Time.timeScale = 1f;
			pauseButton.GetComponentInChildren<Text> ().text = "PAUSE";
		}
		return pauseCanvas.activeSelf;
	}

	void ToggleUI (GameObject ui) {
		ui.SetActive (!ui.activeSelf);
	}

	public void PauseClick () {
		if (TogglePause ()) {
			pauseUI.SetActive (true);
			settingsUI.SetActive (false);
		}
	}

	public void SettingsClick () {
		if (!pauseCanvas.activeSelf) {
			TogglePause ();
		}
		if (!settingsUI.activeSelf) {
			pauseUI.SetActive (false);
			settingsUI.SetActive (true);
		} else {
			pauseUI.SetActive (true);
			settingsUI.SetActive (false);
		}
	}
}