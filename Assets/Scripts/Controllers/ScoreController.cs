using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour {

	public static LevelData levelData;

	static int scoreMultiplier = 1;
	static float multiplierCounter = 0;

	static float timeBefore;
	static float timeAfter;
	static float deltaTime;
	static System.Random rand;

	public static void Awake () {
		rand = new System.Random (System.DateTime.Now.Millisecond);
		levelData = GlobalData.Instance.GetActiveLevel ();
		Debug.Log ("Loaded level data: " + levelData.levelName);
		timeBefore = timeAfter = Time.time;
	}

	public static int ScoreUpdate (int score) {
		score = CalcScore (score);
		Debug.Log ("Score: " + score.ToString() + ", x " + scoreMultiplier.ToString());
		return score;
	}

	static void Accumulate () {
		timeBefore = timeAfter;
		timeAfter = Time.time;
		deltaTime = timeAfter - timeBefore;
		if (deltaTime <= levelData.clickDecay / scoreMultiplier)
			multiplierCounter += levelData.clickWeight;
		else
			multiplierCounter -= levelData.clickWeight;
		if ((multiplierCounter >= levelData.multiplierDynamic) && (scoreMultiplier < levelData.multiplierLimit)) {
			scoreMultiplier += 1;
			multiplierCounter = 0;
		}
		if ((multiplierCounter < 0) && (scoreMultiplier > 1)) {
			scoreMultiplier -= 1;
			multiplierCounter = levelData.multiplierDynamic - 1;
		}
	}

	static bool Crit () {
		if (levelData.critChance > rand.Next (1, 100))
			return true;
		else
			return false;
	}

	static int CalcScore (int score) {
		Accumulate ();
		if (Crit ()) {
			score += levelData.critMultiplier * scoreMultiplier;
			Debug.Log ("Crit!");
		}
		else
			score += scoreMultiplier;
		return score;
	}

	//for debugging
	static int CalcScore2 (int score) {
		score += 1;
		return score;
	}
}