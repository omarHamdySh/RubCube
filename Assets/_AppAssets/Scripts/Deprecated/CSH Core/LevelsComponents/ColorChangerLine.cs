using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ColorChangerLine : MonoBehaviour
{
    public GameplayColor colorChangerColor;

    public GameplayColor RandomizeGamePlayColor()
    {
        return (GameplayColor)Random.Range(0, 3);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CharacterStack characterStack = LevelManager.Instance.mainStack;

            foreach (var character in characterStack.characters)
            {
                character.changeColorTo(colorChangerColor);
            }

            GameManager.Instance.sfxSource.clip = GameManager.Instance.colorChangerAudioClip;
            GameManager.Instance.sfxSource.Play();

        }
    }
}
