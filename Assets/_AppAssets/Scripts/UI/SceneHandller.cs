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
    /// Load the MainMenu Scene with loading
    /// </summary>
    /// <param name="loading">loading Slider</param>
    public void BackToMainMenu(Slider loading)
    {
        LoadLevel(0, loading);
    }

    /// <summary>
    /// Load the MainMenu Scene without loading
    /// </summary>
    public void BackToMainMenu()
    {
        LoadLevel(0);
    }

    /// <summary>
    /// Restart the current Scene with loading
    /// </summary>
    /// <param name="loading">loading Slider</param>
    public void RestartLevel(Slider loading)
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex, loading);
    }

    /// <summary>
    /// Restart the current Scene without loading
    /// </summary>
    public void RestartLevel()
    {
        LoadLevel(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Load Next Level in build setting with loading
    /// </summary>
    /// /// <param name="loading">loading Slider</param>
    public void LoadNextLevel(Slider loading)
    {
        int nextLevelIndex = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        //nextLevelIndex = nextLevelIndex == 0 ? nextLevelIndex + 1 : nextLevelIndex;
        LoadLevel(nextLevelIndex, loading);
    }

    /// <summary>
    /// Load Next Level in build setting without loading
    /// </summary>
    public void LoadNextLevel()
    {
        int nextLevelIndex = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        nextLevelIndex = nextLevelIndex == 0 ? nextLevelIndex + 1 : nextLevelIndex;
        LoadLevel(nextLevelIndex);
    }


    /// <summary>
    /// Moving from scene to another
    /// </summary>
    /// <param name="scencName">Name of the scene you will go to</param>
    /// <param name="loading">progress bar</param>
    public void LoadLevel(string scencName, Slider loading = null)
    {
        if (loading)
        {
            StartCoroutine(LoadAsynchronously(scencName, loading));
        }
        else
        {
            SceneManager.LoadScene(scencName);
        }
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
        if (loading)
        {
            StartCoroutine(LoadAsynchronously(scencNum, loading));
        }
        else
        {
            SceneManager.LoadScene(scencNum);
        }
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
