using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color_Swap_Manager : MonoBehaviour
{
    public static Color_Swap_Manager inst;
    public Color indicatorColor;
    public List<Color_Palet> Color_Paltes = new List<Color_Palet> ();
    public List<Material> blocksMaterials = new List<Material>();
    void Awake ()
    {
        inst = this;
    }

}

[System.Serializable]
public class Color_Palet
{
    [SerializeField]
    public List<Gradient> Color_Paltes = new List<Gradient> ();
}