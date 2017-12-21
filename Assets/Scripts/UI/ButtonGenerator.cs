using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGenerator : MonoBehaviour {
	
	public GameObject buttonPrefab;

	private PlayerData playerData;

	void Start () {
		playerData = GlobalData.Instance.GetActivePlayer ();
		foreach (KeyValuePair<string, int> pair in GlobalData.Instance.levelDict) {
			int i = pair.Value;
			string name = pair.Key;
			GameObject newButton = (GameObject)GameObject.Instantiate (buttonPrefab);

			newButton.transform.SetParent (gameObject.transform, false);
			newButton.SetActive (true);
			newButton.name = name;
			newButton.GetComponent<Button> ().interactable = playerData.unlockedLevels [i];
			newButton.GetComponent<Button> ().onClick.AddListener (delegate{OnClick(name);});
			newButton.GetComponentInChildren<Text> ().text = GlobalData.Instance.allLevelData [i].levelShowName;
		}
	}

	void OnClick (string name) {
		playerData.selectedLevel = name;
		GlobalData.Instance.loadNext = playerData.selectedLevel;
		Debug.Log("Set level to " + name);
	}

	public void Done () {
		GlobalData.Instance.allPlayerData [GlobalData.Instance.activePlayer] = playerData;
	}
}