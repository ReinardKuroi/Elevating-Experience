using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {

	public static SoundManager Instance { get; private set; }

	public AudioMixer aMixer;

	private AudioSource musicSource;
	private AudioSource sfxSource;

	private IEnumerator coroutine;

	private System.Random random;

	void Awake () {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
		coroutine = null;
		AudioSource[] sources = gameObject.GetComponents<AudioSource> ();
		musicSource = sources [0];
		sfxSource = sources [1];
		random = new System.Random ();
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
	
		intro = Resources.Load ("Sounds/" + name + "-intro", typeof(AudioClip)) as AudioClip;
		loop = Resources.Load ("Sounds/" + name + "-loop", typeof(AudioClip)) as AudioClip;

		if ((intro != null) && (loop != null)) {
			coroutine = PlayMusic (intro, loop);
			StartCoroutine (coroutine);
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
		if (coroutine != null) {
			StopCoroutine (coroutine);
			coroutine = null;
		}
		musicSource.volume = Mathf.Lerp (1f, 0f, Time.time);
		musicSource.Stop ();
		musicSource.volume = 1f;
	}

	public void PlaySound (string name) {
		AudioClip sound = Resources.Load ("Sounds/" + name, typeof(AudioClip)) as AudioClip;

		//randomize pitch?
		sfxSource.pitch = 1f + ((float)random.NextDouble() - 0.5f)/5;

		sfxSource.clip = sound;
		sfxSource.loop = false;
		sfxSource.Play ();
	}
}