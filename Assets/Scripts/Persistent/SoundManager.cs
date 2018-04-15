using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {

	public static SoundManager Instance { get; private set; }

	public AudioMixer aMixer;

	private AudioSource musicSource;
	private AudioSource sfxSource;

	void Awake () {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}

		AudioSource[] sources = gameObject.GetComponents<AudioSource> ();
		musicSource = sources [0];
		sfxSource = sources [1];
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

	public void LevelMusic (string name) {
		AudioClip intro;
		AudioClip loop;
	
		intro = Resources.Load (name + "-intro") as AudioClip;
		loop = Resources.Load (name + "-loop") as AudioClip;

		if ((intro != null) && (loop != null)) {
			StartCoroutine (PlayMusic (intro, loop));
		}
	}

	public IEnumerator PlayMusic (AudioClip intro, AudioClip loop) {
		musicSource.clip = intro;
		musicSource.loop = false;
		musicSource.Play ();
		yield return new WaitWhile (() => musicSource.isPlaying);
		musicSource.clip = loop;
		musicSource.loop = true;
		musicSource.Play ();
	}

	public void StopMusic () {
		musicSource.Stop ();
	}
}