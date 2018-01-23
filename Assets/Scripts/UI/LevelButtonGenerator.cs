using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonGenerator : MonoBehaviour {

	public GameObject buttonPrefab;

	private List<GameObject> buttons = new List<GameObject> ();
	private delegate void OnClick ();

	void Start () {
		PlayerData playerData = GlobalData.Instance.ActivePlayerData;

		foreach (ScoreData data in playerData.scoreData) {
			OnClick OnClickLevel = delegate {
				GlobalData.Instance.ActiveLevelIndex = data.index;
				Colorize ();
			};

			buttons.Add (NewButton (OnClickLevel, data.showName, buttonPrefab, data.isUnlocked));
		}
		Colorize ();
	}

	//Generic button creator
	//Creates buttons from a prefab

	GameObject NewButton (OnClick onClick, string name, GameObject prefab, bool interactable) {
		GameObject button = (GameObject)GameObject.Instantiate (prefab);

		button.transform.SetParent (gameObject.transform, false);
		button.SetActive (true);
		button.name = name;
		button.GetComponent<Button> ().interactable = interactable;
		button.GetComponent<Button> ().onClick.AddListener (delegate {onClick ();});
		button.GetComponentInChildren<Text> ().text = name;

		return button;
	}

	void Colorize () {
		PlayerData playerData = GlobalData.Instance.ActivePlayerData;
		foreach (GameObject g in buttons) {
			g.GetComponent<Image> ().color = Color.white;
		}
		buttons [playerData.activeLevel].GetComponent<Image> ().color = Color.red;
	}
}