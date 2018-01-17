using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public FSM.Process game;
	[HideInInspector]
	public ScoreController scoreController;
	InputManager inputManager;

	//Singleton
	public static GameManager Instance { get; private set; }
	void Awake () {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
		if (game == null)
			game = new FSM.Process ();
		if (inputManager == null)
			inputManager = new InputManager ();
	}

	public int Score {
		get { return scoreController.Score; }
	}

	public int Multiplier {
		get { return scoreController.Multiplier; }
	}

	public float Weightbar {
		get { return scoreController.Weightbar; }
	}

	//highscore code

	public void SetHighscore () {
		if (Score > GlobalData.Instance.Highscore)
			GlobalData.Instance.Highscore = Score;
	}

	//public actions for game state

	public void Pause () {
		if (game.MoveNext (FSM.Action.Pause) == FSM.State.Paused) {
			Debug.Log ("Game paused.");
		}
	}

	public void Resume () {
		if (game.MoveNext (FSM.Action.Resume) == FSM.State.Active) {
			Debug.Log ("Game resumed.");
		}
	}

	public void Play () {
		if (game.MoveNext (FSM.Action.Begin) == FSM.State.Active) {
			if (scoreController == null)
				scoreController = gameObject.AddComponent<ScoreController> () as ScoreController;
			else
				scoreController = gameObject.GetComponent<ScoreController> ();
			scoreController.Set ();
			Debug.Log ("Game started.");
		}
	}

	public void Stop () {
		if (game.MoveNext (FSM.Action.End) == FSM.State.Inactive) {
			if (scoreController)
				Destroy (scoreController);
			GlobalData.Instance.SaveGameData ();
			Debug.Log ("Game ended.");
		}
	}

	public void Quit () {
		if (game.MoveNext (FSM.Action.Exit) == FSM.State.Terminated) {
			GlobalData.Instance.SaveGameData ();
			Debug.Log ("Exiting game.");
		}
	}

	public FSM.State GetState () {
		return game.CurrentState;
	}

	//cycle

	void Update () {
		if (game.CurrentState == FSM.State.Active) {
			inputManager.HandleInput ();
		}
	}
}