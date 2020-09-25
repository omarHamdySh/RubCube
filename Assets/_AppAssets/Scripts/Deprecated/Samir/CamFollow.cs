using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public static CamFollow instance;

    public List<Transform> targets;

    public Vector3 offset;
    public float smoothTime = .5f;

    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomLimiter = 50f;

    private Vector3 velocity;
    private Camera cam;

    int playersStacked;
    [SerializeField] Vector3 addToCenterPoint;
    private void Awake()
    {
        instance = this;
        cam = GetComponent<Camera>();
    }

    private void Start()
    {
        StartCoroutine(AddingFirstPlayer());
    }

    IEnumerator AddingFirstPlayer()
    {
        yield return new WaitForEndOfFrame();
        if (LevelManager.Instance)
            AddCharacterToCam(LevelManager.Instance.mainStack.characters.Peek().transform);
    }

    private void LateUpdate()
    {

        if (targets.Count == 0)
            return;


        move();
        //zoom();
        ChangeRotation();
    }

    void zoom()
    {

        float newZoom = Mathf.Lerp(maxZoom, minZoom, getGreatestDistance() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);


    }

    Vector3 camNewPosition;
    void move()
    {
        camNewPosition = offset;

        camNewPosition.z += targets[0].transform.position.z;

        // Assign value to Camera position
        transform.position = camNewPosition;
    }

    float getGreatestDistance()
    {

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {

            bounds.Encapsulate(targets[i].position);


        }

        return bounds.size.x;

    }


    Vector3 getCenterPoint()
    {

        if (targets.Count == 1)
        {

            return targets[0].position;


        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {


            bounds.Encapsulate(targets[i].position);

        }

        return bounds.center;


    }

    public void AddCharacterQueue(Queue<Character> characters)
    {
        Queue<Character> charactersClone = new Queue<Character>(characters);
        for (int i = charactersClone.Count; i > 0; i--)
        {
            ++playersStacked;
            AddCharacterToCam(charactersClone.Dequeue().transform);
        }
    }

    public void AddCharacterToCam(Transform characterTransform)
    {
        ++playersStacked;
        targets.Add(characterTransform);
        //targetGroup.AddMember(characterTransform, weight + playersStacked, radius);
        //UpdateTransposer();
    }

    public void RemoveCharacterFromCam(Transform characterTransform)
    {
        --playersStacked;
        targets.Remove(characterTransform);
        //targetGroup.RemoveMember(characterTransform);
        //UpdateTransposer();
    }
    public void ChangeRotation()
    {
        Vector3 midPositions=new Vector3();

        for (int i = 0; i < targets.Count; i++)
        {
            midPositions += targets[i].position;
        }
        midPositions /= targets.Count;
        midPositions += addToCenterPoint;
        midPositions.x = 0;
        transform.LookAt(midPositions);
    }
}
