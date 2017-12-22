using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Engine : MonoBehaviour {

	//MAKE A PROPER GAME MANAGER ASSHOLE DONT BE A LAZY CUNT

	void Awake () {
//		GlobalData.Instance.SetActiveLevel ();
		ScoreController.Awake ();
	}

	void Update () {
		//fix later
		if (Time.timeScale == 0f)
			return;
		
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