using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NouranTest : MonoBehaviour
{
    public Character Character;
    public GameplayColor color;

    [ContextMenu("Test")]
    public void test()
    {
        GameManager.Instance.changeModelToColor(Character, Character.currentCharacterColor, Character.currentGender);
    }

    
}
