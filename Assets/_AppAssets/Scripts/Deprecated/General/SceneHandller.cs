using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneHandller : MonoBehaviour
{
    /// <summary>
    /// Exit the game
    /// </summary>
    public void QuitTheGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Load the MainMenu Scene
    /// </summary>
    public void BackToMainMenu()
    {
        StartCoroutine(LoadAsynchronously(0));
    }

    /// <summary>
    /// Restart the current Scene
    /// </summary>
    public void RestartLevel()
    {
        StartCoroutine(LoadAsynchronously(SceneManager.GetActiveScene().name));
    }

    public void LoadNextLevel(Slider loading = null)
    {
        int nextLevelIndex = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        nextLevelIndex = nextLevelIndex == 0 ? nextLevelIndex + 1 : nextLevelIndex;
        LoadLevel(nextLevelIndex, loading);
    }

    /// <summary>
    /// Moving from scene to another
    /// </summary>
    /// <param name="scencName">Name of the scene you will go to</param>
    /// <param name="loading">progress bar</param>
    public void LoadLevel(string scencName, Slider loading = null)
    {
        StartCoroutine(LoadAsynchronously(scencName, loading));
    }

    private IEnumerator LoadAsynchronously(string scencName, Slider loading = null)
    {
        yield return new WaitForSeconds(0.5f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(scencName);
        while (!operation.isDone)
        {
            if (loading)
            {
                loading.value = operation.progress;
            }

            yield return null;
        }
    }

    /// <summary>
    /// Moving from scene to another
    /// </summary>
    /// <param name="scencNum">index of the scene you will go to</param>
    /// <param name="loading">progress bar</param>
    public void LoadLevel(int scencNum, Slider loading = null)
    {
        StartCoroutine(LoadAsynchronously(scencNum, loading));
    }

    private IEnumerator LoadAsynchronously(int scencNum, Slider loading = null)
    {
        yield return new WaitForSeconds(0.5f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(scencNum);
        while (!operation.isDone)
        {
            if (loading)
            {
                loading.value = operation.progress;
            }

            yield return null;
        }
    }
}
