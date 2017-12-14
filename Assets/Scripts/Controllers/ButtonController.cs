using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : ObjectController {

	void Start () {
		
	}
	//Invoked when the object is clicked
	public override void OnClick() {
		AnimationController.PlayAnimation (gameObject, "Pressed");
		SoundController.PlaySound ();
		ScoreController.ScoreUpdate ();
	}
}