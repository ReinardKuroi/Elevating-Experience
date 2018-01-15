using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager {

	public InputManager () { }

	public abstract class Command {
		public abstract void Execute ();
	}

	private Command LeftClick;

	void Start () {
		LeftClick = new Click ();
	}

	public void HandleInput () {
		if (Input.GetKeyDown (KeyCode.Mouse0))
			LeftClick.Execute ();
	}

	public class Click : Command {
		public override void Execute () {
			GameObject gameObject;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit) && hit.collider.gameObject)
				gameObject = hit.collider.gameObject;
			else
				gameObject = null;

			if (gameObject) {
				ObjectController objectController = gameObject.GetComponent<ObjectController> ();
				if (objectController)
					objectController.OnClick ();
			}
		}
	}

	public class NullCommand : Command {
		public override void Execute () {
			;
		}
	}
}