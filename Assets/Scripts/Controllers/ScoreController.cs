using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour {

	private LevelData levelData;
	private System.Random systemRandom;
	private Subject scoreObserver;

	private int _score;
	private int _multiplier;
	private float _weightbar;

	public int Score {
		get { return _score; }
		private set { _score = value; }
	}

	public int Multiplier {
		get { return _multiplier; }
		private set { _multiplier = value; }
	}

	public float Weightbar {
		get { return _weightbar; }
		private set { _weightbar = value; }
	}

	public void Set () {
		_score = 0;
		_multiplier = 1;
		_weightbar = 0f;
		levelData = GlobalData.Instance.ActiveLevelData;
		systemRandom = new System.Random (System.DateTime.Now.Millisecond);
		scoreObserver = new Subject ();
		scoreObserver.AddObserver (new Highscore ());
		StartCoroutine (Accumulate ());
	}

	private IEnumerator Accumulate () {
		FSM.State gamestate;
		do {
			gamestate = GameManager.Instance.GetState ();
			if (gamestate == FSM.State.Active) {
				if (_weightbar > 0f)
					_weightbar -= Time.deltaTime * (levelData.clickDecay + _multiplier / levelData.clickDecay);
				if (_weightbar >= levelData.multiplierDynamic && _multiplier < levelData.multiplierLimit) {
					_multiplier += 1;
					_weightbar = 0.5f;
				}
				if (_weightbar <= 0f && _multiplier > 1) {
					_multiplier -= 1;
					_weightbar = levelData.multiplierDynamic - 0.5f;
				}
			}
			yield return new WaitForEndOfFrame ();
		} while ((gamestate == FSM.State.Active) || (gamestate == FSM.State.Paused));
	}

	public void OnClick () {
		if (_weightbar < levelData.multiplierDynamic)
			_weightbar += levelData.clickWeight;
		if (Crit ())
			_score += _multiplier * levelData.critMultiplier;
		else
			_score += _multiplier;
		scoreObserver.Notify ();
	}

	private bool Crit () {
		return (levelData.critChance > systemRandom.Next (1, 100));
	}

	private class Highscore : Observer {

		private delegate void DelegateVoid ();
		private DelegateVoid scoreUpdate;

		public Highscore () {
			scoreUpdate = GameManager.Instance.SetHighscore;
		}

		public override void OnNotify ()
		{
			scoreUpdate ();
		}
	}
}