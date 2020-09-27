using System.Collections;
using UnityEngine;

public class GamePlay_AI : MonoBehaviour
{

	// Use this for initialization
	public GameObject Cube;
	public float rotateTime = 3.0f;
	public float rotateDegrees = 90.0f;
	public bool rotating = false;
	public bool Founded = false;
	public bool landed = false;

	Vector3 [] Random_Dir = new Vector3 [8];
	int a;
	void Start ()
	{

		Random_Dir [0] = -Vector3.forward;
		Random_Dir [1] = Vector3.left;
		StartCoroutine ("Hold_Rotate");

	}

	IEnumerator Auto_Rotate ()
	{
		while (true)
		{
			if (!Founded)
			{
				a = Random.Range (0, 2);
				StartCoroutine (Rotate_obj (transform, Cube.transform, Random_Dir [a], rotateDegrees, rotateTime));
			}
			if (Founded)
			{
				StartCoroutine (Rotate_obj (transform, Cube.transform, Vector3.up, rotateDegrees, rotateTime));
			}
			yield return new WaitForSeconds (1);

		}

	}

	IEnumerator Rotate_obj (Transform thisTransform, Transform otherTransform, Vector3 rotateAxis, float degrees, float totalTime)
	{
		if (rotating)
		{
			yield break;
		}
		rotating = true;

		Quaternion startRotation = thisTransform.rotation;
		//Vector3 startPosition = thisTransform.position;
		transform.RotateAround (otherTransform.position, rotateAxis, degrees);
		Quaternion endRotation = thisTransform.rotation;
		//Vector3 endPosition = thisTransform.position;
		thisTransform.rotation = startRotation;
		//thisTransform.position = startPosition;

		float rate = degrees / totalTime;
		for (float i = 0.0f; i < degrees; i += Time.deltaTime * rate)
		{
			yield return null;
			thisTransform.RotateAround (otherTransform.position, rotateAxis, Time.deltaTime * rate);
		}

		thisTransform.rotation = endRotation;
		//thisTransform.position = endPosition;
		rotating = false;

	}

	IEnumerator Hold_Rotate ()
	{
		yield return new WaitForSeconds (2f);
		StartCoroutine ("Auto_Rotate");
	}

}