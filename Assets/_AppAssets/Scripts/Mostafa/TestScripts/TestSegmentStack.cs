using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSegmentStack : MonoBehaviour
{
    public Queue<Character> characters;
    public Character char_from_inspector;

    private void Start()
    {
        characters = new Queue<Character>();

        characters.Enqueue(char_from_inspector);
        //foreach (Character C in chars_from_inspector)
        //{
        //    characters.Enqueue(C);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("logged");
            LevelManager.Instance.combine(characters);
        }
    }
    


}
