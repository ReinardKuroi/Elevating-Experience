using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour {

	private PlayerData playerData;
	private string musicVolume = SoundManager.Instance.musicVolume;
	private string sfxVolume = SoundManager.Instance.sfxVolume;
	private AudioMixer aMixer = SoundManager.Instance.aMixer;

	private GameObject music;
	private Slider musicSlider;
	private Toggle musicToggle;

	private GameObject sfx;
	private Slider sfxSlider;
	private Toggle sfxToggle;

	void Awake () {	
		music = GameObject.Find ("MusicVolumeControl");
		sfx = GameObject.Find ("SFXVolumeControl");

		musicSlider = music.GetComponentInChildren<Slider> ();
		musicToggle = music.GetComponentInChildren<Toggle> ();
		sfxSlider = sfx.GetComponentInChildren<Slider> ();
		sfxToggle = sfx.GetComponentInChildren<Toggle> ();
	}

	public void Init () {
		playerData = GlobalData.Instance.allPlayerData [GlobalData.Instance.activePlayer];

		musicSlider.value = playerData.musicVolume;
		musicToggle.isOn = playerData.musicEnabled;
		sfxSlider.value = playerData.sfxVolume;
		sfxToggle.isOn = playerData.sfxEnabled;
	}

	void Update () {
		if (gameObject.activeSelf) {
			if (musicSlider.enabled) {
				SetVolume (musicSlider.value, musicVolume);
				playerData.musicVolume = musicSlider.value;
			}
			if (sfxSlider.enabled) {
				SetVolume (sfxSlider.value, sfxVolume);
				playerData.sfxVolume = sfxSlider.value;
			}

			ToggleVolume (musicToggle.isOn, musicVolume, musicSlider);
			playerData.musicEnabled = musicToggle.isOn;
			ToggleVolume (sfxToggle.isOn, sfxVolume, sfxSlider);
			playerData.sfxEnabled = sfxToggle.isOn;
		}
	}

	void SetVolume (float volume, string cName) {
		aMixer.SetFloat (cName, (float)System.Math.Log10((double)volume)*20);
	}

	public void ToggleVolume (bool toggle, string cName, Slider slider) {
		if (!toggle) {
			aMixer.SetFloat (cName, -80f);
			slider.enabled = false;
		} else
			slider.enabled = true;
	}

	public void Done () {
		if (playerData != null) {
			GlobalData.Instance.SetActivePlayer (playerData);
			playerData = null;
		}
	}
}
