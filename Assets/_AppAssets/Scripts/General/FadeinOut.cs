using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
public class FadeinOut : MonoBehaviour
{
    Renderer ren;
    void Start ()
    {
        ren = GetComponent<Renderer> ();
        ren.sharedMaterial.DOFade (0.4f, "_TintColor", 0.5f).SetLoops (-1, LoopType.Yoyo).From (.1f);

    }

    // Update is called once per frame
    void Update ()
    {

    }
}