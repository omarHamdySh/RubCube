using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomFlyingPath : MonoBehaviour {

    public GameObject[] waypoints;
    int current = 0;
    public float speed;
    float WPradius = 1;

    void Update()
    {
        if (Vector3.Distance(waypoints[current].transform.position, transform.position) < WPradius)
        {
            current++;// = Random.Range(0, waypoints.Length);
            print(current);
            if (current >= waypoints.Length)
            {
                current = 0;
            }
        }

        Vector3 direction = waypoints[current].transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position, Time.deltaTime * speed);
    }
}
