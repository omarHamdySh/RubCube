using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;

public class LevelUI : SceneHandller
{
    #region Singleton
    public static LevelUI instance { private set; get; }

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public bool isUIOpen = true;
    [SerializeField] private Slider loading;

    private void Start()
    {

    }

    public void ToggleISUIOpen(bool enabeld)
    {
        isUIOpen = enabeld;
    }


    #region Main
    #endregion

    #region Gameplay
    [Header("Gameplay")]
    [SerializeField] private TextMeshProUGUI timerTxt;
    [SerializeField] private Vector3 timerTextAnimScale;
    [SerializeField] private float timerDelayAnim;
    [SerializeField] private string timerLastSecondsColor = "red";
    [SerializeField] private TextMeshProUGUI scoreTxt, bestScoreTxt;

    private bool isTimerAnimationOn;
    //[SerializeField] private float timer; for test
    private Coroutine timerAnimCoroutine;

    //private void Update()
    //{
    //    SetTimerText(timer);
    //}

    public void SetTimerText(float timer)
    {
        if (!isTimerAnimationOn && timer <= 10f && timer >= 0)
        {
            isTimerAnimationOn = true;
            timerAnimCoroutine = StartCoroutine(TimerAnimation());
        }

        if (timer <= 0 || (isTimerAnimationOn && timer > 10f))
        {
            isTimerAnimationOn = false;
            timerTxt.transform.localScale = Vector3.one;
            StopCoroutine(timerAnimCoroutine);
        }

        var timespan = TimeSpan.FromSeconds(timer);

        if (timer <= 10f && timer >= 0)
        {
            timerTxt.text = "Timer\n" + "<color=\"" + timerLastSecondsColor + "\">" + timespan.ToString(@"ss\:ff") + "</color>";
        }
        else
        {
            timerTxt.text = "Timer\n" + timespan.ToString(@"ss\:ff");
        }
    }

    IEnumerator TimerAnimation()
    {
        while (true)
        {
            yield return new WaitUntil(() => !isUIOpen);
            timerTxt.transform.DOScale(timerTextAnimScale, timerDelayAnim);
            yield return new WaitForSeconds(timerDelayAnim);
            timerTxt.transform.DOScale(Vector3.one, timerDelayAnim);
            yield return new WaitForSeconds(timerDelayAnim);
        }
    }

    public void SetScoreText(int score)
    {
        scoreTxt.text = "Score\n" + score;
    }

    public void SetBestScoreText(int bestScore)
    {
        bestScoreTxt.text = "Best Score\n" + bestScore;
        PlayerPrefs.SetInt(ImportantData.LevelScore + SceneManager.GetActiveScene().buildIndex, bestScore);
    }
    #endregion

    #region WinMenu
    [Header("WinMenu")]
    [SerializeField] private Canvas winCanvas;

    public void OpenWinCanvas()
    {
        isUIOpen = true;
        winCanvas.enabled = true;
        ZUIManager.Instance.OpenMenu(winCanvas.transform.GetChild(0).name);
    }
    #endregion

    #region LoseMenu
    [Header("LoseMenu")]
    [SerializeField] private Canvas loseCanvas;

    public void OpenLoseCanvas()
    {
        isUIOpen = true;
        loseCanvas.enabled = true;
        ZUIManager.Instance.OpenMenu(loseCanvas.transform.GetChild(0).name);
    }
    #endregion
}
