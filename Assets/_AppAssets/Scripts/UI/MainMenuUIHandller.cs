using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIHandller : SceneHandller
{
    [SerializeField] private Slider loading;

    private int currentLevelIndex;

    private void Start()
    {
        currentLevelIndex = PlayerPrefs.GetInt(ImportantData.CurrentLevel, 1);
    }

    public void LoadCurrentLevel()
    {
        LoadLevel(currentLevelIndex, loading);
    }
}
