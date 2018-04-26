using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSounds : MonoBehaviour {

	void Start () {
		int i = 0;
		foreach (Button o in Resources.FindObjectsOfTypeAll<Button>() as Button[]) {
			i++;
			o.onClick.AddListener (() => Pressed ());
		}
		Debug.Log ("Got buttons: " + i);
	}

	void Pressed () {
		SoundManager.Instance.PlaySound ("menu-button");
	}
}