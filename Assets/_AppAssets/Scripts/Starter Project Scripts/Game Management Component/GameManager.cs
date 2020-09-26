using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;


// The levels that player have to pass
//Must intialize ASAP  ****************************************IMPORTANT*****************************

public enum GameplayColor
{
    Red,
    Yellow,
    Green,
}

public enum CharacterGender
{
    Male,
    Female
}


/// <summary>
/// Event payload
/// </summary>
/// <param name="timeUnitValue"></param>
public delegate void TimeEvents(float timeUnitValue);
public delegate bool GamePlayStatesEvents(IGameplayState otherState);

[RequireComponent(typeof(GameplayFSMManager), typeof(TimeManager))]

public class GameManager : MonoBehaviour
{
    #region Custom inspector attributes
    public static int turnsRemaining = 3;

    [Header("Managers")]
    //UI elements
    public GameplayFSMManager gameplayFSMManager;                       //reference for the state machine controller to access his state
    public TimeManager timeManager;

    [Header("Audio")]
    //background music
    public AudioSource musicSource;
    private AudioClip currentMusicClip;
    public List<AudioClip> musicList;

    //sfx
    public AudioSource sfxSource;
    public AudioClip ElectricShockSound;
    public AudioClip ShapePopulationSound;
    public AudioClip bouncHitAudioClip;
    public AudioClip characterCombinationAudioClip;
    public List<AudioClip> maleHurtAudioClip;
    public List<AudioClip> femaleHurtAudioClip;
    public AudioClip coinCollectionAudioClip;
    public AudioClip colorChangerAudioClip;
    //public 


    #endregion

    #region GameManger Data Memebers 

    [Header("Gameplay Attributes")]

    private static GameManager _Instance;                               //reference for this script to access it from another place to manage/control his variables and function
    public event TimeEvents OnRealSecondChanged;
    public event TimeEvents OnRealMinuteChanged;
    public event TimeEvents OnGameHourChanged;
    public event TimeEvents OnGameDayChanged;

    public GamePlayStatesEvents OnTransitionHaveToEnd;
    public GamePlayStatesEvents OnGamePlayPause;

    #endregion

    #region Gameplay DataMembers

    [Header("Gameplay Attributes")]
    //LevelManager
    public bool isTesting;
    //-------------------------------------------
    public string playerName;
    public int currentLevel;
    public CharacterGender playerGender;
    public List<Material> cubesMaterials;


    public int gold = 0, crystal = 0;
    private bool isStart, isFinished;

    [Header("Default Static Behavior")]
    public GameplayColor firstCharacterColor;

    #endregion

    #region Level Events
    public UnityEvent StartLevel;
    public UnityEvent OnWin;
    public UnityEvent OnLose;
    public UnityEvent PlayerDeath;
    public UnityEvent OnHitEndLine;
    #endregion

    public static GameManager Instance
    {
        get { return _Instance; }
    }

    private void Awake()
    {
        /** Order of methods calling is critical**/
        /*if (_Instance == null)
        {
            _Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }*/
        _Instance = this;


        ///DontDestroyOnLoad(this.gameObject);
        gameplayFSMManager = GetComponent<GameplayFSMManager>();
        timeManager = GetComponent<TimeManager>();
        SceneManager.sceneLoaded += delegate { OnSceneLoad(); };
    }



    /// <summary>
    ///     Check which game play it is, and position the player at that position.
    ///     Also change the weapon item according to the player choice
    /// </summary>

    public void OnSceneLoad()
    {

    }

    private void PlaySFX(AudioClip sfx)
    {
        if (sfxSource != null && !sfxSource.isPlaying)
            sfxSource.PlayOneShot(sfx);
    }

    public void PlayMusic(AudioClip music)
    {
        if (musicSource != null && music != null)
        {
            musicSource.clip = music;
            musicSource.Play();
        }
    }

    #region Public GamePlay Methods







    public GameplayColor RandomizeGamePlayColor()
    {
        return (GameplayColor)UnityEngine.Random.Range(0, 3);
    }

    //internal GameObject fetchForCharacterModelPrefab(string name)
    //{
    //    foreach (GameObject gameObj in characterModelsPrefabs)
    //    {
    //        if (gameObj.name.ToLower().Equals(name.ToLower()))
    //        {
    //            gameObj.transform.position = Vector3.zero;
    //            gameObj.transform.rotation = Quaternion.identity;
    //            return gameObj;
    //        }
    //    }
    //    return null;
    //}



    #endregion

