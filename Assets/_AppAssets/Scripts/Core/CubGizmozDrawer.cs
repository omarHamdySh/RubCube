using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubGizmozDrawer : MonoBehaviour
{
    public Collider collider;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(collider)
        Gizmos.DrawSphere(collider.bounds.center, 0.2f);
        Gizmos.DrawSphere(collider.bounds.center+Vector3.forward, 0.2f);

        Gizmos.DrawSphere(collider.bounds.center + transform.forward, 0.2f);

        Debug.Log(Vector3.Dot((collider.bounds.center - transform.forward).normalized, (collider.bounds.center - Vector3.forward).normalized));
    }
}
