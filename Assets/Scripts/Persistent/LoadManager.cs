using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadManager : MonoBehaviour {

	public static LoadManager Instance { get; private set; }

	public Slider loadingSlider;

	void Awake () {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
	}

	void Start () {
		LoadScene ("Login");
	}

	public void LoadScene (string name) {
		Debug.Log ("Loading scene: " + name);
		StartCoroutine (LoadNew (name));
	}

	//IMPLEMENT MORE FANCY STUFF HERE

	IEnumerator LoadNew (string name) {
		yield return new WaitForEndOfFrame ();

		GameObject fader = gameObject.transform.GetChild (0).gameObject;
		fader.SetActive (true);
		loadingSlider.value = 0f;
		AnimationController.PlayAnimation (fader, "FaderCloseLinear");
		SoundManager.Instance.PlaySound ("elevator-close");

		yield return new WaitForSeconds (1.5f);

		AsyncOperation ao = SceneManager.LoadSceneAsync (name);

		while (!ao.isDone) {
			loadingSlider.value = Mathf.Clamp01 (ao.progress / 0.9f);
			yield return null;
		}

		yield return new WaitForSeconds (0.5f);

		AnimationController.PlayAnimation (fader, "FaderOpenLinear");
		SoundManager.Instance.PlaySound ("elevator-open");
	}
}