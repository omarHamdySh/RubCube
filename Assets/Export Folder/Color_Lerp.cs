using System.Collections;
using UnityEngine;

public class Color_Lerp : MonoBehaviour
{

	// Use this for initialization
	public Gradient [] colors;
	private Renderer R;
	float time;
	public float speed = 1.0f;
	public int PaletNumber = 0;
	FaceBlock _faceblock;
	Color_Palet Palet;
	int Ran;
	void Start ()
	{
		_faceblock = GetComponent<FaceBlock> ();
		Palet = _faceblock.faceBlockContainer.parentFaceBlocksRow.parentFace.parentCube.color_Swap_Manager.Color_Paltes [PaletNumber];
		R = GetComponent<Renderer> ();
		Ran = Random.Range (0, Palet.Color_Paltes.Count);
	}

	// Update is called once per frame
	void Update ()
	{
		time += Time.deltaTime / 3;
		R.material.color = Palet.Color_Paltes [Ran].Evaluate (Mathf.Clamp (Mathf.PingPong (time, 0.7f), 0f, 1f));

	}

}