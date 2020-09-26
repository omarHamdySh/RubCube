using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningGamePlayState : IGameplayState
{
    GameplayState stateName = GameplayState.runningGameplayState;

    public GameplayFSMManager gameplayFSMManager;


    public void OnStateEnter()
    {
        GameManager.Instance.OnGamePlay.Invoke();
    }

    public void OnStateExit()
    {

    }

    public void OnStateUpdate()
    {

    }
    string ToString()
    {
        return stateName.ToString();
    }

    public GameplayState GetState()
    {
        return stateName;
    }
}
