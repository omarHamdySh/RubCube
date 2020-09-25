using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;

public class FB_EventsHandler : MonoBehaviour
{
    /**
     * Include the Facebook namespace via the following code:
     * using Facebook.Unity;
     *
     * For more details, please take a look at:
     * developers.facebook.com/docs/unity/reference/current/FB.LogAppEvent
     */
    public static FB_EventsHandler instance;
    private void Awake()
    {
        
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
        else
        {
            //Handle FB.Init
            FB.Init(() =>
            {
                FB.ActivateApp();
            });
        }
        instance = this;
    }
    public void LogLevels_AchievedEvent()
    {
        FB.LogAppEvent(
            "Levels_Achieved"
        );
    }

    public void LogLevels_AchievedEvent(int level_no)
    {
        var parameters = new Dictionary<string, object>();
        parameters["level_no"] = level_no;
        FB.LogAppEvent(
            "levels_achieved",
            1,
            parameters
        );
    }


    // Unity will call OnApplicationPause(false) when an app is resumed
    // from the background
    void OnApplicationPause(bool pauseStatus)
    {
        // Check the pauseStatus to see if we are in the foreground
        // or background
        if (!pauseStatus)
        {
            //app resume
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
            }
            else
            {
                //Handle FB.Init
                FB.Init(() =>
                {
                    FB.ActivateApp();
                });
            }
        }
    }
}
