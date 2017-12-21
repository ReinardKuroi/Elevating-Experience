using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour {

	public static SoundManager Instance { get; private set; }

	public AudioMixer aMixer;
	public string musicVolume;
	public string sfxVolume;

	private PlayerData playerData;

	void Awake () {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
		musicVolume = "musicVolume";
		sfxVolume = "sfxVolume";
	}

	public void Init () {
		playerData = GlobalData.Instance.allPlayerData [GlobalData.Instance.activePlayer];

		if (playerData.musicEnabled)
			aMixer.SetFloat (musicVolume, (float)System.Math.Log10 ((double)playerData.musicVolume) * 20);
		else
			aMixer.SetFloat (musicVolume, -80f);
		if (playerData.sfxEnabled)
			aMixer.SetFloat (sfxVolume, (float)System.Math.Log10 ((double)playerData.sfxVolume) * 20);
		else
			aMixer.SetFloat (musicVolume, -80f);
	}
}
