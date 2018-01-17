using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerButtonGenerator : MonoBehaviour {

	public GameObject buttonPrefab;

	private List<GameObject> buttons = new List<GameObject> ();
	private delegate void OnClick ();

	void Start () {
		foreach (PlayerData playerData in GlobalData.Instance.allPlayerData) {
			int index = GlobalData.Instance.allPlayerData.IndexOf (playerData);
			OnClick OnClickPlayer = delegate {
				Debug.LogError ("Selected Player " + index);
				GlobalData.Instance.LastActivePlayer = index;
				Colorize ();
			};

			buttons.Add (NewButton (OnClickPlayer, playerData.name, buttonPrefab, true));
		}
		Colorize ();
	}

	//Generic button creator
	//Creates buttons from a prefab

	GameObject NewButton (OnClick onClick, string name, GameObject prefab, bool interactable) {
		GameObject button = new GameObject ();

		button = (GameObject)GameObject.Instantiate (prefab);

		button.transform.SetParent (gameObject.transform, false);
		button.SetActive (true);
		button.name = name;
		button.GetComponent<Button> ().interactable = interactable;
		button.GetComponent<Button> ().onClick.AddListener (delegate {onClick ();});
		button.GetComponentInChildren<Text> ().text = name;

		return button;
	}

	void Colorize () {
		foreach (GameObject g in buttons) {
			g.GetComponent<Image> ().color = Color.white;
		}

		int index = GlobalData.Instance.LastActivePlayer;
		if (index != -1) {
			buttons [index].GetComponent<Image> ().color = Color.red;
		}
	}
}