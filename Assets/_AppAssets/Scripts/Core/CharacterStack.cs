
using Invector.vCharacterController;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterStack : MonoBehaviour
{
    public Queue<Character> characters;
    public Transform root_transform;
    public float characterHeight;

    public void Start()
    {
        characters = new Queue<Character>();
    }

    public void Init()
    {
        addCharacter();
    }

    public void addToTheStack(List<Character> newCharacters)
    {
        for (int i = newCharacters.Count - 1; i >= 0; i--)
        {
            //characters.Enqueue(newCharacters[i]);

            //if (i != 0)
            //    characters.Peek().changeToHangingAnimationState();
            //else
            //    characters.Peek().changeToRunningAnimationState();
        }
    }


    public void removeOneFormCharacterStack()
    {
        characters.Dequeue().changeToExitingAnimationState();
    }

    //adds new stack to the current stack 
    //public void appendChracterStack(CharacterStack cstack, float height)
    //{
    //    characterHeight = height;
    //    //concatenate stacks
    //    foreach (Character C in characters)
    //    {
    //        cstack.characters.Enqueue(C);

    //        //remove later
    //        C.OnAnimationStateHanging += hangBodycallback;
    //        C.OnAnimationStateRunning += runBodycallback;
    //    }
    //    characters = cstack.characters;
    //    Destroy(cstack.gameObject);

    //    align();
    //}

    //public void appendCharacter(Character character, float height)
    //{

    //    characterHeight = height;
    //    characters.Prepend(character);

    //    //remove later
    //    character.OnAnimationStateHanging += hangBodycallback;
    //    character.OnAnimationStateRunning += runBodycallback;


    //    align();
    //}

    public void appendQueue(Queue<Character> newCharacters, float height)
    {
        Queue<Character> final = new Queue<Character>();

        while (newCharacters.Count > 0)
        {
            newCharacters.Peek().transform.parent = transform;
            final.Enqueue(newCharacters.Dequeue());

        }

        //concatenate stacks
        foreach (Character character in characters)
        {
            final.Enqueue(character);

            //remove later
            character.OnAnimationStateHanging += hangBodycallback;
            character.OnAnimationStateRunning += runBodycallback;

        }

        characters = final;
        LevelManager.Instance.charactersArr = characters.ToArray();
        Array.Reverse(LevelManager.Instance.charactersArr);

        align();
    }


    [ContextMenu("align")]
    //put objects in proper position
    public void align()
    {
        int i = 0;
        root_transform.position = characters.Peek().transform.position;
        foreach (Character C in characters)
        {
            C.transform.position = new Vector3(root_transform.position.x, (i * characterHeight) + root_transform.position.y, root_transform.position.z);
            i++;

            //disable useless stuff
            C.changeToHangingAnimationState();
            C.gameObject.tag = "dummy";
        }


        LevelManager.Instance.currentWalkingCharacter = characters.Peek();
        LevelManager.Instance.currentWalkingInputController = characters.Peek().GetComponent<vThirdPersonInput>();
        if (LevelManager.Instance.camFollow != null)
            LevelManager.Instance.camFollow.player = characters.Peek().gameObject;

        characters.Peek().changeToRunningAnimationState();
        characters.Peek().gameObject.tag = "Player";
    }

    #region debugging_functions



    [ContextMenu("testStack")]
    public void foo()
    {
        Stack<int> ints = new Stack<int>();

        ints.Push(1);
        ints.Push(2);
        ints.Push(3);

        foreach (int i in ints)
        {
            Debug.Log(i);
        }
    }


    [ContextMenu("pop")]
    public void pop()
    {
        Destroy(characters.Dequeue().gameObject);
    }

    [ContextMenu("addDummy")]
    public void addCharacter()
    {
        GameObject obj = Instantiate(GameManager.Instance.GetTheVeryFirstCharacter(), root_transform.position, root_transform.rotation, transform);
        var C = obj.GetComponent<Character>();
        obj.transform.position = root_transform.position;
        obj.transform.rotation = root_transform.rotation;
        characters.Enqueue(C);
        C.OnAnimationStateHanging += hangBodycallback;
        C.OnAnimationStateRunning += runBodycallback;
        //obj.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0.1f, 1), Random.Range(0.1f, 1), Random.Range(0.1f, 1));
        align();
    }
    #endregion

    //statecall back
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
        character.GetComponent<vThirdPersonInput>().forward_movement = 1f;
        //character.GetComponent<vThirdPersonController>().useRootMotion = true;
        character.GetComponent<Rigidbody>().isKinematic = false;
        character.GetComponent<Rigidbody>().useGravity = true;
        character.GetComponent<vThirdPersonInput>().enabled = true;
    }

    public Character RemoveLastPlayer()
    {
        if (characters.Count == 0)
        {
            return null;
        }

        Character removedCharacter = characters.Dequeue();
        removedCharacter.changeToExitingAnimationState();
        LevelManager.Instance.currentWalkingCharacter = null;
        LevelManager.Instance.currentWalkingInputController = null;
        removedCharacter.gameObject.tag = "dummy";
        removedCharacter.transform.SetParent(null);
        if (!removedCharacter.isAfterEndLine)
        {
            Destroy(removedCharacter.gameObject, 2f);
        }


        if (characters.Count > 0)
        {
            LevelManager.Instance.currentWalkingCharacter = characters.Peek();
            LevelManager.Instance.currentWalkingInputController = characters.Peek().GetComponent<vThirdPersonInput>();
            characters.Peek().changeToRunningAnimationState();
            characters.Peek().gameObject.tag = "Player";
        }
        else
        {
            LevelManager.Instance.currentWalkingCharacter = null;
            LevelManager.Instance.currentWalkingInputController = null;
        }

        LevelManager.Instance.charactersArr = characters.ToArray();
        Array.Reverse(LevelManager.Instance.charactersArr);
        return removedCharacter;

    }


}
