using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceBlockContainer : MonoBehaviour
{
    public bool isPatternBased;

    public FaceBlocksRow parentFaceBlocksRow;
    public FaceBlock faceBlock;

    public void Init() {
        faceBlock.faceBlockContainer = this;
        faceBlock.Init();
    }

}
