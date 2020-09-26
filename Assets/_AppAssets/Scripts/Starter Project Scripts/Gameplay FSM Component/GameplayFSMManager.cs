using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// define the gamepaly states
/// Transition state controls the the transition between two states
/// washing state which the player clean the teeth after bacterias attack it (teeth)
/// fighting state which the start state of the player where he/she defends opposite bacteria
/// pause state which controling the pause status for opening/closing the menu
/// </summary>
public enum GameplayState
{
    runningGameplayState,
    Pause
}

public class GameplayFSMManager : MonoBehaviour
{
    //Debug Variables
    public TextMeshProUGUI currentStateTxt;
    public TextMeshProUGUI hintTxt;
    /// <summary>
    /// Declaration of dynamic variables for surving the logic goes here.
    /// Eg.
    ///     public int chasingRange;
    ///     public int shootingRange;
    ///     public int alertRange;
    /// </summary>
    //define the stack which controlling the current state
    Stack<IGameplayState> stateStack = new Stack<IGameplayState>();

    /// <summary>
    /// Declaration of states Instances goes here.
    /// </summary>

    [HideInInspector]
    public RunningGamePlayState runningGamePlayState;
    public PauseState pauseState;
    //define a temp to know which the state the player come from it to pause state
    [HideInInspector]
    public IGameplayState tempFromPause;

    /// <summary>
    /// Declaration of references will be used for the states logic goes here
    /// Eg. 
    ///     public ISteer steeringScript;
    ///     public GameObject pathRoute;
    ///     public Queue<GameObject> enemyQueue = new Queue<GameObject>();
    /// 
    /// </summary>
    private void Start()
    {
        /// <summary>
        /// Instantiation of states Instances goes here.
        /// Eg.
        /// chaseEnemy = new ChaseState()
        ///        {
        ///     chasingRange = this.chasingRange,
        ///     shootingRange = this.shootingRange,
        ///     alertRange = this.alertRange,
        ///     movementController = this
        ///         };
        /// </summary>

        ////Instantiate the first state

        pauseState = new PauseState()
        {
            gameplayFSMManager = this
        };

        runningGamePlayState = new RunningGamePlayState()
        {
            gameplayFSMManager = this
        };


        //push the first state for the player
        PushState(pauseState);
        if (hintTxt)
        {
            hintTxt.enabled = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        stateStack.Peek().OnStateUpdate();
    }
    /// <summary>
    /// functions to define the stak functionality
    /// </summary>
    public void PopState()
    {
        if (stateStack.Count > 0)
            stateStack.Pop().OnStateExit();
    }
    public void PushState(IGameplayState newState)
    {
        newState.OnStateEnter();
        stateStack.Push(newState);

        if (currentStateTxt)
            currentStateTxt.text = stateStack.Peek().ToString();
    }


    /// <summary>
    /// functions to defining how changing the gameplay state
    /// </summary>
    ///
    public void changeToAState(GameplayState toState) {
        switch (toState)
        {
            case GameplayState.Pause:
                pauseGame();
                break;
            case GameplayState.runningGameplayState:
                toRunningState();
                break;
            default:
                break;
        }
    }
    public void toRunningState()
    {
        PopState();
        PushState(runningGamePlayState);
    }

    public void pauseGame()
    {
        if (tempFromPause == null)
        {
            tempFromPause = stateStack.Peek();
            PopState();
            PushState(pauseState);
        }

    }
    public void resumeGame()
    {
        if (tempFromPause != null)
        {
            PopState();
            PushState(tempFromPause);
            tempFromPause = null;
        }
    }

    //return the current state at the stack
    public GameplayState getCurrentState()
    {
        return stateStack.Peek().GetState();
    }

}
