using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSounds : MonoBehaviour {

	void Start () {
		int i = 0;
		foreach (Button o in Object.FindObjectsOfType<Button>()) {
			i++;
			o.onClick.AddListener (() => Debug.Log ("Got one!"));
		}
		Debug.Log ("Got buttons: " + i);
	}
}