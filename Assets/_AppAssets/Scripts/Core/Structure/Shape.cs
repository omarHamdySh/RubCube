using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : Face
{

    public bool isAboutToPopulated;
    [ContextMenu("Initialize Shape")]
    public override void Init()
    {
        foreach (var row in rows)
        {
            row.parentFace = this;
            row.Init();
        }
        applyPattern();
    }

    public override void applyPattern()
    {
        print("Do the inverse of pattern");
        bool[,] cells = patternTypeGrid.GetCells();
        for (int i = 0; i < patternTypeGrid.GridSize.x; i++)
        {
            for (int j = 0; j < patternTypeGrid.GridSize.y; j++)
            {
                rows[i].faceBlockContainers[j].gameObject.SetActive(!cells[i, j]);
            }
        }
    }

    public void hideRows()
    {
        foreach (var row in rows)
        {
            row.gameObject.SetActive(false);
        }
    }

    public void showRows() 
    {
        foreach (var row in rows)
        {
            row.gameObject.SetActive(true);
        }
    }

    public void ApplyShapePrefab(GameObject prefab) {

        foreach (var row in rows)
        {
            foreach (var container in row.faceBlockContainers)
            {
                deleteAllChildrenOfFaceBlock(container);
                Instantiate(prefab, container.faceBlock.transform);
            }
        }
    }


    private static void deleteAllChildrenOfFaceBlock(FaceBlockContainer container)
    {
        for (int i = 0; i < container.faceBlock.transform.childCount; i++)
        {

#if UNITY_EDITOR
            DestroyImmediate(container.faceBlock.transform.GetChild(i).gameObject);
#else
                                    Destroy(obj);
#endif

        }
    }


}
