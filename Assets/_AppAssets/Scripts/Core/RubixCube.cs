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

    #endregion

    #region Private Methods

    #endregion

}
