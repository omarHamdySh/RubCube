using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System;

public class ShapesManager : MonoBehaviour
{
    public Shape currentShape;
    public RubixCube rubixCube;
    public float populationAnimationDuration;
    public Ease populationAnimationEaseType;

    public GameObject shapePrefab;
    public GameObject phantomBlock;


    public List<Shape> shapes;
    public SwipeControl swipeControl;
    [SerializeField] bool isAIControlled;
    List<GameObject> phantomObjects = new List<GameObject>();

    public UnityEvent OnShapeMatch;
    public UnityEvent OnDoesntMatch;
    public UnityEvent OnRotationDoesntMatch;
    public UnityEvent OnShapePopualationEnd;
    public UnityEvent OnAllShapesPopulation;

    public UnityEvent OnCurrentFaceIsPopulated;
    public UnityEvent OnCurrentFaceIsUnPopulated;

    private void Start()
    {
        if (isAIControlled)
        {
            startGame();
        }

    }

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

        compareShapeAndCurrenUpFace();

    }


    public void reInsert(Shape shape)
    {
        shapes.Insert(0, shape);
    }

    public void compareShapeAndCurrenUpFace()
    {
        StartCoroutine(compareFaces());
        StartCoroutine(projectShapePhantom());
        OnShapeMatch.AddListener(populateCurrentShape);
    }

    IEnumerator compareFaces()
    {

        yield return new WaitForSeconds(0.1f);

        if (currentShape != null)
        {
            if (rubixCube.currentUpFace.patternTypeGrid == currentShape.patternTypeGrid)
            {
                if (rubixCube.currentUpFace.isPopulated)
                {
                    OnCurrentFaceIsPopulated.Invoke();
                }
                else
                {
                    OnCurrentFaceIsUnPopulated.Invoke();
                }

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
                                else
                                {

                                }

                            }
                        }
                    }
                }
                rubixCube.currentUpFace.faceCollider.enabled = true;

                if (isPopulatable)
                {
                    rubixCube.currentUpFace.isPopulated = true;
                    OnShapeMatch.Invoke();
                }
                else
                {
                    OnRotationDoesntMatch.Invoke();
                }

            }
            else
            {
                OnDoesntMatch.Invoke();
            }
        }
    }

    IEnumerator projectShapePhantom()
    {
        yield return new WaitForSeconds(0.1f);

        if (currentShape != null)
        {
            if (phantomObjects.Count == 0) ;
            {
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
                                var phantomObject = Instantiate(phantomBlock, hit.point /*+ (Vector3.up * .5f)*/, Quaternion.identity, currentShape.transform);
                                phantomObjects.Add(phantomObject);

                                if (hit.collider.gameObject.tag != "Target")
                                {
                                    updatePhantomObjectsColors(false);
                                }
                                else
                                {
                                    updatePhantomObjectsColors(true);

                                }

                            }
                        }
                    }
                }
            }
        }
    }

    private void updatePhantomObjectsColors(bool state)
    {
        bool isAnyFalse = false;

        foreach (var phantomObject in phantomObjects)
        {
            var controller = phantomObject.GetComponent<HollowController>();

            if (!controller.isPopulatable)
            {
                isAnyFalse = true;
            }
        }

        foreach (var phantomObject in phantomObjects)
        {
            var controller = phantomObject.GetComponent<HollowController>();

            if (state)
            {
                if (isAnyFalse)
                {
                    controller.isPopulatable = false;
                    controller.changeColorToFalse();
                }
                else
                {
                    controller.isPopulatable = true;
                    controller.changeColorToTrue();
                }
            }
            else
            {
                controller.isPopulatable = false;
                controller.changeColorToFalse();
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
        deletePhantomObjects();
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

    public void deletePhantomObjects()
    {
        foreach (var obj in phantomObjects)
        {
            Destroy(obj.gameObject);
        }

        phantomObjects = new List<GameObject>();
    }

    public int randomizeRotation()
    {
        int num = UnityEngine.Random.Range(0, 5);

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
