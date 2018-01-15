using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour {

	private LevelData levelData;
	private System.Random systemRandom;

	public int Score {
		get { return Score; }
		private set { Score = value; }
	}
	public int Multiplier {
		get { return Multiplier; }
		private set { Multiplier = value; }
	}

	public float Weightbar {
		get { return Weightbar; }
		private set { Weightbar = value; }
	}

	public void Set () {
		Score = 0;
		Multiplier = 1;
		Weightbar = 0f;
		levelData = GlobalData.Instance.GetActiveLevelData ();
		systemRandom = new System.Random (System.DateTime.Now.Millisecond);
		StartCoroutine (Accumulate ());
	}

	private IEnumerator Accumulate () {
		while (GameManager.Instance.game.CurrentState == GameManager.State.Active) {
			if (Weightbar > 0f)
				Weightbar -= Time.deltaTime * levelData.clickDecay;
			if (Weightbar >= levelData.multiplierDynamic && Multiplier < levelData.multiplierLimit) {
				Multiplier += 1;
				Weightbar = 0f;
			}
			if (Weightbar <= 0f && Multiplier > 1)
				Multiplier -= 1;
			Debug.Log ("Weight: " + Weightbar.ToString () + ", Score: " + Score.ToString () + ", Multiplier: " + Multiplier.ToString ());
			yield return new WaitForEndOfFrame ();
		}
	}

	public void OnClick () {
		if (Weightbar < levelData.multiplierDynamic)
			Weightbar += levelData.clickWeight;
		if (Crit ())
			Score += Multiplier * levelData.critMultiplier;
		else
			Score += Multiplier;
	}

	private bool Crit () {
		return (levelData.critChance > systemRandom.Next (1, 100));
	}
}