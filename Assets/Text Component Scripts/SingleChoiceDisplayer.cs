using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SingleChoiceDisplayer : MonoBehaviour
{

    public List<SingleChoiceDisplayer> paragraphChoices;
    [HideInInspector]
    public Text thisChoiceTxt;
    [HideInInspector]
    public LeanLocalizedText leanLocalizedTxt;


    public GameObject paragraphTile; //Needs to be renamed to paragraph Content but no time to repeat passing the object in inspector.
    [HideInInspector]
    public Animator highlightAnimator;
    public Image BG;
    private Color normalColor = new Color(255,255,255);
    private TextTypingAnimation typingAnimator;
    // Start is called before the first frame update
    void Start()
    {
        thisChoiceTxt = paragraphTile.GetComponentInChildren<Text>();
        leanLocalizedTxt = paragraphTile.GetComponentInChildren<LeanLocalizedText>();
        typingAnimator = GetComponentInParent<TextTypingAnimation>();
        highlightAnimator = BG.GetComponentInParent<Animator>();
    }


    public void enableThisChoice(SingleChoiceDisplayer singleChoice)
    {

        singleChoice.disableAllChoicesButThis();
        singleChoice.enableThis(singleChoice);

    }

    private void enableThis(SingleChoiceDisplayer singleChoice) {
        //Enable the highlight of the this choice.
        highlightAnimator.enabled = true;

        //Disable the tile game object preparing for starting the typing animation
        paragraphTile.SetActive(true);


        //Before the animation is about to work disable the ability to change the language.
        // code goes here

        //Disable the lean Localized Text script before doing anything.
        leanLocalizedTxt.enabled = false;

        //Take or cut the text out of the text component and pass it to the typing text animator.
        typingAnimator.contentTxt = thisChoiceTxt;


        string temp = thisChoiceTxt.text;
        thisChoiceTxt.text = "";
        //Animate the text and display it.
        typingAnimator.Play("", temp, leanLocalizedTxt,
            (LeanLocalization.CurrentLanguage.ToLower().Equals("arabic"))?TypingTextDirection.rtl: TypingTextDirection.ltr
           );

        //Empty the text object of the typing text animator.
        //typingAnimator.contentTxt = null;

        //After finishing the animation turn the the LeanLocalizedScript On back again.
        //leanLocalizedTxt.enabled = true;

        //After the animation is finished enable the ability to change the language.
        // code goes here

    }
    public void disableAllChoicesButThis() {
        //call their disableThisChoiceMethod():
        foreach (var item in paragraphChoices)
        {
            if (item!= this)
            {
                item.disableThisChoice();
            }
        }
    }
    public void disableThisChoice() {

        //Disable the highlight of this choice.
        highlightAnimator.enabled = false;
        //BG.color = normalColor;

        //Disable the tile game object that holds the current choice header.
        paragraphTile.SetActive(false);
    }
}
