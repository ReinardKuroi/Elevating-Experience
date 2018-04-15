using System.Collections;
using UnityEngine;
using System;

[ExecuteInEditMode]
public class ShaderEffect_BleedingColors : MonoBehaviour {

	public float intensity = 3;
	public float shift = 0.5f;
	private Material material;

	private System.Random random;
	private float intShift;
	private float valShift;

	// Creates a private material used to the effect
	void Awake ()
	{
		material = new Material( Shader.Find("Hidden/BleedingColors") );
	}

	// Postprocess the image
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		random = new System.Random ();

		intShift = (float)random.NextDouble ();
		valShift = (float)random.NextDouble ();

		if (intShift < 0.5f) { intShift = 0.5f; }
		if (valShift < 0.3f) { valShift = 0.3f; }
		material.SetFloat("_Intensity", intensity*intShift);
		material.SetFloat("_ValueX", shift*valShift);
		Graphics.Blit (source, destination, material);
	}
}
