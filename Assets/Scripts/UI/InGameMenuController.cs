using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenuController : MonoBehaviour {

	public GameObject pauseCanvas;
	public GameObject pauseUI;
	public GameObject settingsUI;
	public GameObject pauseButton;
	public Sprite pauseSprite;
	public Sprite playSprite;

	void Awake () {
		pauseCanvas.SetActive (false);
		pauseUI.SetActive (false);
		settingsUI.SetActive (false);
		pauseButton.GetComponent<Image> ().sprite = pauseSprite;
	}

	void TogglePause () {
		if (GameManager.Instance.game.CurrentState == GameManager.State.Active) {
			GameManager.Instance.Pause ();
			pauseCanvas.SetActive (true);
			pauseButton.GetComponent<Image> ().sprite = playSprite;
		}
		if (GameManager.Instance.game.CurrentState == GameManager.State.Paused) {
			GameManager.Instance.Resume ();
			pauseCanvas.SetActive (false);
			pauseButton.GetComponent<Image> ().sprite = pauseSprite;
		}
	}

	public void PauseClick () {
		TogglePause ();
		pauseUI.SetActive (true);
		settingsUI.SetActive (false);
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

	public void Quit () {
		GameManager.Instance.Stop ();
		LoadManager.Instance.LoadScene ("MainMenu");
	}
}