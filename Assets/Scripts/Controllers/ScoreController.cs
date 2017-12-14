using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController {

	public static LevelData levelData;

	static int margin;
	static int scoreMultiplier;
	static float timeBefore, timeAfter;
	static float deltaTime;
	static int score;

	public static void Awake () {
		levelData = GlobalData.Instance.GetActiveLevel ();

		Debug.Log ("Loaded level " + levelData.levelName);

		margin = levelData.multiplierLimit;
		scoreMultiplier = 1;
		timeBefore = timeAfter = Time.time;
	}

	public static void ScoreUpdate () {
		CalcScore2 ();
		if (score > GlobalData.Instance.highscore) {
			GlobalData.Instance.highscore = score;
			PlayerPrefs.SetInt ("Highscore", GlobalData.Instance.highscore);
		}

		//Update UI
		string s = "Score: " + score.ToString() + ", Highscore: " + GlobalData.Instance.highscore.ToString() + ", x " + scoreMultiplier.ToString() + ", margin " + margin.ToString();
		Debug.Log (s);
	}

	static void CalcScore2 () {
		if ((score >= margin) && (scoreMultiplier < levelData.multiplierLimit)) {
			margin = score * (levelData.multiplierLimit - scoreMultiplier);
			scoreMultiplier += 1;
		}
		score += scoreMultiplier;
	}

	public static int GetScore () {
		return score;
	}
	/*
	static void CalcScore () {
		//Timey-wimey magic
		timeBefore = timeAfter;
		timeAfter = Time.time;
		deltaTime = timeAfter - timeBefore;
		Debug.Log ("Delta time: " + deltaTime);
		//Calculate Score
		//Currently depends on how much points you have and how fast are you clicking
		if ((score >= margin) && (scoreMultiplier < levelData.multiplierLimit)) {
			margin = score * (levelData.multiplierLimit - scoreMultiplier);
			scoreMultiplier += 1;
		}
		if ((deltaTime > (1 / scoreMultiplier)) && (scoreMultiplier > 1)) {
			margin = score * (levelData.multiplierLimit - scoreMultiplier);
			scoreMultiplier -= 1;
		}
		score += (int)Mathf.Ceil (scoreMultiplier / deltaTime);

	}*/
}