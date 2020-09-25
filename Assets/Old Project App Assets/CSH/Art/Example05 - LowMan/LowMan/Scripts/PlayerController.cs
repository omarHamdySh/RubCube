using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
public class PlayerController : MonoBehaviour
{
    public ThirdPersonCharacter character;

    private void Start()
    {
        character = GetComponent<ThirdPersonCharacter>();
    }

    private void Update()
    {
        //character.Move(transform.forward * 100f, false, true) ;
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("jump");
            //Move with a specific velocity (Direction and speed)
            character.Move(Vector3.zero, false, true);
        }
        

    }
}
