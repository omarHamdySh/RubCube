using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class RubixCube : MonoBehaviour
{

    #region Enums
    public enum FacePatternType
    {
        None,
        TShape,
        IShape
    }

    #endregion

    #region  Public Data Memebers

    public List<Face> faces;
    public GameObject core;
    public UnityEvent OnCubeTwist;
    public float rotateDuration;

    #endregion

    #region  Private Data Memebers

    private bool isRotating;

    #endregion

    #region MonoBehaviours

    #endregion

    #region Public Methods

    [ContextMenu("Initialize Cube")]
    public void Init()
    {
        InitiFaces();
    }

    public void InitiFaces()
    {
        foreach (Face face in faces)
        {
            face.parentCube = this;
            face.Init();
            OnCubeTwist.AddListener(face.updateFaceDirection);
        }
    }

    [ContextMenu("twist Left")]
    public void twistLeft()
    {
        if (!isRotating)
        {
            isRotating = true;
            this.transform.DORotate(transform.rotation.eulerAngles + Vector3.up * 90, rotateDuration, RotateMode.FastBeyond360).OnComplete(OnTwistComplete).SetEase(Ease.Linear);
        }
    }

    [ContextMenu("twist Right")]
    public void twistRight()
    {
        if (!isRotating)
        {
            isRotating = true;
            this.transform.DORotate(transform.rotation.eulerAngles + Vector3.up * -90, rotateDuration, RotateMode.FastBeyond360).OnComplete(OnTwistComplete).SetEase(Ease.Linear);
        }
    }

    [ContextMenu("twist Up")]
    public void twistUp()
    {
        if (!isRotating)
        {
            isRotating = true;
            this.transform.DORotate(transform.rotation.eulerAngles + Vector3.forward * 90, rotateDuration, RotateMode.FastBeyond360).OnComplete(OnTwistComplete).SetEase(Ease.Linear);
        }
    }

    [ContextMenu("twist Down")]
    public void twistDown()
    {
        if (!isRotating)
        {
            isRotating = true;
            this.transform.DORotate(transform.rotation.eulerAngles + Vector3.forward * -90, rotateDuration, RotateMode.FastBeyond360).OnComplete(OnTwistComplete).SetEase(Ease.Linear);
        }
    }

    public void OnTwistComplete()
    {
        isRotating = false;
        OnCubeTwist.Invoke();
    }

    [ContextMenu("Init Prefab")]
    public void prefabInit()
    {
        Face[] fetchedFaces = GetComponentsInChildren<Face>();

        foreach (var face in fetchedFaces)
        {
            faces.Add(face);
        }
    }

    public void applyPattern(Face face)
    {
        bool[] row1States = { true, true, true };
        bool[] row2States = { true, true, true };
        bool[] row3States = { true, true, true };

        switch (face.patternType)
        {
            case FacePatternType.None:

                break;
            case FacePatternType.TShape:
                row2States[0] = false; row2States[2] = false;
                row3States[0] = false; row3States[2] = false;
                break;
            case FacePatternType.IShape:
                row2States[0] = false; row2States[2] = false;
                break;

            default:
                break;
        }

        face.reflectPattern(row1States, row2States, row3States);

    }

    #endregion

    #region Private Methods

    #endregion

}
