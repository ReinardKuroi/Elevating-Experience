using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState : MonoBehaviour {

	public GameObject mainMenu;
	public List<GameObject> menuItems;

	void Awake () {
		mainMenu.SetActive (true);
		foreach (GameObject g in menuItems)
			g.SetActive (false);
	}
}