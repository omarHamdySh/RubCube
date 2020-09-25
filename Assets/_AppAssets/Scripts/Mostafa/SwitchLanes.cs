using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SwitchLanes : MonoBehaviour
{

    public List<Transform> lanes;

    public int currentLane;
    public Transform laner;


    //how long to switch between lanes
    public float transition_time;

    // Start is called before the first frame update
    void Start()
    {
        //mostafa modification
        currentLane = 1;
        Vector3 tmp = lanes[currentLane].transform.position;
        tmp.y = 1;
        laner.transform.position = tmp;
    }

  
    //mostafa modification
    [ContextMenu("switchLeft")]
    public void switchLeft()
    {
        if (currentLane > 0)
        {
            Vector3 tmp = new Vector3(lanes[currentLane - 1].position.x, transform.position.y, transform.position.z);
            laner.DOMove(tmp, transition_time, false);
            currentLane--;
        }
    }

    //mostafa modification
    [ContextMenu("switchRight")]
    public void switchRight()
    {
        if (currentLane < lanes.Count - 1)
        {
            Vector3 tmp = new Vector3(lanes[currentLane + 1].position.x, transform.position.y, transform.position.z);
            laner.DOMove(tmp, transition_time, false);
            currentLane++;
        }
    }
}
