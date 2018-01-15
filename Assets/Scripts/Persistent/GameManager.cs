using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public Process game;
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
		game = new Process ();
		scoreController = new ScoreController ();
		inputManager = new InputManager ();
	}

	public int Score {
		get { return Score; }
		private set { Score = value; }
	}
	public int Multiplier {
		get { return Multiplier; }
		private set { Multiplier = value; }
	}

	//states and actions

	public enum State
	{
		Inactive,
		Active,
		Paused,
		Terminated
	}

	public enum Action
	{
		Begin,
		End,
		Pause,
		Resume,
		Exit
	}

	//public actions for game state

	public void Pause () {
		if (game.MoveNext (Action.Pause) == State.Paused) {
			Debug.Log ("Game paused.");
		}
	}

	public void Resume () {
		if (game.MoveNext (Action.Resume) == State.Active) {
			Debug.Log ("Game resumed.");
		}
	}

	public void Play () {
		if (game.MoveNext (Action.Begin) == State.Active) {
			scoreController = new ScoreController ();
			scoreController.Set ();
			Debug.Log ("Game started.");
		}
	}

	public void Stop () {
		if (game.MoveNext (Action.End) == State.Inactive) {
			GlobalData.Instance.SaveGameData ();
			Debug.Log ("Game ended.");
		}
	}

	public void Quit () {
		if (game.MoveNext (Action.Exit) == State.Terminated) {
			GlobalData.Instance.SaveGameData ();
			Debug.Log ("Exiting game.");
		}
	}

	//cycle

	void Update () {
		if (game.CurrentState == State.Active) {
			inputManager.HandleInput ();
		}
	}

	//state machine

	public class Process {
		public class StateTransition {
			readonly State CurrentState;
			readonly Action Command;

			public StateTransition (State currentState, Action command) {
				CurrentState = currentState;
				Command = command;
			}

			public override int GetHashCode () {
				return 17 + 31 * CurrentState.GetHashCode () + 31 * Command.GetHashCode ();
			}

			public override bool Equals (object obj) {
				StateTransition other = obj as StateTransition;
				return other != null && this.CurrentState == other.CurrentState && this.Command == other.Command;
			}
		}

		Dictionary<StateTransition, State> transitions;
		public State CurrentState { get; private set; }

		public Process () {
			CurrentState = State.Inactive;
			transitions = new Dictionary<StateTransition, State> {
				{new StateTransition (State.Inactive, Action.Exit), State.Terminated},
				{new StateTransition (State.Inactive, Action.Begin), State.Active},
				{new StateTransition (State.Active, Action.End), State.Inactive},
				{new StateTransition (State.Active, Action.Pause), State.Paused},
				{new StateTransition (State.Paused, Action.End), State.Inactive},
				{new StateTransition (State.Paused, Action.Resume), State.Active}
			};
		}

		public State GetNext (Action command) {
			StateTransition transition = new StateTransition (CurrentState, command);
			State nextState;
			if (!transitions.TryGetValue (transition, out nextState))
				throw new UnityException ("Error! Invalid transition: " + CurrentState + " -> " + command);
			return nextState;
		}

		public State MoveNext (Action command) {
			CurrentState = GetNext (command);
			return CurrentState;
		}
	}
}