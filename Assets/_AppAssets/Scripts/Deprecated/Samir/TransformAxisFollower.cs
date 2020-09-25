using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformAxisFollower : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform objectTransform;
    [SerializeField] bool followX = true, followY = true, followZ = true;
    Vector3 position;
    private void Awake()
    {
        position = transform.position;
    }
    void Update()
    {
        if (followX)
            position.x = objectTransform.position.x;
        if (followY)
            position.y = Mathf.MoveTowards(transform.position.y, objectTransform.position.y,.5f*Time.deltaTime);
        if (followZ)
            position.z = objectTransform.position.z;

        transform.position = position;
    }
}
