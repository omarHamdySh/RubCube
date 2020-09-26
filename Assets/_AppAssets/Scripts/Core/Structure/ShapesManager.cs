using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class ShapesManager : MonoBehaviour
{
    public Shape currentShape;
    public RubixCube rubixCube;
    public float populationAnimationDuration;
    public Ease populationAnimationEaseType;

    public UnityEvent OnShapeMatch;
    public UnityEvent OnDoesntMatch;
    public UnityEvent OnShapePopualationEnd;

    public void compareShapeAndCurrenUpFace()
    {
        StartCoroutine(compareFaces());
        OnShapeMatch.AddListener(populateCurrentShape);
    }

    IEnumerator compareFaces()
    {

        yield return new WaitForSeconds(0.5f);
        if (rubixCube.currentUpFace.patternType == currentShape.patternType)
        {
            Vector3 shapeDirection = (currentShape.transform.forward - currentShape.faceCollider.bounds.center).normalized;
            Vector3 currentUpFaceDirection = (rubixCube.currentUpFace.transform.forward - rubixCube.currentUpFace.faceCollider.bounds.center).normalized;

            if (Vector3.Dot(shapeDirection, currentUpFaceDirection) > 0.95f)
            {
                OnShapeMatch.Invoke();
            }
            else
            {
                OnDoesntMatch.Invoke();
            }
        }
        else
        {
            OnDoesntMatch.Invoke();
        }
    }

    public void populateCurrentShape()
    {
        currentShape.transform.DOMove(rubixCube.currentUpFace.transform.position, populationAnimationDuration).SetEase(populationAnimationEaseType).OnComplete(OnPopulationAnimationEnd);
    }

    public void OnPopulationAnimationEnd()
    {
        GameManager.Instance.sfxSource.clip = GameManager.Instance.ShapePopulationSound;
        GameManager.Instance.sfxSource.Play();
        OnShapePopualationEnd.Invoke();
    }

}
