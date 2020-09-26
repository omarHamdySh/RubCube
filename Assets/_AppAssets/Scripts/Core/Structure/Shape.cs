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
        RubixCube.applyPatternReverse(this);
    }
}
