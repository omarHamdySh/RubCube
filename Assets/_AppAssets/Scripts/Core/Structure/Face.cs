using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : MonoBehaviour
{
    #region Face Enums
    public enum Direction
    {
        up,
        down,
        forward,
        backward,
        left,
        right
    }

    #endregion

    #region Public  DataMembers

    public RubixCube parentCube; //Parent Type Object
    public Direction faceDirection;
    public RubixCube.FacePatternType patternType;
    public Collider collider;
    public Transform landingPositionObject;

    public List<FaceBlocksRow> rows;
    #endregion

    #region Public Methods

    public void Init()
    {
        foreach (var row in rows)
        {
            row.parentFace = this;
            row.Init();
        }

        parentCube.applyPattern(this);
    }
    public void updateFaceDirection()
    {
        //Initialize Face Pattern Based Rows
        GetComponentsInChildren<FaceBlocksRow>();
    }


    [ContextMenu("Init Prefab")]
    public void prefabInit()
    {
        FaceBlocksRow[] fetchedRows = GetComponentsInChildren<FaceBlocksRow>();

        foreach (var row in fetchedRows)
        {
            if (row.isPatternBased)
            {
                rows.Add(row);
            }
        }
    }


    public void reflectPattern(bool[] raw1States, bool[] raw2States, bool[] raw3States)
    {
        rows[0].faceBlockContainers[0].gameObject.SetActive(raw1States[0]);
        rows[0].faceBlockContainers[1].gameObject.SetActive(raw1States[1]);
        rows[0].faceBlockContainers[2].gameObject.SetActive(raw1States[2]);       
        
        rows[1].faceBlockContainers[0].gameObject.SetActive(raw2States[0]);
        rows[1].faceBlockContainers[1].gameObject.SetActive(raw2States[1]);
        rows[1].faceBlockContainers[2].gameObject.SetActive(raw2States[2]);     
        
        rows[2].faceBlockContainers[0].gameObject.SetActive(raw3States[0]);
        rows[2].faceBlockContainers[1].gameObject.SetActive(raw3States[1]);
        rows[2].faceBlockContainers[2].gameObject.SetActive(raw3States[2]);

    }
    #endregion
}
