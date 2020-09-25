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
  
    #region DataMembers

    public Direction faceDirection;
    public RubixCube.FacePatternType patternType;
    public Collider collider;
    public Transform landingPositionObject;

    public List<FaceBlocksRow> rows;
    #endregion

    #region Public Methods
   
    public void updateFaceDirection()
    {
        //Initialize Face Pattern Based Rows
        GetComponentsInChildren<FaceBlocksRow>();
    }

    #endregion
}
