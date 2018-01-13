using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager Instance { get; private set; }
	public Process game;

	private LevelData levelData;
	private int currentScore;
	private int currentMutliplier;
	private System.Random randomNumber;

	void Awake () {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
		game = new Process ();
	}

	//states and commands

	public enum State
	{
		Inactive,
		Active,
		Paused,
		Terminated
	}

	public enum Command
	{
		Begin,
		End,
		Pause,
		Resume,
		Exit
	}

	//public commands for game state

	public void Pause () {
		if (game.MoveNext (Command.Pause) == State.Paused) {
			Debug.Log ("Game paused.");
		}
	}

	public void Resume () {
		if (game.MoveNext (Command.Resume) == State.Active) {
			Debug.Log ("Game resumed.");
		}
	}

	public void Play () {
		if (game.MoveNext (Command.Begin) == State.Active) {
			currentScore = 0;
			currentMutliplier = 1;
			levelData = GlobalData.Instance.GetActiveLevelData ();
			randomNumber = new System.Random (System.DateTime.Now.Millisecond);
			Debug.Log ("Game started.");
		}
	}

	public void Stop () {
		if (game.MoveNext (Command.End) == State.Inactive) {
			GlobalData.Instance.SaveGameData ();
			Debug.Log ("Game ended.");
		}
	}

	public void Quit () {
		if (game.MoveNext (Command.Exit) == State.Terminated) {
			GlobalData.Instance.SaveGameData ();
			Debug.Log ("Exiting game.");
		}
	}

	//game engine

	void Update () {
		if (game.CurrentState == State.Active) {
			//Code for the game itself
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				GameObject g = Cast();
				if (g) {
					ObjectController o = g.GetComponent<ObjectController> ();
					if (o)
						o.OnClick ();
				}
			}
		}
	}

	//misc

	public int Score () {
		return currentScore;
	}

	public int Multiplier () {
		return currentMutliplier;
	}

	bool Crit () {
		return (levelData.critChance > randomNumber.Next (1, 100));
	}

	GameObject Cast () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit) && hit.collider.gameObject) {
			return hit.collider.gameObject;
		}
		return null;
	}

	//state machine

	public class Process {
		public class StateTransition {
			readonly State CurrentState;
			readonly Command Command;

			public StateTransition (State currentState, Command command) {
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
				{new StateTransition (State.Inactive, Command.Exit), State.Terminated},
				{new StateTransition (State.Inactive, Command.Begin), State.Active},
				{new StateTransition (State.Active, Command.End), State.Inactive},
				{new StateTransition (State.Active, Command.Pause), State.Paused},
				{new StateTransition (State.Paused, Command.End), State.Inactive},
				{new StateTransition (State.Paused, Command.Resume), State.Active}
			};
		}

		public State GetNext (Command command) {
			StateTransition transition = new StateTransition (CurrentState, command);
			State nextState;
			if (!transitions.TryGetValue (transition, out nextState))
				throw new UnityException ("Error! Invalid transition: " + CurrentState + " -> " + command);
			return nextState;
		}

		public State MoveNext (Command command) {
			CurrentState = GetNext (command);
			return CurrentState;
		}
	}
}