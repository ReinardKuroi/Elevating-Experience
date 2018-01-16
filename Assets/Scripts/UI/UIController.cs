using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public Text scoreTableText;
	public Text multiplierText;
	public Slider weightBar;

	private int score;

	// Use this for initialization
	void Start () {
		LevelData levelData = GlobalData.Instance.GetActiveLevelData ();
		score = 0;
		scoreTableText.text = "";
		multiplierText.text = "";
		weightBar.minValue = 0;
		weightBar.maxValue = levelData.multiplierDynamic;
		weightBar.value = 0;
	}
	
	// Update is called once per frame
	public void Update () {
		StartCoroutine(SlowRoll(GameManager.Instance.Score));
		scoreTableText.text = score.ToString ();
		multiplierText.text = "X" + GameManager.Instance.Multiplier.ToString ();
		weightBar.value = GameManager.Instance.Weightbar;
	}

	IEnumerator SlowRoll (int target) {
		while (this.score < target) {
			int diff = target - score;
			this.score += (int)Mathf.Sqrt (diff);
			yield return new WaitForSeconds (0.1f);
		}
	}
}