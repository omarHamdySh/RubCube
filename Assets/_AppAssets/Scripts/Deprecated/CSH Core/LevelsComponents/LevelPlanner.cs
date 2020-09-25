using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPlanner : MonoBehaviour
{

    #region Level Planner Data Members

    [SerializeField] private Transform levelComponentsRoot;

    [SerializeField] private float startPositionZ;
    [SerializeField] private float endPositionZ;
    [SerializeField] private float spacingAmount;
    [SerializeField] private float segmentSize;

    [SerializeField] private List<LevelSegment> levelSegments;

    #endregion

    #region Test Data Members

    [SerializeField] private int segmentsNumber;
    [SerializeField] private GameObject segmentPrefab;

    #endregion
    private void Start()
    {
        InstantiateLevelsSegments();
    }

    [ContextMenu("Instantiate Level Segments")]
    public void InstantiateLevelsSegments()
    {

        //ResetLevel();

        foreach (Transform spawnedSegment in transform)
        {
            //GameObject spawnedSegment = Instantiate(segmentPrefab, (Vector3.zero + Vector3.forward * startPositionZ), Quaternion.identity, transform);
            var segment = spawnedSegment.GetComponentInChildren<LevelSegment>();
            segment.Init();
            levelSegments.Add(segment);
        }
    }

    /// <summary>
    /// Disable one of the lanes that dosn't share the same color with the line changer color
    /// </summary>
    /// <param name="spawnedSegment"></param>
    /// <returns></returns>
    IEnumerator randomizeLaneDisabling(LevelSegment spawnedSegment, GameplayColor colorChangerColor)
    {
        yield return new WaitForSeconds(0);
        Lane lane = spawnedSegment.lanes[Random.Range(0, 3)];
        if (lane.laneColor == colorChangerColor)
        {
            while (lane.laneColor == colorChangerColor)
            {
                lane = spawnedSegment.lanes[Random.Range(0, 3)];
            }
            lane.gameObject.SetActive(false);
        }
        else
        {
            lane.gameObject.SetActive(false);
        }
    }

    public void ResetLevel()
    {
        foreach (LevelSegment levelSegment in levelSegments)
        {
#if UNITY_EDITOR
            DestroyImmediate(levelSegment.transform.parent.gameObject);
#else
                Destroy(levelSegment.transform.parent.gameObject);
#endif
        }

        levelSegments = new List<LevelSegment>();


    }


}
