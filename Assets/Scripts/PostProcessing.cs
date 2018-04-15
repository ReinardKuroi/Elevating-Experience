using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PostProcessing : MonoBehaviour {

	public Material mat;

	void OnRenderImage (RenderTexture source, RenderTexture dest) {
		Graphics.Blit (source, dest, mat);
	}
}