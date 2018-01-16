using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM {

	//states and actions

	public enum State
	{
		Inactive,
		Active,
		Paused,
		Transition,
		Loading,
		Terminated
	}

	public enum Action
	{
		Begin,
		End,
		Pause,
		Resume,
		Load,
		Transition,
		Exit
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