using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour {

	public Text welcomeText;

	public void Awake () {
		OpenLoginMenu ();
	}

	public void OpenLoginMenu () {
		Debug.Log ("Opened 'Login' Menu.");
		int activePlayer = GlobalData.Instance.GetLastActivePlayer ();
		if (activePlayer == -1) {
			//no activeplayer found
			//can only create new
			//disable load button
			//disable continue button
			Debug.Log ("No active player found.");
			welcomeText.text = "WELCOME!";
		} else {
			GlobalData.Instance.SetLastActivePlayer (activePlayer);
			PlayerData playerData = GlobalData.Instance.GetActivePlayerData ();
			Debug.Log ("Got active player " + playerData.name);
			welcomeText.text = "WELCOME, " + playerData.name + "!";
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
		Loader.Instance.LoadScene ("MainMenu");
	}

	public void Load () {
		Debug.Log ("Pressed 'Load'.");
		Loader.Instance.LoadScene ("MainMenu");
	}

	public void Create (InputField input) {
		Debug.Log ("Pressed 'New'.");
		string playerName = input.text;
		GlobalData.Instance.CreateNewPlayerData (playerName);
		SoundManager.Instance.SetVolume ();
		Loader.Instance.LoadScene ("MainMenu");
	}
}