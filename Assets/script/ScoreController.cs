using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreController : MonoBehaviour {

	const int MULTI = 5;
	const int CLICK = 1;

	static int margin;
	static int scoreMultiplier;
	static float timeBefore, timeAfter;
	static float deltaTime;

	void start () {
		//if
		GlobalData.Instance.score = 0;
		margin = MULTI;
		scoreMultiplier = 1;
		timeBefore = timeAfter = Time.time;
	}

	public static void ScoreUpdate () {
		//Timey-wimey magic
		timeBefore = timeAfter;
		timeAfter = Time.time;
		deltaTime = timeAfter - timeBefore;
		Debug.Log ("Delta time: " + deltaTime);
		//Calculate Score
		//Currently depends on how much points you have and how fast are you clicking
		if ((GlobalData.Instance.score >= margin) && (scoreMultiplier < MULTI)) {
			margin = GlobalData.Instance.score * (MULTI - scoreMultiplier);
			scoreMultiplier += 1;
		}
		if ((deltaTime > (1 / scoreMultiplier)) && (scoreMultiplier > 1)) {
			margin = GlobalData.Instance.score * (MULTI - scoreMultiplier);
			scoreMultiplier -= 1;
		}
		GlobalData.Instance.score += (int)Mathf.Ceil (scoreMultiplier / deltaTime);

		if (GlobalData.Instance.score > GlobalData.Instance.highscore) {
			PlayerPrefs.SetInt ("Highscore", GlobalData.Instance.highscore);
			GlobalData.Instance.highscore = GlobalData.Instance.score;
		}

		//Update UI
		string s = "Score: " + GlobalData.Instance.score.ToString() + ", x " + scoreMultiplier.ToString() + ", margin " + margin.ToString();
		Debug.Log (s);
	}
}
