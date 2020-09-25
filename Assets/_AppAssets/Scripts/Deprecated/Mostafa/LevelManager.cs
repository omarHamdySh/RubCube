using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Diagnostics.Contracts;
using Invector.vCharacterController;
using System.Linq;
using System;

public class LevelManager : MonoBehaviour
{

    #region singleton
    private static LevelManager _Instance;
    public static LevelManager Instance
    {
        get { return _Instance; }
    }

    private void Awake()
    {
        /** Order of methods calling is critical**/
        if (_Instance == null)
        {
            _Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
    #endregion

    public CharacterStack mainStack;

    public Character currentWalkingCharacter;
    public GameObject currentWalkingGameObject;// camera will follow this, assign it in characterStack.align
    public vThirdPersonInput currentWalkingInputController;

    public float jumpBoost = 1;
    public float jump_height = 4;
    public float character_height = 1.75f;

    public List<Transform> lanepoints;
    public int currentLane;
    public float transition_time;
    public FollowPlayer camFollow;
    public float tweenStackFollowTime;//time it takes characters in the stack to follow each other 

    public float jumpTapThreshold;//how much time a jump tap takes
    [SerializeField] private float jumpTapTimePassed = 0;

    public Transform LevelEndPoint;
    public bool isEndLineActive;

    // Start is called before the first frame update
    void Start()
    {
        mainStack.Init();
        camFollow = Camera.main.gameObject.AddComponent<FollowPlayer>();
        camFollow.FollowStackOfPlayers();

        camFollow.x_offset = -2; camFollow.y_offset = -3.5f; camFollow.z_offset = 7;
        camFollow.player = currentWalkingGameObject;
        //Vector3 tmp = lanepoints[currentLane].transform.position;
        //  tmp.y = 1;
        //currentWalkingCharacter.transform.position = tmp;

        mainStack.root_transform = mainStack.characters.Peek().transform;

        currentWalkingCharacter.changeToHangingAnimationState();
        GameManager.Instance.OnLose.AddListener(OnLose);
        //mainStack.appendCharacter(currentWalkingCharacter.GetComponent<Character>(), 1.3f);
        GameManager.Instance.OnHitEndLine.AddListener(OnHitEndLine);
    }

    // Update is called once per frame
    public Character[] charactersArr;

    private void FixedUpdate()
    {
        for (int i = 1; i < mainStack.characters.Count; i++)
        {
            Vector3 tmp = new Vector3(charactersArr[i].transform.position.x, charactersArr[i].transform.position.y + character_height, charactersArr[i].transform.position.z);
            charactersArr[i - 1].transform.DOMove(tmp, 0.01f, false);
        }
    }

    public void Update()
    {

        if (jumpTapTimePassed > 0)
        {
            jumpTapTimePassed -= Time.deltaTime;
        }

        if (currentWalkingCharacter)
            currentWalkingCharacter.transform.position = Vector3.Lerp(currentWalkingCharacter.transform.position, new Vector3(lanepoints[currentLane].position.x, currentWalkingCharacter.transform.transform.position.y, currentWalkingCharacter.transform.transform.position.z), 0.2f);

    }

    public float jumpHeight()
    {
        return jump_height * jumpBoost;
    }

    #region lane_controls_and_swipe
    //mostafa modification
    [ContextMenu("switchLeft")]
    public void switchLeft()
    {
        if (!LevelUI.instance.isUIOpen)
        {
            if (currentLane > 0)
            {
                Vector3 tmp = new Vector3(lanepoints[currentLane - 1].position.x, currentWalkingCharacter.transform.transform.position.y, currentWalkingCharacter.transform.transform.position.z);
                //currentWalkingCharacter.transform.DOMove(tmp, transition_time, false);
                currentLane--;
            }
        }
    }

    public void jump()
    {
        if (!LevelUI.instance.isUIOpen)
        {
            if (currentWalkingInputController.TapJump())
            {
                jumpTapTimePassed = jumpTapThreshold;
                currentWalkingCharacter.changeToJumpingAnimationState();
                StartCoroutine(playNormalJumpAudioClip());
            }
        }
    }

    IEnumerator playNormalJumpAudioClip()
    {
        yield return new WaitForSeconds(0);
        GameManager.Instance.sfxSource.clip = GameManager.Instance.normalJumpingAudioClip;
        GameManager.Instance.sfxSource.Play();
    }

    //mostafa modification
    [ContextMenu("switchRight")]
    public void switchRight()
    {
        if (!LevelUI.instance.isUIOpen)
        {
            if (currentLane < lanepoints.Count - 1)
            {
                Vector3 tmp = new Vector3(lanepoints[currentLane + 1].position.x, currentWalkingCharacter.transform.transform.position.y, currentWalkingCharacter.transform.transform.position.z);
                //currentWalkingCharacter.transform.position = tmp; //.DOMove(tmp, transition_time, false);
                currentLane++;
            }
        }
    }

    [ContextMenu("switchMid")]
    public void switchMid()
    {
        if (!LevelUI.instance.isUIOpen)
        {
            if (currentLane != lanepoints.Count / 2)
            {
                currentLane = lanepoints.Count / 2;
                Vector3 tmp = new Vector3(lanepoints[currentLane].position.x, currentWalkingCharacter.transform.transform.position.y, currentWalkingCharacter.transform.transform.position.z);
                currentWalkingCharacter.transform.DOMove(tmp, transition_time, true);
            }
            //currentLane++;
        }
    }
    #endregion


    //operations related to handling the main stack in which the player resides
    [ContextMenu("combine")]
    public void combine(Queue<Character> chars)
    {
        mainStack.appendQueue(chars, 0.6f);
    }

    public void hangFirstCharacter()
    {
        currentWalkingCharacter.changeToHangingAnimationState();
    }

    public void runFirstCharacter()
    {
        currentWalkingCharacter.changeToRunningAnimationState();
    }
    public bool isJumping()
    {
        return jumpTapTimePassed > 0f;
    }

    public void resetJumping()
    {
        jumpTapTimePassed = 0;
    }

    public void RemoveLastPlayer(bool isObstacle = false)
    {

        Character removedCharacter = mainStack.RemoveLastPlayer();
        if (removedCharacter != null)
        {
            removedCharacter.isDequeued = false;
            CameraGroupController.instance.RemoveCharacterFromCam(removedCharacter.transform);
            CamFollow.instance.RemoveCharacterFromCam(removedCharacter.transform);

            if (isObstacle)
            {
                removedCharacter.changeToDyingAnimationState();

            }
        }

        if (mainStack.characters.Count == 0)
        {
            if (isEndLineActive)
            {
                GameManager.Instance.OnWin.Invoke();
            }
            else
            {
                GameManager.Instance.OnLose.Invoke();
            }
        }
    }

    void OnLose()
    {
        print("lose");
    }

    public void OnHitEndLine()
    {
        foreach (var character in mainStack.characters)
        {
            character.isAfterEndLine = true;
        }
    }
}