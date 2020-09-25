using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialInGame : MonoBehaviour
{


    [Multiline]
    [SerializeField] private string tutorialTxt;
    [SerializeField] private GameObject tutorialBtn;
    [SerializeField] private GameObject bg;

    private bool isTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(ImportantData.PlayerTag) && !isTriggered)
        {
            isTriggered = false;
            StopTime();
            bg.SetActive(true);
            tutorialBtn.SetActive(true);
            tutorialBtn.GetComponentInChildren<TextMeshProUGUI>().text = tutorialTxt;
        }
    }

    private void StopTime()
    {
        Time.timeScale = 0;
    }

    public void StartTime()
    {
        Time.timeScale = 1;
    }
}
