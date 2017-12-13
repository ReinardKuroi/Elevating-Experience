using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			GameObject g = Cast();
			if (g) {
				ObjectController o = g.GetComponent<ObjectController> ();
				if (o)
					o.OnClick ();
			}
		}
	}

	GameObject Cast () {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
			if (Physics.Raycast (ray, out hit) && hit.collider.gameObject) {
				return hit.collider.gameObject;
			}
		return null;
     }
}