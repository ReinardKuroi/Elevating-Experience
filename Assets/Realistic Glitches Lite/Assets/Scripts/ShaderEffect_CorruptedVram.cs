using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class ShaderEffect_CorruptedVram : MonoBehaviour {

	public float shift = 10;
	private Texture texture;
	private Material material;

	private System.Random random;

	void Awake ()
	{
		material = new Material( Shader.Find("Hidden/Distortion") );
		texture = Resources.Load<Texture>("Checkerboard-big");
	}
		
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		random = new System.Random ();

		material.SetFloat("_ValueX", shift);
		material.SetTexture("_Texture", texture);
		if (random.Next (1, 10000) > 9990) {
			Graphics.Blit (source, destination, material);
		} else {
			Graphics.Blit (source, destination);
		}
	}
}
