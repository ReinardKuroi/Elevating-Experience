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

	const int MULTI = 5;
	const int CLICK = 1;

	int score;
	int margin;
	int scoreMultiplier;
	Text scoreTableText;
	float timeBefore, timeAfter;
	float deltaTime;


	//Initialize
	public ScoreObserver (GameObject ScoreTable, int Score, int Margin, int ScoreMultiplier) {
		this.score = Score;
		this.margin = Margin;
		this.scoreMultiplier = ScoreMultiplier;
		this.scoreTableText = ScoreTable.GetComponent<Text> ();
		scoreTableText.text = "";
		timeBefore = timeAfter = Time.time;
	}


	//Invoked when observer is triggered
	public override void OnNotify () {

		//Timey-wimey magic
		timeBefore = timeAfter;
		timeAfter = Time.time;
		deltaTime = timeAfter - timeBefore;

		CalcScore ();
		PrintScore ();
		SaveScore ();

		Debug.Log ("Delta time: " + deltaTime);
	}


	//Calculates current score basedon different parameters
	void CalcScore () {
		//Currently depends on how much points you have and how fast are you clicking
		if ((score >= margin) && (scoreMultiplier < MULTI)) {
			margin = score + score * (MULTI - scoreMultiplier);
			scoreMultiplier += 1;
		}
		if ((deltaTime > (MULTI / scoreMultiplier)) && (scoreMultiplier > 1)) {
			margin =  score + score * (MULTI / scoreMultiplier);
			scoreMultiplier -= 1;
		}
		score += (int)Mathf.Ceil (scoreMultiplier / deltaTime);
	}


	//Updates the score parameter of the UI
	void PrintScore () {
		string s = "Score: " + score + ", x " + scoreMultiplier + ", margin " + margin;
		Debug.Log (s);
		scoreTableText.text = s;
	}


	//Writes Your score into a file and saves it
	void SaveScore () {
		
	}
}