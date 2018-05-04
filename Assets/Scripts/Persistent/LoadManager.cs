using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour {

	public static LoadManager Instance { get; private set; }

//	private int depth = -1000;
	[Range(0, 1)]
	public float fadeSpeed = 0.8f;

	public GameObject prefab;


	void Awake () {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
	}

	void Start () {
		SoundManager.Instance.LevelMusic ("menu");
		LoadScene ("Login");
	}

	public void LoadScene (string name) {
		Debug.Log ("Loading scene: " + name);
		StartCoroutine (LoadNew (name));
	}
	//IMPLEMENT MORE FANCY STUFF HERE

	IEnumerator Fade (Direction fadeDirection) {
		yield return new WaitForEndOfFrame ();

		float alpha = 0f;
		float start = 0f;
		float target = 0f;

		if (true) {
		}

		if (fadeDirection == Direction.In) {
			start = 1f;
			target = 0f;
		} else if (fadeDirection == Direction.Out) {
			start = 0f;
			target = 1f;
		}

		alpha = start;

		while (alpha != target) {
			alpha = Mathf.Lerp (start, target, Time.deltaTime * fadeSpeed);
			
		}
	}

	IEnumerator LoadNew (string name) {
		yield return new WaitForEndOfFrame ();

		AsyncOperation ao = SceneManager.LoadSceneAsync (name);

		while (!ao.isDone) {
			yield return null;
		}
	}

	private enum Direction
	{
		In,
		Out
	};
}