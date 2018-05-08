using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmissionPulse : MonoBehaviour {

	private Material mat;

	void Start () {
		mat = gameObject.GetComponent<Renderer> ().material;
	}

	void Update () {
		Color color = Color.Lerp (Color.red, Color.blue, Mathf.PingPong (Time.time, 1));
		mat.SetColor ("_EmissionColor", color);
	}
}