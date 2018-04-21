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

	// Creates a private material used to the effect
	void Awake ()
	{
		material = new Material( Shader.Find("Hidden/TintTechnical") );
		random = new System.Random ();
	}

	// Postprocess the image
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		material.SetFloat("_ValueX", y*(float)random.NextDouble());
		material.SetFloat("_ValueY", u);
		material.SetFloat("_ValueZ", v);

		Graphics.Blit (source, destination, material);
	}
}