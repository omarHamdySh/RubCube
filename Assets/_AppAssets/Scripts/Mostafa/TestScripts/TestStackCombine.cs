using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStackCombine : MonoBehaviour
{

    public CharacterStack mainStack;

    public CharacterStack testStack;

    [ContextMenu("combine")]
    public void combine()
    {
        //mainStack.appendChracterStack(testStack, 1.5f);
        mainStack.appendQueue(testStack.characters, 1.5f);
    }
}
