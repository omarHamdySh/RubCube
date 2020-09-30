using System.Collections;
using EZCameraShake;
using UnityEngine;
using UnityEngine.Events;

public class ContinuousExplosionForce : MonoBehaviour
{
	public float force = 10000.0f;
	public float radius = 5.0f;
	public float upwardsModifier = 0.0f;
	public ForceMode forceMode;
	GameObject G;
	public UnityEvent OnExplosion;

	// Use this for initialization
	void Start ()
	{
		G = GameObject.Find ("Grenade");
	}

	[ContextMenu ("Explode")]
	public void Explode ()
	{
		StartCoroutine ("Exp");
		OnExplosion.Invoke ();
		CameraShaker.Instance.ShakeOnce (0.5f, 20f, 0.1f, 0.5f);
	}

	[ContextMenu ("Shake The Camera")]
	public void Shake ()
	{
		CameraShaker.Instance.ShakeOnce (0.5f, 20f, 0.1f, 0.5f);
	}

	IEnumerator Exp ()
	{
		for (int i = 0; i < 20; i++)
		{
			foreach (Collider col in Physics.OverlapSphere (transform.position, radius))
			{
				if (col.GetComponent<Rigidbody> () != null)
				{
					col.GetComponent<Rigidbody> ().AddExplosionForce (force + i + 1, transform.position, radius, upwardsModifier, forceMode);
				}
			}
			yield return new WaitForFixedUpdate ();
		}
	}
	// Update is called once per frame

	void FixedUpdate ()
	{
		//	 if (Input.GetMouseButtonDown (0)) {
		//			foreach(Collider col in Physics.OverlapSphere(transform.position, radius))
		//			{
		//				if(col.GetComponent<Rigidbody>() != null)
		//				{
		//					col.GetComponent<Rigidbody>().AddExplosionForce(force,transform.position,radius,upwardsModifier,forceMode);
		//				}
		//			}
		//		}

	}
}