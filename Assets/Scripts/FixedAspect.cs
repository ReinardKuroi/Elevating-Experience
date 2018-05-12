using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
public class FixedAspect : MonoBehaviour {
	public Vector2 targetAspect = new Vector2 (1, 2);
	Camera _camera;

	void Start () {
		UpdateCrop ();
	}

	public void UpdateCrop () {
		_camera = GetComponent<Camera> ();
		float screenRatio = Screen.width / (float)Screen.height;
		float targetRatio = targetAspect.x / targetAspect.y;

		if (Mathf.Approximately (screenRatio, targetRatio)) {
			_camera.rect = new Rect (0, 0, 1, 1);
		} else if (screenRatio > targetRatio) {
			float normalizedWidth = targetRatio / screenRatio;
			float barThickness = (1f - normalizedWidth) / 2f;
			_camera.rect = new Rect (barThickness, 0, normalizedWidth, 1);
		} else {
			float normalizedHeight = screenRatio / targetRatio;
			float barThickness = (1f - normalizedHeight) / 2f;
			_camera.rect = new Rect (0, barThickness, 1, normalizedHeight);
		}
	}
}