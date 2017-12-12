using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour {

	public static GlobalData Instance { get; private set; }

	public const int MULTI = 5;
	public const int CLICK = 1;

	public int score = 0;
	public int highscore = 0;

	private void Awake () {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}

		highscore = PlayerPrefs.GetInt ("Highscore", 0);
	}
}