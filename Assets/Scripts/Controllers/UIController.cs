	using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public Text scoreTableText;
	public Text highscoreText;

	private int score;

	// Use this for initialization
	void Start () {
		scoreTableText.text = "";
		highscoreText.text = "";
	}
	
	// Update is called once per frame
	public void Update () {
		StartCoroutine(SlowRoll( ScoreController.GetScore ()));
		scoreTableText.text = "Score: " + score;
		highscoreText.text = "Highscore: " + GlobalData.Instance.highscore.ToString ();
	}

	IEnumerator SlowRoll (int target) {
		while (this.score < target) {
			int diff = target - score;
			this.score += (int)Mathf.Sqrt (diff);
			yield return new WaitForSeconds(0.1f);
		}
	}

}