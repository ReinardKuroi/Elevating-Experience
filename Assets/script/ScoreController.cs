using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreController : MonoBehaviour {

	const int MULTI = 5;
	const int CLICK = 1;

	int score;
	int margin;
	int scoreMultiplier;
	Text scoreTableText;
	float timeBefore, timeAfter;
	float deltaTime;

	void start () {
		score = 0;
		margin = MULTI;
		scoreMultiplier = 1;
		scoreTableText.text = "";
		timeBefore = timeAfter = Time.time;
	}

	public void ScoreUpdate () {
		//Timey-wimey magic
		timeBefore = timeAfter;
		timeAfter = Time.time;
		deltaTime = timeAfter - timeBefore;
		Debug.Log ("Delta time: " + deltaTime);
		//Calculate Score
		//Currently depends on how much points you have and how fast are you clicking
		if ((score >= margin) && (scoreMultiplier < MULTI)) {
			margin = score * (MULTI - scoreMultiplier);
			scoreMultiplier += 1;
		}
		if ((deltaTime > (1 / scoreMultiplier)) && (scoreMultiplier > 1)) {
			margin = score * (MULTI - scoreMultiplier);
			scoreMultiplier -= 1;
		}
		score += (int)Mathf.Ceil (scoreMultiplier / deltaTime);

		//Update UI
		string s = "Score: " + score.ToString() + ", x " + scoreMultiplier.ToString() + ", margin " + margin.ToString();
		Debug.Log (s);
		scoreTableText.text = s;
	}
}
