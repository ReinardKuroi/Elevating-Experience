using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraCanvasScaler : CanvasScaler {
	public const float kLogBase = 2;

	private Canvas _Canvas;

	protected override void OnEnable() {
		_Canvas = GetComponent<Canvas>();
		base.OnEnable();
	}

	protected override void HandleScaleWithScreenSize() {
		Vector2 screenSize = new Vector2(Screen.width, Screen.height);
		if (_Canvas.renderMode == RenderMode.ScreenSpaceCamera && _Canvas.worldCamera != null) {
			screenSize.x *= _Canvas.worldCamera.rect.width;
			screenSize.y *= _Canvas.worldCamera.rect.height;
		}

		float scaleFactor = 0;
		switch (m_ScreenMatchMode) {
		case ScreenMatchMode.MatchWidthOrHeight:
			{
				float logWidth = Mathf.Log(screenSize.x / m_ReferenceResolution.x, kLogBase);
				float logHeight = Mathf.Log(screenSize.y / m_ReferenceResolution.y, kLogBase);
				float logWeightedAverage = Mathf.Lerp(logWidth, logHeight, m_MatchWidthOrHeight);
				scaleFactor = Mathf.Pow(kLogBase, logWeightedAverage);
				break;}
		case ScreenMatchMode.Expand:
			{
				scaleFactor = Mathf.Min(screenSize.x / m_ReferenceResolution.x, screenSize.y / m_ReferenceResolution.y);
				break;}
		case ScreenMatchMode.Shrink:
			{
				scaleFactor = Mathf.Max(screenSize.x / m_ReferenceResolution.x, screenSize.y / m_ReferenceResolution.y);
				break;}
		}

		SetScaleFactor(scaleFactor);
		SetReferencePixelsPerUnit(m_ReferencePixelsPerUnit);
	}
}