using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {

	public static SoundManager Instance { get; private set; }

	public AudioMixer aMixer;

	private PlayerData playerData;

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
		playerData = GlobalData.Instance.GetActivePlayerData ();
		foreach (AudioSettings audio in playerData) {
			if (audio.enabled) {
				aMixer.SetFloat (audio.name, audio.volume);
			} else {
				aMixer.SetFloat (audio.name, -80f);
			}
		}
	}

	//Gets slider values, NOT SLIDERS
	//sets active PlayerData to it
	public void GetVolume (float musicVolume, bool musicEnabled, float sfxVolume, bool sfxEnabled) {
		playerData = GlobalData.Instance.GetActivePlayerData ();
		playerData.music.volume = Mathf.Log10 (musicVolume) * 20;
		playerData.music.enabled = musicEnabled;
		playerData.sfx.volume = Mathf.Log10 (sfxVolume) * 20;
		playerData.sfx.enabled = sfxEnabled;
		GlobalData.Instance.SetActivePlayerData (playerData);
	}
}
