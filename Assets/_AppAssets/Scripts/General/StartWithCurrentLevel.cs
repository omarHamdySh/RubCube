using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartWithCurrentLevel : SceneHandller
{
    [SerializeField] private Slider loading;
    private void Start()
    {
        int currentLevelIndex = PlayerPrefs.GetInt(ImportantData.CurrentLevel, 1);
        LoadLevel(currentLevelIndex, loading);
    }
}
