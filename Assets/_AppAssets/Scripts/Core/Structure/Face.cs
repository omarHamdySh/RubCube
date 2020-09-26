using Array2DEditor;
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
    public Array2DBool patternTypeGrid;
    public Collider faceCollider;

    public List<FaceBlocksRow> rows;
    public List<GameObject> indicatorsPrefabsObjects;

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

        applyPattern();
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

    public virtual void applyPattern()
    {
        bool[,] cells = patternTypeGrid.GetCells();
        for (int i = 0; i < patternTypeGrid.GridSize.x; i++)
        {
            for (int j = 0; j < patternTypeGrid.GridSize.y; j++)
            {
                rows[i].faceBlockContainers[j].gameObject.SetActive(cells[i, j]);

                if (!cells[i, j])
                {
                    if (parentCube)
                    {
                        //Assign color to FadeinOut.cs -> Color
                        var emptyIndicatorCube = Instantiate(parentCube.IndecatorCube, rows[i].faceBlockContainers[j].transform.position, Quaternion.identity, transform);
                        emptyIndicatorCube.GetComponent<FadeinOut>().FadeColor = parentCube.color_Swap_Manager.indicatorColor;
                        indicatorsPrefabsObjects.Add(emptyIndicatorCube);
                    }
                }
            }
        }

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

        foreach (var obj in indicatorsPrefabsObjects)
        {
#if UNITY_EDITOR
            DestroyImmediate(obj.gameObject);
#else
                Destroy(obj.gameObject);
#endif
        }
    }

    #endregion

    #region MonoBehaviors
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shape")
        {
            parentCube.OnPatternFillEnd.Invoke();
            other.GetComponent<Collider>().enabled = false;
        }
    }

    #endregion

    #region private methods

    public void getFaceDirection()
    {
        if (parentCube)
        {
            if (Vector3.Dot((parentCube.core.transform.position - this.faceCollider.bounds.center).normalized, Vector3.up.normalized) > 0.8f)
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
    }


    public void OnPatternFillEnd()
    {
        foreach (var cubeObj in indicatorsPrefabsObjects)
        {
            Destroy(cubeObj.gameObject);
        }
    }
    #endregion

}