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

    public GameObject shapePrefab;
    public List<Shape> shapes;


    public UnityEvent OnShapeMatch;
    public UnityEvent OnDoesntMatch;
    public UnityEvent OnShapePopualationEnd;

    [ContextMenu("Initialize")]
    public void Init()
    {
        foreach (var facePatternType in rubixCube.facePatternsTypes)
        {
            var shapeObj = Instantiate(shapePrefab, transform.position, Quaternion.identity, transform);
            shapeObj.transform.Rotate(Vector3.up, randomizeRotation());
            var shape = shapeObj.GetComponent<Shape>();
            shapes.Add(shape);
            shape.patternTypeGrid = facePatternType;
            shape.Init();
            shapeObj.SetActive(false);
        }
        currentShape = shapes[0];
        currentShape.gameObject.SetActive(true);
        shapes.RemoveAt(0);
        
    }

    public void compareShapeAndCurrenUpFace()
    {
        StartCoroutine(compareFaces());
        OnShapeMatch.AddListener(populateCurrentShape);
    }

    IEnumerator compareFaces()
    {

        yield return new WaitForSeconds(0.1f);

        if (currentShape != null)
        {
            if (rubixCube.currentUpFace.patternTypeGrid == currentShape.patternTypeGrid)
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
    }

    public void populateCurrentShape()
    {
        currentShape.transform.DOMove(rubixCube.currentUpFace.transform.position, populationAnimationDuration).SetEase(populationAnimationEaseType).OnComplete(OnPopulationAnimationEnd);
        GameManager.Instance.sfxSource.clip = GameManager.Instance.ShapePopulationSound;
        GameManager.Instance.sfxSource.Play();
    }

    public void OnPopulationAnimationEnd()
    {
        currentShape.transform.parent = rubixCube.currentUpFace.transform;
        currentShape = null;
        OnShapePopualationEnd.Invoke();
        rubixCube.OnPatternFillEnd.Invoke();
    }

    public int randomizeRotation()
    {
        int num = Random.Range(0, 5);

        switch (num)
        {
            case 1:
                return 0;
            case 2:
                return 90;
            case 3:
                return 180;
            case 4:
                return 270;
            default:
                return 360;
        }
    }

    [ContextMenu("Reset")]
    public void reset()
    {

        foreach (var shape in shapes)
        {
#if UNITY_EDITOR
            DestroyImmediate(shape.gameObject);
#else
                Destroy(shape.gameObject);
#endif
        }

        shapes = new List<Shape>();
    }
}
