using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutterController : MonoBehaviour {

	public float speed;

	void Update () {
		gameObject.transform.Rotate (Vector3.left, speed * Time.deltaTime);
	}
}