    #region  Gameplay States Global Methods 
    /// <summary>
    /// function to pause the scene and all the live scripts in the scene
    /// </summary>
    public void PauseGame()
    {
        gameplayFSMManager.pauseGame();
        #region --deperacted Code
        ///
        /// this code make all scene the scene stop and also the controller functionality
        /// Time.timeScale = 0;
        ///
        #endregion
    }
    /// <summary>
    /// /// <summary>
    /// function to resume the scene and all the live scripts in the scene
    /// </summary>
    /// </summary>
    public void ResumeGame()
    {
        gameplayFSMManager.resumeGame();
        #region --deperacted Code
        ///
        /// this code make all scene the scene stop and also the controller functionality
        /// Time.timeScale = 1;
        ///
        #endregion
    }

    public void resetGame()
    {

    }

    public void StartGame()
    {
        StartLevel.Invoke();
        isStart = true;
    }

    /// <summary>
    /// Proceed Transition which means that go to what ever game play state you were 
    /// going before the transition.
    /// </summary>
    public void proceedTransition()
    {
        PauseState nullObj = null;
        bool result = OnTransitionHaveToEnd(nullObj);

        if (result)
        {
            StartCoroutine(turnOfStateFSMHit());
        }

    }

    /// <summary>
    /// Just For the sample scene purpose showing the data reflecting the 
    /// GamePlay FSM component details and how it works.
    /// </summary>
    /// <returns></returns>
    IEnumerator turnOfStateFSMHit()
    {
        yield return new WaitForSeconds(3);
        if (gameplayFSMManager.hintTxt.enabled)
        {
            gameplayFSMManager.hintTxt.enabled = false;
            gameplayFSMManager.hintTxt.text = "";
            gameplayFSMManager.hintTxt.color = Color.white;
        }
    }
    #endregion

    #region Time Periods Related Events 
    /// <summary>
    /// This method will be called by the time manager each second
    /// Functionality:
    ///     it fires the OnSecondChanged Event which will fire every and each method
    ///     That is listening to that event
    /// </summary>

    public void OnSecondChange()
    {
        OnRealSecondChanged(timeManager.gameTime.realSecond);
    }

    /// <summary>
    /// This method will be called by the time manager each minute.
    /// Functionality:
    ///     it fires the OnMinuteChanged Event which will fire every and each method
    ///     That is listening to that event
    /// </summary>
    public void OnMinuteChange()
    {
        OnRealMinuteChanged(timeManager.gameTime.realMinute);
    }

    /// <summary>
    /// This method will be called by the time manager each GameDay.
    /// Functionality:
    ///     it fires the OnGameDayChanged Event which will fire every and each method
    ///     That is listening to that event
    /// </summary>
    public void OnGameDayChange()
    {
        OnGameDayChanged(timeManager.gameTime.gameDay);
    }

    /// <summary>
    /// This method will be called by the time manager each GameHour.
    /// Functionality:
    ///     it fires the OnGameHourChanged Event which will fire every and each method
    ///     That is listening to that event
    /// </summary>
    public void OnGameHourChange()
    {
        OnGameHourChanged(timeManager.gameTime.gameHour);
    }
    #endregion

    #region Other Methods
    public void showMouseCursor()
    {
        Cursor.visible = true;
    }

    public void hideMouseCursor()
    {
        Cursor.visible = false;
    }

    #endregion

    #region Deprecated Leveling code
    /**
    public enum GameLevel {//Must be declared out of the class;
           Level1,
           Level2,
           Level3
    }
     * 
    /// <summary>
    /// this methods returns the index of specific level in the enum
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public int GetLevelIndex(GameLevel level)
    {
        var states = Enum.GetValues(typeof(GameLevel));
        foreach (var item in states)
        {
            if ((GameLevel)item == level)
            {
                return (int)item;
            }
        }
        return -1;
    }
    public void MoveToTheNextLevel()
    {
        currentLevel = (GameLevel)GetLevelIndex(currentLevel) + 1;
        LevelManager.Instance.incrementEnemySpwanTime();

        if (currentLevelInfoLabel)
            currentLevelInfoLabel.text = currentLevel.ToString();

        switch (currentLevel)
        {
            case GameLevel.Level1:
                LevelManager.Instance.enemySpeed = LevelManager.Instance.level1EnemySpeed;

                break;
            case GameLevel.Level2:
                LevelManager.Instance.enemySpeed = LevelManager.Instance.level2EnemySpeed;
                break;
            case GameLevel.Level3:
                LevelManager.Instance.enemySpeed = LevelManager.Instance.level3EnemySpeed;
                break;
            default:
                break;
        }
        if (speedEnemyInfoLabel)
            speedEnemyInfoLabel.text = LevelManager.Instance.enemySpeed.ToString();
    }
    **/
    #endregion

    #region PlayerProgress Methods



    #endregion


}
