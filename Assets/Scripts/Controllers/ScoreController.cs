using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour {

	private LevelData levelData;
	private System.Random systemRandom;

	public int score;
	public int multiplier;
	public float weightbar;

	public void Set () {
		score = 0;
		multiplier = 1;
		weightbar = 0f;
		levelData = GlobalData.Instance.GetActiveLevelData ();
		systemRandom = new System.Random (System.DateTime.Now.Millisecond);
		StartCoroutine (Accumulate ());
	}

	private IEnumerator Accumulate () {
		FSM.State gamestate;
		do {
			gamestate = GameManager.Instance.GetState ();
			if (gamestate == FSM.State.Active) {
				if (weightbar > 0f)
					weightbar -= Time.deltaTime * (levelData.clickDecay + multiplier / levelData.clickDecay);
				if (weightbar >= levelData.multiplierDynamic && multiplier < levelData.multiplierLimit) {
					multiplier += 1;
					weightbar = 0.5f;
				}
				if (weightbar <= 0f && multiplier > 1) {
					multiplier -= 1;
					weightbar = levelData.multiplierDynamic - 0.5f;
				}
			}
			yield return new WaitForEndOfFrame ();
		} while ((gamestate == FSM.State.Active) || (gamestate == FSM.State.Paused));
	}

	public void OnClick () {
		if (weightbar < levelData.multiplierDynamic)
			weightbar += levelData.clickWeight;
		if (Crit ())
			score += multiplier * levelData.critMultiplier;
		else
			score += multiplier;
	}

	private bool Crit () {
		return (levelData.critChance > systemRandom.Next (1, 100));
	}
}