using System.Collections;
using UnityEngine;

public class Color_Lerp : MonoBehaviour
{

	// Use this for initialization
	public Gradient [] colors;
	private Renderer R;
	float time;
	public float speed = 1.0f;
	int Ran;
	void Start ()
	{
		R = GetComponent<Renderer> ();
		Ran = Random.Range (0, 3);
	}

	// Update is called once per frame
	void Update ()
	{
		time += Time.deltaTime / 3;
		R.material.color = colors [Ran].Evaluate (Mathf.Clamp (Mathf.PingPong (time, 0.7f), 0f, 1f));

	}

}