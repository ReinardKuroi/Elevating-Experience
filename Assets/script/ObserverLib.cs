using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class Observer{
	public abstract void OnNotify ();
}

public class Subject {
	List<Observer> observers = new List<Observer> ();

	public void Notify () {
		for (int i = 0; i < observers.Count;  i++) {
			observers [i].OnNotify ();
		}
	}

	public void AddObserver (Observer observer) {
		observers.Add (observer);
	}

	public void RemoveObserver (Observer observer) {
		observers.Remove (observer);
	}
}

public class ScoreObserver : Observer {


	//Initialize
	public ScoreObserver () {
	}


	//Invoked when observer is triggered
	public override void OnNotify () {
		ScoreController.ScoreUpdate ();
	}
}