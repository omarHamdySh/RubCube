using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
public class FadeinOut : MonoBehaviour
{
    Renderer ren;
    public Color FadeColor;
    void Start ()
    {
        ren = GetComponent<Renderer>();
        ren.sharedMaterial.DOFade (0.2f, "_TintColor", 0.5f).SetLoops (-1, LoopType.Yoyo).From (.05f);

    }

    public void updateColor(Color color)
    {
        FadeColor = color;
        ren = GetComponent<Renderer>();
        ren.sharedMaterial.SetColor("_TintColor", FadeColor);
    }


}