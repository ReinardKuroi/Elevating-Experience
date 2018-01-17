using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour {

	private Slider musicSlider;
	private Toggle musicToggle;

	private Slider sfxSlider;
	private Toggle sfxToggle;

	private PlayerData playerData;
	private bool dontIgnoreTrigger;
	private Dictionary<string, AudioControl> audioControls;

	void Awake () {
		audioControls = new Dictionary<string, AudioControl> ();
		audioControls.Add (GlobalData.exposedMusicVolume, new AudioControl (GameObject.Find (GlobalData.exposedMusicVolume)));
		audioControls.Add (GlobalData.exposedSFXVolume, new AudioControl (GameObject.Find (GlobalData.exposedSFXVolume)));

		foreach (KeyValuePair<string, AudioControl> control in audioControls) {
			control.Value.Slider.onValueChanged.AddListener (delegate {
				Trigger (control.Key);
			});
			control.Value.Toggle.onValueChanged.AddListener (delegate {
				Trigger (control.Key);
			});
		}
		dontIgnoreTrigger = false;

		Init ();
	}

	void Init () {
		PlayerData playerData = GlobalData.Instance.ActivePlayerData;
		foreach (AudioSettings audio in playerData.audioSettings) {
			audioControls [audio.name].Slider.value = Mathf.Pow (10, audio.volume / 20);
			audioControls [audio.name].Toggle.isOn = audio.enabled;
		}
		SoundManager.Instance.SetVolume ();
		dontIgnoreTrigger = true;
	}

	public void Trigger (string name) {
		if (dontIgnoreTrigger) {
			SoundManager.Instance.GetVolume (name, audioControls[name].Slider.value, audioControls[name].Toggle.isOn);
			SoundManager.Instance.SetVolume ();
		}
	}

	private class AudioControl {
		public Slider Slider;
		public Toggle Toggle;

		public AudioControl (GameObject g) {
			this.Slider = g.GetComponentInChildren<Slider> ();
			this.Toggle = g.GetComponentInChildren<Toggle> ();
		}
	}
}