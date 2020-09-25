using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLanguage : MonoBehaviour
{
    public LeanLocalization ll;

    //public void toggleLanguage() {
    //    if (LeanLocalization.CurrentLanguage.ToLower().Equals("arabic"))
    //    {
    //    }
    //    else if (LeanLocalization.CurrentLanguage.ToLower().Equals("english")) {
    //    }
    //}

    public void SwitchToArabic()
    {
        ll.SetCurrentLanguage("Arabic");
    }

    public void SwitchToEnglish()
    {
        ll.SetCurrentLanguage("English");
    }
}
