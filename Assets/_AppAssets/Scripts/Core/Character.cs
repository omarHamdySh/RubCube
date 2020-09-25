using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public delegate void CharacterAnimationStateEvent(Character character);
    public CharacterAnimationStateEvent OnAnimationStateIdleHanging;
    public CharacterAnimationStateEvent OnAnimationStateIdleGrounded;
    public CharacterAnimationStateEvent OnAnimationStateHanging;
    public CharacterAnimationStateEvent OnAnimationStateRunning;
    public CharacterAnimationStateEvent OnAnimationStateJumping;
    public CharacterAnimationStateEvent OnAnimationStateDying;
    public CharacterAnimationStateEvent OnAnimationStateExitingStack;
    public enum CharacterAnimationState
    {
        IdleHanging,
        IdleGrounded,
        Hanging,
        Running,
        Jumping,
        Dying,
        ExitingStack
    }

    public CharacterGender currentGender;
    public CharacterAnimationState animationState;
    public GameplayColor currentCharacterColor;
    public SkinnedMeshRenderer meshRenderer;
    [SerializeField] private Animator myAnim;
    public bool isAfterEndLine;
    internal void changeToIdleGroundedAnimationState()
    {
        animationState = CharacterAnimationState.IdleGrounded;
        if (OnAnimationStateIdleGrounded != null)
            OnAnimationStateIdleGrounded.Invoke(this);
        myAnim.SetBool("IsIdle", true);
    }

    public void changeToIdleHangingAnimationState()
    {
        animationState = CharacterAnimationState.IdleHanging;
        if (OnAnimationStateIdleHanging != null)
            OnAnimationStateIdleHanging.Invoke(this);
        myObstacle.SetActive(false);
    }

    public void changeToExitingAnimationState()
    {
        animationState = CharacterAnimationState.ExitingStack;
        if (OnAnimationStateExitingStack != null)
            OnAnimationStateExitingStack.Invoke(this);
        myObstacle.SetActive(false);

    }

    public void changeToHangingAnimationState()
    {
        animationState = CharacterAnimationState.Hanging;
        if (OnAnimationStateHanging != null)
            OnAnimationStateHanging.Invoke(this);
        myAnim.SetBool("IsIdle", false);
        myAnim.SetBool("IsHangging", true);
        myObstacle.SetActive(false);

    }

    public void changeToRunningAnimationState()
    {
        animationState = CharacterAnimationState.Running;
        if (OnAnimationStateRunning != null)
            OnAnimationStateRunning.Invoke(this);
        myAnim.SetBool("IsIdle", false);
        myAnim.SetBool("IsHangging", false);
        myObstacle.SetActive(false);

    }

    public void changeToDyingAnimationState()
    {
        //I guess this will be done by activating a ragdoll for the character, ask marwan for the best approach
        animationState = CharacterAnimationState.Dying;
        if (OnAnimationStateDying != null)
            OnAnimationStateDying.Invoke(this);
        //changeToIdleGroundedAnimationState();////to remove later
        myAnim.SetBool("IsDead", true);
        myObstacle.SetActive(false);
        StartCoroutine(playCoinsCharacterHitSound());
    }

    public void changeToJumpingAnimationState()
    {
        animationState = CharacterAnimationState.Jumping;
        if (OnAnimationStateDying != null)
            OnAnimationStateDying.Invoke(this);
        myObstacle.SetActive(false);

    }

    public void changeColorTo(GameplayColor color)
    {
        currentCharacterColor = color;
        GameManager.Instance.changeModelToColor(this, currentCharacterColor, currentGender);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Wall => Before end line & After it.
            //Character
        }
        OnCollide(collision.gameObject);

    }


    private void OnTriggerEnter(Collider other)
    {
        OnCollide(other.gameObject);
    }


    private float timer;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            timer += Time.deltaTime;

            if (timer > 0.2f)
            {
                if (this == LevelManager.Instance.currentWalkingCharacter)
                {
                    transform.position = transform.position + Vector3.forward * 5;
                }


                timer = 0;
            }

        }
    }


    private void OnCollisionExit(Collision collision)
    {
        timer = 0;
    }

    private void OnCollide(GameObject collideObject)
    {
        switch (collideObject.tag)
        {
            case "Player":
                break;
            case "Wall":
                {
                    if (!isDequeued)
                    {
                        LevelManager.Instance.RemoveLastPlayer();
                    }

                    // todo dye
                    if (!isAfterEndLine)
                    {
                        changeToDyingAnimationState();
                    }

                    else
                        changeToIdleGroundedAnimationState();
                    collideObject.GetComponent<Collider>().enabled = false;//todo
                    this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    break;
                }
            case "EndLine":
                //changeToExitingAnimationState();
                isAfterEndLine = true;
                LevelManager.Instance.isEndLineActive = true;
                GameManager.Instance.OnHitEndLine.Invoke();
                break;
            case "TreasureBox":
                GameManager.Instance.OnWin.Invoke();
                //changeToExitingAnimationState();
                changeToIdleGroundedAnimationState();
                break;
            case "UncollectedPlayer":
                break;
        }
    }

    [HideInInspector] public bool isDequeued;
    [SerializeField] GameObject myObstacle;

    IEnumerator playCoinsCharacterHitSound()
    {
        yield return new WaitForSeconds(0);
        if (currentGender == CharacterGender.Male)
        {
            GameManager.Instance.sfxSource.clip = GameManager.Instance.maleHurtAudioClip[UnityEngine.Random.Range(0, 3)];
        }
        else
        {
            GameManager.Instance.sfxSource.clip = GameManager.Instance.femaleHurtAudioClip[UnityEngine.Random.Range(0, 3)];
        }
        GameManager.Instance.sfxSource.Play();
    }
}
