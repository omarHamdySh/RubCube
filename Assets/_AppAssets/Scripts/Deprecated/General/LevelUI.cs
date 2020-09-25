using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Globalization;

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

    [SerializeField] private TextMeshProUGUI[] goldTxts;
    [SerializeField] private TextMeshProUGUI[] crystalTxts;
    [SerializeField] private TextMeshProUGUI currentLevelTxt, nextLevelTxt;
    [SerializeField] private Slider currentLevelProgress;
    [SerializeField] private TextMeshProUGUI LevelIndecator;

    public bool isUIOpen = true;
    private void Start()
    {
        int gold = PlayerPrefs.GetInt(ImportantData.gold, 0);
        int crystal = PlayerPrefs.GetInt(ImportantData.crystal, 0);
        GameManager.Instance.gold = gold;
        GameManager.Instance.crystal = crystal;
        SetGold(gold);
        SetCrystal(crystal);
        LevelIndecator.text = "Level " + SceneManager.GetActiveScene().buildIndex;
    }

    public void SetGold(int gold)
    {
        PlayerPrefs.SetInt(ImportantData.gold, gold);
        string goldString = gold.ToString();
        NumberFormatInfo setPrecision = new NumberFormatInfo();
        setPrecision.NumberDecimalDigits = 1;
        if (gold / 1000000000 > 0)
        {
            float percentGold = gold / 1000000000.0f;
            goldString = percentGold.ToString("N", setPrecision) + "B";
        }
        else if (gold / 1000000 > 0)
        {
            float percentGold = gold / 1000000.0f;
            goldString = percentGold.ToString("N", setPrecision) + "M";
        }
        else if (gold / 1000 > 0)
        {
            float percentGold = gold / 1000.0f;
            goldString = percentGold.ToString("N", setPrecision) + "K";
        }

        foreach (TextMeshProUGUI text in goldTxts)
        {
            text.text = goldString;
        }
    }

    public void SetCrystal(int crystal)
    {
        PlayerPrefs.SetInt(ImportantData.crystal, crystal);
        string crystalString = crystal.ToString();
        NumberFormatInfo setPrecision = new NumberFormatInfo();
        setPrecision.NumberDecimalDigits = 1;
        if (crystal / 1000000000 > 0)
        {
            float percentCrystal = crystal / 1000000000.0f;
            crystalString = percentCrystal.ToString("N", setPrecision) + "B";
        }
        else if (crystal / 1000000 > 0)
        {
            float percentCrystal = crystal / 1000000.0f;
            crystalString = percentCrystal.ToString("N", setPrecision) + "M";
        }
        else if (crystal / 1000 > 0)
        {
            float percentCrystal = crystal / 1000.0f;
            crystalString = percentCrystal.ToString("N", setPrecision) + "K";
        }

        foreach (TextMeshProUGUI text in crystalTxts)
        {
            text.text = crystalString;
        }
    }

    public void SetCurrentLevelProgress(float progress)
    {
        currentLevelProgress.value = progress;
    }

    public float GetCurrentLevelProgress()
    {
        return currentLevelProgress.value;
    }


    public void ToggleISUIOpen(bool enabeld)
    {
        isUIOpen = enabeld;
    }

}
