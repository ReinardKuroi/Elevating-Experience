using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController {
	public static void PlayAnimation (GameObject g, string s) {
		Animator anim = g.GetComponent<Animator> ();
		anim.Play (s);
	}
}