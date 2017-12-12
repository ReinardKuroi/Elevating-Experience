using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {

	Subject subject = new Subject ();
	public GameObject ScoreTable;

	void Start () {
		ScoreObserver score = new ScoreObserver ();

		subject.AddObserver (score);
	}


	//Invoked when the object is clicked
	public void OnClick() {
		Animator anim = GetComponent<Animator> ();
		anim.Play ("Pressed");
		subject.Notify();
	}
}
