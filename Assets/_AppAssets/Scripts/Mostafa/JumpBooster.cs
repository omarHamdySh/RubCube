using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class JumpBooster : MonoBehaviour
{
    public Queue<Character> characters;
    public Lane lane;
    public float jump_boost;
    public Character lastInQueue;// the last character in the queue
    public float charHeight = 2;
    public bool collisionHappened = false;//will be disabled once the collision is detected 
    public ParticleSystem JumpBoostingEffect1;
    private void Start()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (!collisionHappened)
        {
            if (LevelManager.Instance.isJumping())
            {
                collisionHappened = true;
                LevelManager.Instance.resetJumping();
                
                if (LevelManager.Instance.mainStack.characters.Peek().currentCharacterColor == lane.laneColor)
                {
                    other.GetComponent<PhysicsData>().jumpHeight = jump_boost * other.GetComponent<PhysicsData>().defaultJumpHeight;


                    Vector3 positionOfLast = lastInQueue.transform.position;
                    //foreach (Character C in characters)
                    //{
                    //    positionOfLast = C.transform.position;
                    //}
                    JumpBoostingEffect1.Play();
                    StartCoroutine(playBoostedJumpAudioClip());
                    positionOfLast.y += GameManager.Instance.oneCharacterHeightJumpPowerUnit;
                    other.transform.DOMove(positionOfLast, 1 - (1 / (jump_boost + 1)), false).onComplete += combine;
                }
                //GetComponent<Collider>().enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<PhysicsData>())
        other.GetComponent<PhysicsData>().jumpHeight = other.GetComponent<PhysicsData>().defaultJumpHeight;
    }

    private void combine()
    {
        CameraGroupController.instance.AddCharacterQueue(characters);
        CamFollow.instance.AddCharacterQueue(characters);
        LevelManager.Instance.combine(characters);
        LevelManager.Instance.camFollow.FollowStackOfPlayers();
        StartCoroutine(playCoinsStackCombinationSound());
        //LevelManager.Instance.mainStack.OnNewCharacterAdded.Invoke();
        lane.combinationVFX.Play();
    }

    IEnumerator playCoinsStackCombinationSound()
    {
        yield return new WaitForSeconds(0);
        GameManager.Instance.sfxSource.clip = GameManager.Instance.characterCombinationAudioClip;
        GameManager.Instance.sfxSource.Play();
    }

    IEnumerator playBoostedJumpAudioClip()
    {
        yield return new WaitForSeconds(0);
        GameManager.Instance.sfxSource.clip = GameManager.Instance.boostedJumpingAudioClip;
        GameManager.Instance.sfxSource.Play();
    }
}
