using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapesManager : MonoBehaviour
{
    public Shape currentShape;
    public RubixCube rubixCube;

    public void compareShapeAndCurrenUpFace()
    {

        StartCoroutine(compareFaces());
    }

    IEnumerator compareFaces() {

        yield return new WaitForSeconds(0.5f);
        if (rubixCube.currentUpFace.patternType == currentShape.patternType)
        {
            print("Match patten");

            Vector3 shapeDirection = (currentShape.transform.forward - currentShape.faceCollider.bounds.center).normalized;
            Vector3 currentUpFaceDirection = (rubixCube.currentUpFace.transform.forward - rubixCube.currentUpFace.faceCollider.bounds.center).normalized;

            if (Vector3.Dot(shapeDirection, currentUpFaceDirection) > 0.95f)
            {
                print("Match direction");
            }
            else
            {
                print("doesn't match direction");
            }
        }
        else
        {
            print("doesn't match patten");
        }
    }

}
