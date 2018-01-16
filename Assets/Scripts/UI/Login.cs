using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour {

	public Text welcomeText;
	public GameObject continueButton;
	public GameObject loginButton;

	public void Awake () {
		OpenLoginMenu ();
	}

	public void OpenLoginMenu () {
		Debug.Log ("Opened 'Login' Menu.");
		int activePlayer = GlobalData.Instance.GetLastActivePlayer ();
		if (activePlayer == -1) {
			continueButton.GetComponent<Button> ().interactable = false;
			loginButton.GetComponent<Button> ().interactable = false;
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
			continueButton.GetComponent<Button> ().interactable = true;
			loginButton.GetComponent<Button> ().interactable = true;
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
		LoadManager.Instance.LoadScene ("MainMenu");
	}

	public void Load () {
		Debug.Log ("Pressed 'Load'.");
		LoadManager.Instance.LoadScene ("MainMenu");
	}

	public void Create (InputField input) {
		Debug.Log ("Pressed 'New'.");
		string playerName = input.text;
		if (playerName.Length > 0) {
			GlobalData.Instance.CreateNewPlayerData (playerName);
			SoundManager.Instance.SetVolume ();
			LoadManager.Instance.LoadScene ("MainMenu");
		} else {
			Debug.Log ("Player name must not be empty.");
		}
	}
}