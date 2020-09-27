using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerConroller : MonoBehaviour
{
    #region Singleton
    public static TimerConroller Instance { private set; get; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private float timerSeconds, timerMins, timerHours;
    private UnityEvent OnTimerEnd;
    private OnTimerValueChangedFloat onTimerValueChangedFloat;
    private bool isTimerOn;
    private int prevSecond;
    private bool isCountingUp = false;

    private LevelUI levelUI;

    private void Start()
    {
        if (LevelUI.instance)
        {
            levelUI = LevelUI.instance;
        }
    }

    public void StartTimer(float seconds, UnityAction<float> timerValueChangedFloat, UnityAction endTimerCallBack)
    {
        timerSeconds = seconds;

        OnTimerEnd = new UnityEvent();
        OnTimerEnd.AddListener(endTimerCallBack);

        onTimerValueChangedFloat = new OnTimerValueChangedFloat();
        onTimerValueChangedFloat.AddListener(timerValueChangedFloat);

        prevSecond = (int)timerSeconds;

        isTimerOn = true;
    }

    void FixedUpdate()
    {
        if (!levelUI) return;
        if (levelUI.isUIOpen) return;

        if (isTimerOn && !isCountingUp)
        {
            if (timerSeconds > 0)
            {
                timerSeconds -= Time.deltaTime;
                onTimerValueChangedFloat.Invoke(timerSeconds);

                if (timerSeconds <= 0)
                {
                    if (timerMins > 0)
                    {
                        timerMins--;
                        timerSeconds = 60f;
                    }
                    else if (timerHours > 0)
                    {
                        timerHours--;
                        timerMins = 59f;
                        timerSeconds = 60f;
                    }
                }
            }
            else
            {
                isTimerOn = false;
                OnTimerEnd.Invoke();
            }
        }
    }
}

[System.Serializable]
public class OnTimerValueChangedFloat : UnityEvent<float> { }