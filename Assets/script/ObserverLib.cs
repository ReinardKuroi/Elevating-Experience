using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public abstract class Observer {
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

	int Score;
	int Margin;
	int ScoreMultiplier;
	Text ScoreTableText;

	public ScoreObserver (GameObject ScoreTable, int Score, int Margin, int ScoreMultiplier) {
		this.Score = Score;
		this.Margin = Margin;
		this.ScoreMultiplier = ScoreMultiplier;
		this.ScoreTableText = ScoreTable.GetComponent<Text> ();
		ScoreTableText.text = "";
	}

	public override void OnNotify () {
		CalcScore ();
		PrintScore ();
		SaveScore ();
	}

	void CalcScore () {
		if ((Score >= Margin) && (ScoreMultiplier <= 10)) {
			Margin = Score * (10 - ScoreMultiplier);
			ScoreMultiplier += 1;
		}
		Score += ScoreMultiplier;
	}

	void PrintScore () {
		string s = "Score: " + Score + ", x " + ScoreMultiplier;
		Debug.Log (s);
		ScoreTableText.text = s;
	}

	void SaveScore () {
		
	}
}