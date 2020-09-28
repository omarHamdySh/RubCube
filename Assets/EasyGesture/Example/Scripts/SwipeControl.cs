using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class SwipeControl : MonoBehaviour
{

    [SerializeField] private Transform playerPos;
    [SerializeField] private float rotateTime = 3.0f;
    [SerializeField] private float rotateDegrees = 90.0f;
    [SerializeField] private bool rotating = false;
    [SerializeField] UnityEvent OnRotationEnd;

    private Vector3 Pos;
    private float[] Rotations = new float[4];
    private Vector3[] Random_Dir = new Vector3[8];
    public bool isTutorialMode;

    public bool freezeSwipeRightFaceUP;
    public bool freezeSwipeLeftFaceUP;
    public bool freezeSwipeRightFaceDown;
    public bool freezeSwipeLeftFaceDown;
    public bool freezeSwipeRight;
    public bool freezeSwipeLeft;

    private void Start()
    {
        Rotations[0] = 90f;
        Rotations[1] = 180f;
        Rotations[2] = 270f;
        Rotations[3] = 0f;

        Random_Dir[0] = Vector3.up;
        Random_Dir[1] = Vector3.down;
        Random_Dir[2] = Vector3.left;
        Random_Dir[3] = Vector3.right;
        Random_Dir[4] = -Vector3.up;
        Random_Dir[5] = -Vector3.down;
        Random_Dir[6] = -Vector3.left;
        Random_Dir[7] = -Vector3.right;

        if (!isTutorialMode)
        {
            // Random the first shape angle
            rotateRubixCubeRandomly();
        }
    }

    public void rotateRubixCubeRandomly()
    {
        transform.rotation = Quaternion.AngleAxis(Rotations[Random.Range(0, Rotations.Length)], Random_Dir[Random.Range(0, Random_Dir.Length)]);
        OnRotationEnd.Invoke();
    }

    private void OnEnable()
    {
        EasyGesture.onSwipe += OnSwipe;
    }

    private void OnDisable()
    {
        EasyGesture.onSwipe -= OnSwipe;
    }

    private void OnSwipe(EasyGesture.Gesture type, float speed)
    {
        if (rotating)
        {
            return;
        }

        Pos = Input.GetTouch(0).position;
        switch (type)
        {
            case EasyGesture.Gesture.SWIPE_DOWN:
                if (Pos.x < (Screen.width / 2))
                {
                    if (!freezeSwipeLeftFaceUP)
                    StartCoroutine(Rotate_obj(transform, playerPos, Vector3.forward, rotateDegrees, rotateTime));
                }
                if (Pos.x > (Screen.width / 2))
                {
                    if (!freezeSwipeRightFaceUP)
                        StartCoroutine(Rotate_obj(transform, playerPos, Vector3.left, rotateDegrees, rotateTime));
                }
                break;
            case EasyGesture.Gesture.SWIPE_UP:
                if (Pos.x < (Screen.width / 2))
                {
                    if (!freezeSwipeLeftFaceDown)
                        StartCoroutine(Rotate_obj(transform, playerPos, -Vector3.forward, rotateDegrees, rotateTime));
                }
                if (Pos.x > (Screen.width / 2))
                {
                    if (!freezeSwipeRightFaceDown)
                        StartCoroutine(Rotate_obj(transform, playerPos, -Vector3.left, rotateDegrees, rotateTime));
                }
                break;
            case EasyGesture.Gesture.SWIPE_LEFT:
                if (!freezeSwipeLeft)
                    StartCoroutine(Rotate_obj(transform, playerPos, Vector3.up, rotateDegrees, rotateTime));
                break;
            case EasyGesture.Gesture.SWIPE_RIGHT:
                if (!freezeSwipeRight)
                    StartCoroutine(Rotate_obj(transform, playerPos, -Vector3.up, rotateDegrees, rotateTime));
                break;
        }
    }

    IEnumerator Rotate_obj(Transform thisTransform, Transform otherTransform, Vector3 rotateAxis, float degrees, float totalTime)
    {
        rotating = true;

        Quaternion startRotation = thisTransform.rotation;
        //Vector3 startPosition = thisTransform.position;
        transform.RotateAround(otherTransform.position, rotateAxis, degrees);
        Quaternion endRotation = thisTransform.rotation;
        //Vector3 endPosition = thisTransform.position;
        thisTransform.rotation = startRotation;
        //thisTransform.position = startPosition;

        float rate = degrees / totalTime;
        for (float i = 0.0f; i < degrees; i += Time.deltaTime * rate)
        {
            yield return null;
            thisTransform.RotateAround(otherTransform.position, rotateAxis, Time.deltaTime * rate);
        }

        thisTransform.rotation = endRotation;
        //thisTransform.position = endPosition;
        rotating = false;
        OnRotationEnd.Invoke();
    }
}