using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {

	public static SoundManager Instance { get; private set; }

	public AudioMixer aMixer;

	void Awake () {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
	}

	//Gets active PlayerData and checks audio parameters
	//applies them to audio mixer
	public void SetVolume () {
		PlayerData playerData = GlobalData.Instance.ActivePlayerData;
		foreach (AudioSettings audio in playerData.audioSettings) {
			if (audio.enabled) {
				aMixer.SetFloat (audio.name, audio.volume);
			} else {
				aMixer.SetFloat (audio.name, -80f);
			}
		}
	}

	//Gets slider values, NOT SLIDERS
	//sets active PlayerData to it
	public void GetVolume (string name, float volume, bool isOn) {
		PlayerData playerData = GlobalData.Instance.ActivePlayerData;
		AudioSettings audio = playerData.audioSettings.Find(item => item.name == name);
		audio.volume = Mathf.Log10 (volume) * 20;
		audio.enabled = isOn;
		GlobalData.Instance.ActivePlayerData = playerData;
	}
}