using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class changeButtonHighlight : MonoBehaviour
{
    private Image im;
    public Sprite orignal, highlight, clicked;
    public bool image_or_Color;
    // Use this for initialization
    void Start()
    {
        im = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!image_or_Color)
        {
            {
                im.sprite = highlight;
            }

            {
                im.sprite = clicked;
            }
            {
                im.sprite = orignal;
            }

        }
        
        else
        {
            {
                im.color = Color.yellow;
            }

            {
                im.color = Color.green;
            }
            {

                im.color = Color.white;
            }
            {

                im.color = Color.white;
            }
        }
    }

}

