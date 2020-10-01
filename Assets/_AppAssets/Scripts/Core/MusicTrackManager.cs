using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrackManager : MonoBehaviour
{
    public static MusicTrackManager instnce;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instnce != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instnce = this;
            DontDestroyOnLoad(this);
        }

    }

}
