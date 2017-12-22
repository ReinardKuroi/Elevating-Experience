using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour {

	public void Awake () {
		int activePlayer = GlobalData.Instance.GetLastActivePlayer ();
		if (activePlayer == -1) {
			//no activeplayer found
			//can only create new
			//disable load button
			//disable continue button
			Debug.Log ("No active player found.");
		} else {
			Debug.Log ("Got active player " + activePlayer);
			GlobalData.Instance.SetActivePlayer (activePlayer);
			SoundManager.Instance.SetVolume ();
			//set text to welcome, playername
			//enable continue button
		}
	}

	public void OpenNewMenu () {
		Debug.Log ("Opened 'New' Menu.");
		//nothing?
	}

	public void OpenLoadMenu () {
		Debug.Log ("Opened 'Load' Menu.");
		//initialize button selector or something
		//like in button generator for levels
	}

	public void Continue () {
		Debug.Log ("Pressed 'Continue'.");
		SceneManager.LoadScene ("MainMenu");
	}

	public void Load (int index) {
		Debug.Log ("Pressed 'Load'.");
//		GlobalData.Instance.SetActivePlayer (index);
		SceneManager.LoadScene ("MainMenu");
	}

	public void Create (InputField input) {
		Debug.Log ("Pressed 'New'.");
		string playerName = input.text;
		GlobalData.Instance.CreateNewPlayerData (playerName);
		SceneManager.LoadScene ("MainMenu");
	}
}