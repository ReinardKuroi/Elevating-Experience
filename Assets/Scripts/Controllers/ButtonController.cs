using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : ObjectController {

	//Invoked when the object is clicked
	public override void OnClick() {
		AnimationController.PlayAnimation (gameObject, "Idle");
		AnimationController.PlayAnimation (gameObject, "Pressed");
		SoundManager.Instance.PlaySound (GlobalData.Instance.ActiveLevelData.levelName + "-button");
		GameManager.Instance.scoreController.OnClick ();
	}
}