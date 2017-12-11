using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			GameObject g = ClickSelect();

			if (g) {
				ObjectController o = g.GetComponent<ObjectController> ();

				if (o)
					o.OnClick ();
			}
		}
	}


	//Raycast detector
	GameObject ClickSelect() {
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100) && hit.collider.gameObject) {
				return hit.collider.gameObject;
			}
		return null;
     }

}
