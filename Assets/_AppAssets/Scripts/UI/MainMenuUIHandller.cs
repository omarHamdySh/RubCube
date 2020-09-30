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

    public void restartAfter(int delay)
    {
        StartCoroutine(restart(delay));
    }

    IEnumerator restart(int delay)
    {

        yield return new WaitForSeconds(delay);
        RestartLevel();
    }
}
