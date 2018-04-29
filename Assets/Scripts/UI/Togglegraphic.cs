using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]

public class Togglegraphic : MonoBehaviour {

	public Sprite toggleOn, toggleOff;

	private bool isOn;
	private Image image;

	void Start () {
		image = transform.Find ("Background").gameObject.GetComponent<Image> ();
		image.type = Image.Type.Simple;
		image.preserveAspect = true;
		ToggleGraphic ();
	}

	public void ToggleGraphic () {
		if (image != null) {
			image.sprite = gameObject.GetComponent<Toggle> ().isOn ? toggleOn : toggleOff;
		}
	}

	public void OnClick () {
		ToggleGraphic ();
		SoundManager.Instance.PlaySound ("menu-button");
	}
}