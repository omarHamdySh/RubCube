using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : Face
{
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
        bool[,] cells = patternTypeGrid.GetCells();
        for (int i = 0; i < patternTypeGrid.GridSize.x; i++)
        {
            for (int j = 0; j < patternTypeGrid.GridSize.y; j++)
            {
                rows[i].faceBlockContainers[j].gameObject.SetActive(!cells[i, j]);
            }
        }
    }
}
