using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCOptimization : MonoBehaviour {

	public Camera PCCamera;
	public Camera androidCamera;

	void Awake () {
		if (Application.platform == RuntimePlatform.Android) {
			PCCamera.gameObject.SetActive (false);
			androidCamera.gameObject.SetActive (true);
		} else {
			PCCamera.gameObject.SetActive (true);
			androidCamera.gameObject.SetActive (false);
		}
	}

}