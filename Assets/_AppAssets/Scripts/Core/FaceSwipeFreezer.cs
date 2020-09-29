using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceSwipeFreezer : MonoBehaviour
{
    public SwipeControl.FacesSwipes faceSwipe;
    public SwipeControl swipeControl;
    
    public void ApplyFreezeConditions()
    {
        swipeControl.freezeThemAllButThis(faceSwipe);
    }
}
