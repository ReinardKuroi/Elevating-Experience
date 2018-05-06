using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour {

	public static LoadManager Instance { get; private set; }

	public GameObject prefab;

	private GameObject fader;
	private Animator animator;

	void Awake () {
		if (Instance == null) {
			Instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}
	}

	void Start () {
		fader = GameObject.Instantiate (prefab);
		fader.SetActive (false);
		animator = fader.GetComponent<Animator> ();
		SoundManager.Instance.LevelMusic ("menu");
		LoadScene ("Login");
	}

	public void LoadScene (string name) {
		Debug.Log ("Loading scene: " + name);
		StartCoroutine (LoadNew (name));
	}
	//IMPLEMENT MORE FANCY STUFF HERE

	void FaderOpen () {
		animator.GetParameter (0);
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