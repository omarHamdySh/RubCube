using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSegmentSpawner : MonoBehaviour
{
    public LevelSegment LevelSegment;
    public float SpawnRate;


    private void Start()
    {
        //StartCoroutine(Spawn());
    }


    IEnumerator Spawn()
    {
        while (true)
        {
            LevelSegment.Init();

            yield return new WaitForSeconds(SpawnRate);
        }
    }

    [ContextMenu("Spawn")] 
    public void spawn() {

        LevelSegment.Init();
    }
}
