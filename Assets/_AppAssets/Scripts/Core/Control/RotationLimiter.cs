using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RotationLimiter : MonoBehaviour
{
    [SerializeField]
    public UnityEvent<Vector2> OnRotationAxisChange;

    public LeanStepValue2D leanStep;
    public LeanManualRotate leanManualRotate;

    private void Start()
    {
        leanStep.OnStep.AddListener(leanManualRotate.RotateAB);
    }

    public void limitRotation(Vector2 rotationDelta)
    {

        if (rotationDelta.x > rotationDelta.y)
        {
            rotationDelta.y = 0;
        }
        else if (rotationDelta.y > rotationDelta.x)
        {
            rotationDelta.x = 0;
        }
        else
        {
            rotationDelta.x = 0; rotationDelta.y = 0;
        }

        leanStep.AddValue(rotationDelta);

    }
}
