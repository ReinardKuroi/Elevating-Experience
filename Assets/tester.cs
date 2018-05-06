using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tester : MonoBehaviour {

	void Start () {
		Text text = gameObject.GetComponent<Text> ();
		string s = "";
		SaveLoad.LoadFromAssetsTest (ref s, GlobalData.levelDataFilename);
		text.text = s;
	}
}