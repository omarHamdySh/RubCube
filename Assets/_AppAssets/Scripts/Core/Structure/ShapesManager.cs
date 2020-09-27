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
    public UnityEvent OnAllShapesPopulation;

    public SwipeControl swipeControl;

    [ContextMenu("Initialize")]
    public void Init()
    {
        //reset();
        foreach (var facePatternType in rubixCube.facePatternsTypes)
        {
            var shapeObj = Instantiate(shapePrefab, transform.position, Quaternion.identity, transform);
            shapeObj.transform.Rotate(Vector3.up, randomizeRotation());
            var shape = shapeObj.GetComponent<Shape>();
            shapes.Add(shape);
            shape.patternTypeGrid = facePatternType;
            shape.Init();
            shape.hideRows();
        }
    }

    [ContextMenu("Start Game")]
    public void startGame()
    {
        currentShape = shapes[0];
        shapes.RemoveAt(0);
        currentShape.showRows();

        if (rubixCube.currentUpFace.patternTypeGrid == currentShape.patternTypeGrid)
        {
            Shape tempShape = null;
            foreach (Shape shape in shapes)
            {
                if (shape.patternTypeGrid != rubixCube.currentUpFace.patternTypeGrid)
                    if (tempShape == null) tempShape = shape;
            }

            if (tempShape != null)
            {
                reInsert(tempShape);
                shapes.Remove(tempShape);
            }
           
            currentShape.hideRows();
            shapes.Add(currentShape);

            currentShape = shapes[0];
            shapes.RemoveAt(0);
            currentShape.showRows();
        }
    }


    public void reInsert(Shape shape)
    {
        shapes.Insert(0, shape);
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
                rubixCube.currentUpFace.faceCollider.enabled = false;
                bool isPopulatable = true;

                foreach (var rows in currentShape.rows)
                {
                    foreach (var container in rows.faceBlockContainers)
                    {
                        if (container.gameObject.activeInHierarchy)
                        {
                            RaycastHit hit;
                            // Does the ray intersect any objects excluding the player layer
                            if (Physics.Raycast(container.transform.position, -Vector3.up, out hit, Mathf.Infinity))
                            {
                                if (hit.collider.gameObject.tag != "Target")
                                {
                                    isPopulatable = false;
                                }

                            }
                        }
                    }
                }
                rubixCube.currentUpFace.faceCollider.enabled = true;

                if (isPopulatable)
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
        currentShape.isAboutToPopulated = true;
        currentShape.transform.parent = rubixCube.currentUpFace.transform;
        GameManager.Instance.sfxSource.clip = GameManager.Instance.ShapePopulationSound;
        GameManager.Instance.sfxSource.Play();

    }

    public void OnPopulationAnimationEnd()
    {
        if (currentShape.isAboutToPopulated)
        {
            currentShape.isAboutToPopulated = false;
            currentShape = null;
            Handheld.Vibrate();
            OnShapePopualationEnd.Invoke();
            rubixCube.OnPatternFillEnd.Invoke();
        }

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
            if (!shape)
                continue;
            DestroyImmediate(shape.gameObject);
#else
                Destroy(shape.gameObject);
#endif
        }
        shapes = new List<Shape>();
    }

    public void activateTheNextShape()
    {
        if (shapes.Count > 0)
        {
            currentShape = shapes[0];
            currentShape.showRows();
            shapes.RemoveAt(0);
        }
        else
        {
            OnAllShapesPopulation.Invoke();
        }

    }
}
