using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButtonGenerator : MonoBehaviour {

	public GameObject buttonPrefab;
	public Sprite disabledImage;
	public Sprite enabledImage;
	public Sprite selectedImage;

	private List<GameObject> buttons = new List<GameObject> ();
	private delegate void OnClick ();

	void Start () {
		PlayerData playerData = GlobalData.Instance.ActivePlayerData;

		foreach (ScoreData data in playerData.scoreData) {
			OnClick OnClickLevel = delegate {
				GlobalData.Instance.ActiveLevelIndex = data.index;
				SoundManager.Instance.PlaySound ("menu-button");
				Colorize ();
			};

			GameObject g = NewButton (OnClickLevel, data.showName, buttonPrefab, data.isUnlocked);

			buttons.Add (g);
		}
		Colorize ();
	}

	//Generic button creator
	//Creates buttons from a prefab

	GameObject NewButton (OnClick onClick, string name, GameObject prefab, bool interactable) {
		GameObject host = (GameObject)GameObject.Instantiate (prefab);
		GameObject button = host.transform.Find ("Button").gameObject;
		GameObject text = host.transform.Find ("TextMeshPro").gameObject;

		Image i = button.GetComponent<Image> ();
		Button b = button.GetComponent<Button> ();
		TextMeshProUGUI t = text.GetComponent<TextMeshProUGUI> ();

		host.transform.SetParent (gameObject.transform, false);
		host.SetActive (true);
		host.name = name;
		if (interactable) {
			b.interactable = true;
			i.sprite = enabledImage;
		} else {
			b.interactable = false;
			i.sprite = disabledImage;
		}
		b.onClick.AddListener (delegate {onClick ();});
		t.text = name;

		return button;
	}

	void Colorize () {
		PlayerData playerData = GlobalData.Instance.ActivePlayerData;
		foreach (GameObject g in buttons) {
			Button b = g.GetComponent<Button> ();
			if (b.interactable) {
				g.GetComponent<Image> ().sprite = enabledImage;
			}
		}
		buttons [playerData.activeLevel].GetComponent<Image> ().sprite = selectedImage;
	}
}