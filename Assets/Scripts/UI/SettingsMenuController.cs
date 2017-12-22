using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour {

	private GameObject music;
	private Slider musicSlider;
	private Toggle musicToggle;

	private GameObject sfx;
	private Slider sfxSlider;
	private Toggle sfxToggle;

	private PlayerData playerData;

	void Awake () {	
		music = GameObject.Find ("MusicVolumeControl");
		sfx = GameObject.Find ("SFXVolumeControl");

		musicSlider = music.GetComponentInChildren<Slider> ();
		musicToggle = music.GetComponentInChildren<Toggle> ();
		sfxSlider = sfx.GetComponentInChildren<Slider> ();
		sfxToggle = sfx.GetComponentInChildren<Toggle> ();

		Init ();
	}

	void Init () {
		PlayerData playerData = GlobalData.Instance.GetActivePlayerData ();
		musicSlider.value = Mathf.Pow (10, playerData.musicVolume / 20);
		musicToggle.isOn = playerData.musicEnabled;
		sfxSlider.value = Mathf.Pow (10, playerData.sfxVolume / 20);
		sfxToggle.isOn = playerData.sfxEnabled;
		Debug.Log ("UI Data: music " + musicToggle.isOn.ToString () + musicSlider.value.ToString () + ", sfx " + sfxToggle.isOn.ToString () + sfxSlider.value.ToString ());
		SoundManager.Instance.SetVolume ();
	}

	public void Trigger () { SoundManager.Instance.GetVolume (musicSlider.value, musicToggle.isOn, sfxSlider.value, sfxToggle.isOn); SoundManager.Instance.SetVolume (); }
}