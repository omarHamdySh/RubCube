using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class LevelEndLine : MonoBehaviour
{
    public enum EndLineType{
        simple,
        complex
    }

    [Header("End Line Type Section")]
    public EndLineType endLineType;

    [Header("End Line Events Section")]
    public UnityEvent OnPlayerWins;
    public UnityEvent OnPlayerLoses;
    public void declarePlayerWinning()
    {
        OnPlayerWins.Invoke();
    }

    public void declarePlayerLoss()
    {
        OnPlayerLoses.Invoke();
    }
    private void OnTriggerEnter(Collider other)
    {
        //if simple end

        //else if complex end that has a condition in order to declare success or failure.
    }


}
