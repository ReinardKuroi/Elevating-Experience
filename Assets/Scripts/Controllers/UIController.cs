	using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public Text scoreTableText;
	public Text multiplierText;

	private int score;

	// Use this for initialization
	void Start () {
		score = 0;
		scoreTableText.text = "";
		multiplierText.text = "";
	}
	
	// Update is called once per frame
	public void Update () {
		StartCoroutine(SlowRoll( GlobalData.Instance.score));
		scoreTableText.text = score.ToString ();
		multiplierText.text = "X" + GlobalData.Instance.multiplier.ToString ();
	}

	IEnumerator SlowRoll (int target) {
		while (this.score < target) {
			int diff = target - score;
			this.score += (int)Mathf.Sqrt (diff);
			yield return new WaitForSeconds(0.1f);
		}
	}

}