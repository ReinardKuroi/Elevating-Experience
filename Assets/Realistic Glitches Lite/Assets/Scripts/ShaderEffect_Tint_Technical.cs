using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderEffect_Tint_Technical : MonoBehaviour {

	private float y = 1;
	private float u = 1;
	private float v = 1;
	//	public bool swapUV = false;
	private Material material;
	private System.Random random;
	[Range(0, 1)]
	public float speed = 1f;
	private float before;
	private float after;
	private float change;

	// Creates a private material used to the effect
	void Awake ()
	{
		material = new Material( Shader.Find("Hidden/TintTechnical") );
		random = new System.Random ();
		before = 0f;
		after = 1f;
		change = before;
	}

	// Postprocess the image
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		if (change >= after) {
			before = after;
			after = (float)random.NextDouble ();
		}
		change = Mathf.Lerp (before, after, Time.deltaTime / speed);
		material.SetFloat("_ValueX", y*(0.9f+change/10f));
		material.SetFloat("_ValueY", u);
		material.SetFloat("_ValueZ", v);

		Graphics.Blit (source, destination, material);
	}
}