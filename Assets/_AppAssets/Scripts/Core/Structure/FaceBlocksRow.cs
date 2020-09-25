using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceBlocksRow : MonoBehaviour
{
    public bool isPatternBased;

    public Face parentFace;
    public List<FaceBlockContainer> faceBlockContainers;

    public void Init()
    {
        //Initialize faceBlockContainer
        InitFaceBlockContainers();
    }

    public void InitFaceBlockContainers()
    {
        foreach (var faceBlockContainer in faceBlockContainers)
        {
            faceBlockContainer.parentFaceBlocksRow = this;
            faceBlockContainer.Init();
        }
    }

    [ContextMenu("Init Prefab")]
    public void prefabInit()
    {
        FaceBlockContainer[] fetchedBlocksContainers = GetComponentsInChildren<FaceBlockContainer>();

        foreach (var blockContainer in fetchedBlocksContainers)
        {
            if (blockContainer.isPatternBased)
            {
                faceBlockContainers.Add(blockContainer);
            }
        }
    }

}
