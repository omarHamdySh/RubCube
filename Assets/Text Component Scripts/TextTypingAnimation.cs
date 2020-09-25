using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Lean.Localization;
using System;
using System.Collections.Generic;

public enum TypingTextDirection
{
    rtl,
    ltr
}
public class TextTypingAnimation : MonoBehaviour
{
    public IEnumerator currentcourtine;
    //Time taken for each letter to appear (The lower it is, the faster each letter appear)
    public float letterWritingSpeed = 0.01f;
    //Message that will displays till the end that will come out letter by letter
    private string ltrStrMessage;
    private string rtlStrMessage;

    //Text for the message to display
    public Text contentTxt;
    public Text headerTxt;
    public TypingTextDirection currentTyptingDirection;

    private LeanLocalizedText leanLocalizedTxt;
    IEnumerator TypeText()
    {
        if (currentTyptingDirection == TypingTextDirection.rtl)
        {
            #region Deprecated
            //string[] lines = ltrStrMessage.Split(
            //    new[] { "\r\n", "\r", "\n", Environment.NewLine },
            //        StringSplitOptions.None
            //        );

            //for (int i = 0; i < lines.Length - 2; i++)
            //{
            //    lines[i] += "\n";
            //}
            ////Flip the text;

            //int lineBookmark = 0;
            //List<char> charList = new List<char>();
            //List<char> charListTemp = new List<char>();
            //List<char> charListTempLn = new List<char>();

            //for (int k = 0; k < lines.Length - 1; k++)
            //{

            //    //Start the animation on the right to left direction animation.
            //    //Split each char into a char array
            //    char[] charArr = lines[k].ToCharArray();

            //    foreach (var ch in charArr)
            //    {
            //        charListTempLn.Add(ch);
            //    }


            //    foreach (var ch in charListTempLn)
            //    {
            //        if (contentTxt)
            //        {
            //            if (lineBookmark < k)
            //            {
            //                //Start putting the text in the following formula
            //                //The next line is put before the previous line.

            //            }
            //            else
            //            {
            //                charListTemp = charList;
            //                contentTxt.text = ch.ToString();
            //                contentTxt.text += charListTemp;
            //            }


            //        }

            //        //}
            //        //Add 1 letter each
            //        yield return 0;
            //        yield return new WaitForSeconds(letterWritingSpeed);
            //    }
            //}



            #endregion

            string[] lines = ltrStrMessage.Split(
                new[] { "\r\n", "\r", "\n", Environment.NewLine },
                    StringSplitOptions.None
                    );

            //for (int i = lines.Length - 2; i >=0 ; i--)
            //{
            //    lines[i] += "\n";
            //}
            //Flip the text;


            //Start the animation on the right to left direction animation.
            //Split each char into a char array
            char[] charArr = ltrStrMessage.ToCharArray();
            for (int i = charArr.Length - 1; i >= 0; i--)
            {

                string temp = contentTxt.text;
                contentTxt.text = charArr[i].ToString();
                contentTxt.text += temp;

                //Add 1 letter each
                yield return 0;
                yield return new WaitForSeconds(letterWritingSpeed);
            }

        }
        else if (currentTyptingDirection == TypingTextDirection.ltr)
        {
            //Start the animation on the default direction left to right

            //Split each char into a char array
            foreach (char letter in ltrStrMessage.ToCharArray())
            {
                if (contentTxt)
                {
                    contentTxt.text += letter;
                }
                //Add 1 letter each
                yield return 0;
                yield return new WaitForSeconds(letterWritingSpeed);
            }
        }

        leanLocalizedTxt.enabled = true;
        contentTxt = null;
    }
    public void Play(string headerStr, string massage, LeanLocalizedText leanLocalizedTxt, TypingTextDirection currentTyptingDirection)
    {
        this.currentTyptingDirection = currentTyptingDirection;
        this.leanLocalizedTxt = leanLocalizedTxt;

        if (headerTxt)
        {
            headerTxt.text = "";
            headerTxt.text = headerStr;
        }
        if (contentTxt)
        {
            contentTxt.text = "";
        }
        ltrStrMessage = "";
        ltrStrMessage = massage;
        if (currentcourtine != null)
        {
            StopCoroutine(currentcourtine);
        }
        currentcourtine = TypeText();
        StartCoroutine(currentcourtine);
    }

}