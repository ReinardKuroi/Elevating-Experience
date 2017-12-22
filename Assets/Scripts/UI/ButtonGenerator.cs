using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
public class ButtonGenerator : MonoBehaviour {
	
	public GameObject buttonPrefab;

	private PlayerData playerData;
	private List<GameObject> buttons = new List<GameObject> ();

	//THIS IS ALL DISGUSTING FIX IT SUFFER BUTCH
	//JESUS WHY DOES IT EVEN EXIST

	//remake into generic..?

	void Start () {
		playerData = GlobalData.Instance.GetActivePlayerData ();
		foreach (KeyValuePair<string, int> pair in GlobalData.Instance.levelDict) {
			int i = pair.Value;
			string name = pair.Key;
			GameObject newButton = (GameObject)GameObject.Instantiate (buttonPrefab);

			newButton.transform.SetParent (gameObject.transform, false);
			newButton.SetActive (true);
			newButton.name = name;
			newButton.GetComponent<Button> ().interactable = playerData.unlockedLevels [i];
			newButton.GetComponent<Button> ().onClick.AddListener (delegate{OnClick(name);});
//			newButton.GetComponentInChildren<Text> ().text = GlobalData.Instance.allLevelData [i].levelShowName;
			buttons.Add (newButton);
		}
		ResetColors ();
	}

	void OnClick (string name) {
		playerData.activeLevel = name;
		GlobalData.Instance.loadNext = playerData.activeLevel;
		ResetColors ();
	}

	void ResetColors () {
		int i = 0;
		int k;
		foreach (GameObject b in buttons) {
			GlobalData.Instance.levelDict.TryGetValue (playerData.activeLevel, out k);
			if (k == i)
				b.GetComponent<Image> ().color = Color.red;
			else
				b.GetComponent<Image> ().color = Color.white;
			i++;
		}
	}

	public void Done () {
		GlobalData.Instance.SetActivePlayer (playerData);
	}
}*/