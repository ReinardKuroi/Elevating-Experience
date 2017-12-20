using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGenerator : MonoBehaviour {
	
	public GameObject buttonPrefab;

	void Start () {
		foreach (KeyValuePair<string, int> pair in GlobalData.Instance.sceneDict) {
			int i = pair.Value;
			string name = pair.Key;
			GameObject newButton = (GameObject)GameObject.Instantiate (buttonPrefab);
			newButton.transform.SetParent (gameObject.transform, false);
			newButton.SetActive (true);
			newButton.name = name;
			newButton.GetComponent<Button> ().interactable = GlobalData.Instance.allLevelData [i].isUnlocked;
			newButton.GetComponent<Button> ().onClick.AddListener (delegate{OnClick(name);});
			newButton.GetComponentInChildren<Text> ().text = GlobalData.Instance.allLevelData [i].levelShowName;
		}
	}

	void OnClick (string name) {
		GlobalData.Instance.allPlayerData [GlobalData.Instance.activePlayer].selectedLevel = name;
		SaveLoad.SaveFile (ref GlobalData.Instance.allPlayerData, GlobalData.playerDataFilename);
		GlobalData.Instance.loadNext = GlobalData.Instance.allPlayerData [GlobalData.Instance.activePlayer].selectedLevel;
		Debug.Log("Set level to " + name);
	}
}