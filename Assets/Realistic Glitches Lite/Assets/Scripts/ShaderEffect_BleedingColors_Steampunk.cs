using System.Collections;
using UnityEngine;
using System;

[ExecuteInEditMode]
public class ShaderEffect_BleedingColors_Steampunk : MonoBehaviour {

	public float intensity = 3;
	public float shift = 0.5f;
	private Material material;

	private System.Random random;

	private float iCurrent, iNext, i;
	private float vCurrent, vNext, v;

	// Creates a private material used to the effect
	void Awake ()
	{
		material = new Material( Shader.Find("Hidden/BleedingColorsSteampunk") );
	}

	// Postprocess the image
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		random = new System.Random ();

		i = Mathf.Lerp (iCurrent, iNext, Time.time);
		if (i == iNext) {
			iCurrent = iNext;
			iNext = (float)random.NextDouble ();
		}

		v = Mathf.Lerp (vCurrent, vNext, Time.time);
		if (v == vNext) {
			vCurrent = vNext;
			vNext = (float)random.NextDouble ();
		}

		material.SetFloat("_Intensity", intensity*i);
		material.SetFloat("_ValueX", shift*v);
		Graphics.Blit (source, destination, material);
	}
}
