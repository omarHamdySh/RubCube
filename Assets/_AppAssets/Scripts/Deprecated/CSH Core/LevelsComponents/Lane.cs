using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.vCharacterController;
using System;

public class Lane : MonoBehaviour
{
    [Header("Color Attributes")]
    public GameplayColor laneColor;
    
    [Header("JumpingSpot Attributes")]
    public GameObject JumpingSpotSpawnPoint;

    [Header("Characters Attributes")]
    public Queue<Character> spawnedCharactersQueue;
    public Transform charactersSpawnSpot;
    public GameObject Character;
    public ParticleSystem combinationVFX;
    private void Awake()
    {
        spawnedCharactersQueue = new Queue<Character>();
    }

    [ContextMenu("SpwanLane")]
    public void Init()
    {
        SpawnCharactersRandomly();
        SetJumpingSpotPower();
    }

    public void SpawnCharactersRandomly()
    {
        int charactersNoToSpawn = UnityEngine.Random.Range(1, 1);

        Queue<Character> characterQueue = new Queue<Character>();
        Character lastInQueue = null; 
        for (int i = 0; i < charactersNoToSpawn; i++)
        {

            CharacterGender gender = (CharacterGender)UnityEngine.Random.Range(0,2);
            var characterObj = Instantiate(GameManager.Instance.GetCharacterPrefab(laneColor,gender), charactersSpawnSpot.transform);

            characterObj.transform.position = new Vector3(
                                                  charactersSpawnSpot.position.x,
                                                  (i * GameManager.Instance.oneCharacterHeightJumpPowerUnit) + charactersSpawnSpot.position.y,
                                                  charactersSpawnSpot.position.z
                                                 );

            if (i+1 == charactersNoToSpawn)
            {
                Vector3 vfxPos = new Vector3(
                    characterObj.transform.position.x,
                    ((i+1) * GameManager.Instance.oneCharacterHeightJumpPowerUnit) + characterObj.transform.position.y,
                    characterObj.transform.position.z
                    );
                combinationVFX = Instantiate(GameManager.Instance.GetCombinationVFXObject(laneColor)).GetComponent<ParticleSystem>(); ;
                combinationVFX.transform.position = vfxPos;
                combinationVFX.transform.parent = characterObj.transform;
            }

            Character character = characterObj.GetComponent<Character>();
            character.OnAnimationStateHanging += hangBodycallback;
            character.OnAnimationStateRunning += runBodycallback;
            characterObj.GetComponent<vThirdPersonInput>().enabled = false;
 
            if (i != 0)
            {
                character.OnAnimationStateIdleHanging += OnCharacterAimationStateIdleHanging;
                character.changeToIdleHangingAnimationState();
            }
            else
            {
                character.OnAnimationStateIdleGrounded += OnCharacterAnimationStateIdleGrounded;
                character.changeToIdleGroundedAnimationState();
            }


            /********************************************************/
            GameManager.Instance.changeModelToColor(character, laneColor, character.currentGender);

            lastInQueue = characterObj.GetComponent<Character>();
            characterQueue.Enqueue(lastInQueue);
            character.currentCharacterColor = laneColor;


        }
        SpawnJumpingSpotPower(characterQueue, lastInQueue);
        spawnedCharactersQueue = characterQueue;

        foreach (var character in spawnedCharactersQueue)
        {
            character.transform.SetGlobalScale(Vector3.one);
        }
        //GroupedCharacters.transform.parent = JumpingSpot.transform;

    }

    private void OnCharacterAimationStateIdleHanging(Character character)
    {
        character.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void OnCharacterAnimationStateIdleGrounded(Character character)
    {
        character.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void SpawnJumpingSpotPower(Queue<Character> charQ, Character lastInQueue)
    {
        var JumpingSpotPower = Instantiate(SetJumpingSpotPowerColor(), JumpingSpotSpawnPoint.transform);
        JumpingSpotPower.transform.position = new Vector3(JumpingSpotSpawnPoint.transform.position.x, JumpingSpotSpawnPoint.transform.position.y, JumpingSpotSpawnPoint.transform.position.z);
        var jumpSpot = JumpingSpotPower.GetComponent<JumpBooster>();
        jumpSpot.characters = charQ;
        jumpSpot.lastInQueue = lastInQueue;
        jumpSpot.jump_boost = charQ.Count;
        jumpSpot.lane = this;

    }

    public void SetJumpingSpotPower()
    {
        //JumpingSpotSpawnPoint.jumpHeight = GameManager.Instance.oneCharacterHeightJumpPowerUnit * spawnedCharactersList.Count;
    }

    public GameObject SetJumpingSpotPowerColor()
    {
        if (laneColor == GameplayColor.Red)
            return GameManager.Instance.jumpBoostersPrefab[0];
        else if (laneColor == GameplayColor.Green)
            return GameManager.Instance.jumpBoostersPrefab[1];
        else
            return GameManager.Instance.jumpBoostersPrefab[2];
    }




    #region statecall back
    public void hangBodycallback(Character character)
    {
        character.GetComponent<vThirdPersonInput>().forward_movement = 0.0f;
        //character.GetComponent<vThirdPersonController>().useRootMotion = false;
        character.GetComponent<Rigidbody>().isKinematic = true;
        character.GetComponent<Rigidbody>().useGravity = false;
        character.GetComponent<vThirdPersonInput>().enabled = false;
    }

    public void runBodycallback(Character character)
    {
        //this isn't being called for some reason
        character.GetComponent<vThirdPersonInput>().forward_movement = 1.0f;
        //character.GetComponent<vThirdPersonController>().useRootMotion = true;
        character.GetComponent<Rigidbody>().isKinematic = false;
        character.GetComponent<Rigidbody>().useGravity = true;
        character.GetComponent<vThirdPersonInput>().enabled = true;
    }
    #endregion
}
