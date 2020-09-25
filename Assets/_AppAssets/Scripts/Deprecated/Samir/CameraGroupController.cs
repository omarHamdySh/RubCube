using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Collections;
public class CameraGroupController : MonoBehaviour
{
    public static CameraGroupController instance;
    [SerializeField] float weight, radius=.5f;
    CinemachineTargetGroup targetGroup;
    int playersStacked;
    Cinemachine.CinemachineTargetGroup.Target target;
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    CinemachineTransposer transposer;
    Vector3 initialCamOffset, currentcamOffset;
   
    void Awake()
    {
        instance = this;
        targetGroup = GetComponent<CinemachineTargetGroup>();
        if(virtualCamera==null)
        {
            virtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        }
        transposer= virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        initialCamOffset = transposer.m_FollowOffset;
        currentcamOffset = initialCamOffset;
    }

    private void Start()
    {
        StartCoroutine(AddingFirstPlayer());
        //GameManager.Instance.OnHitEndLine.AddListener(OnEndLineAchieved);
    }

    IEnumerator AddingFirstPlayer()
    {
        yield return new WaitForEndOfFrame();
        if(LevelManager.Instance)
            AddCharacterToCam(LevelManager.Instance.mainStack.characters.Peek().transform);
    }

    public void AddCharacterQueue(Queue<Character> characters)
    {
        Queue<Character> charactersClone = new Queue<Character>(characters);
        for (int i = charactersClone.Count; i >0 ; i--)
        {
            AddCharacterToCam(charactersClone.Dequeue().transform);
        }
    }

    public void AddCharacterToCam(Transform characterTransform)
    {
        ++playersStacked;

        targetGroup.AddMember(characterTransform, weight+playersStacked* playersStacked* playersStacked*.8f, radius);
        UpdateTransposer();
    }

    public void RemoveCharacterFromCam(Transform characterTransform)
    {
        --playersStacked;

        targetGroup.RemoveMember(characterTransform);
        UpdateTransposer();
    }

    void UpdateTransposer()
    {
        currentcamOffset.y = initialCamOffset.y + playersStacked * .12f;
        currentcamOffset.z = initialCamOffset.z - playersStacked * 2f;
        transposer.m_FollowOffset = currentcamOffset;
    }

    /*public void OnEndLineAchieved()
    {
        transposer.m_FollowOffset = new Vector3(0, 4+ initialCamOffset.y + playersStacked * .1f, -9+ initialCamOffset.z - playersStacked * 1.2f);
        
    }*/

}
