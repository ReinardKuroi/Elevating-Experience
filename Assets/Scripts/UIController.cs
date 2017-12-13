using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public Text scoreTableText;
	public Text highscoreText;

	// Use this for initialization
	void Start () {
		scoreTableText.text = "";
		highscoreText.text = "";
	}
	
	// Update is called once per frame
	public void Update () {
		scoreTableText.text = "Score: " + GlobalData.Instance.score.ToString ();
		highscoreText.text = "Highscore: " + GlobalData.Instance.highscore.ToString ();
	}
}