using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupedCharactersQueue : MonoBehaviour
{
    public Queue<Character> spawnedCharactersQueue = new Queue<Character>();

    void Start()
    {
        
    }

    public void AddCharacter(Character character)
    {
        spawnedCharactersQueue.Enqueue(character);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager.Instance.combine(spawnedCharactersQueue);
        }
    }
}
