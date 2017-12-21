using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour {

	public void Awake () {

	}

	public void Create (InputField input) {
		string playerName = input.text;
		GlobalData.Instance.NewPlayer (playerName);
		GlobalData.Instance.InitPlayerDictionary ();
		SceneManager.LoadScene ("MainMenu");
	}
}
