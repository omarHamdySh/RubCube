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
    public Collider faceCollider;

    public List<FaceBlocksRow> rows;
    #endregion

    #region Public Methods

    [ContextMenu("Initialize")]
    public virtual void Init()
    {
        foreach (var row in rows)
        {
            row.parentFace = this;
            row.Init();
        }

        RubixCube.applyPattern(this);
        updateFaceDirection();
    }
    public void updateFaceDirection()
    {
        getFaceDirection();

        if (faceDirection == Direction.up && parentCube)
        {
            parentCube.currentUpFace = this;
        }
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

    [ContextMenu("Reset To None Pattern")]
    public void reset()
    {
        foreach (var row in rows)
        {
            foreach (var faceBlockContainer in row.faceBlockContainers)
            {
                faceBlockContainer.gameObject.SetActive(true);
            }
        }
    }

    #endregion

    #region MonoBehaviors

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag =="Shape")
        {
            parentCube.OnPatternFillEnd.Invoke();
            other.GetComponent<Collider>().enabled = false;
        }
    }

    #endregion


    #region private methods

    public void getFaceDirection()
    {
        if (Vector3.Dot((parentCube.core.transform.position -this.faceCollider.bounds.center).normalized, Vector3.up.normalized) > 0.8f)
        {
            faceDirection = Direction.down;
        }
        else if (Vector3.Dot((parentCube.core.transform.position - this.faceCollider.bounds.center), Vector3.down.normalized) > 0.8f)
        {
            faceDirection = Direction.up;
        }
        else if (Vector3.Dot((parentCube.core.transform.position - this.faceCollider.bounds.center).normalized, Vector3.right.normalized) > 0.8f)
        {
            faceDirection = Direction.left;
        }
        else if (Vector3.Dot((parentCube.core.transform.position - this.faceCollider.bounds.center).normalized, Vector3.left.normalized) > 0.8f)
        {
            faceDirection = Direction.right;
        }
        else if (Vector3.Dot((parentCube.core.transform.position - this.faceCollider.bounds.center).normalized, Vector3.forward.normalized) > 0.8f)
        {
            faceDirection = Direction.backward;
        }
        else if (Vector3.Dot((parentCube.core.transform.position - this.faceCollider.bounds.center).normalized, Vector3.back.normalized) > 0.8f)
        {
            faceDirection = Direction.forward;
        }
    }

    #endregion


}