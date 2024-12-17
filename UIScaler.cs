using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScaler : MonoBehaviour
{

    public Text[] buttonText;
    public int screenSizeDivider=50;
    private int previousHeight;

    void Start()
    {
        ResizeUI();
        previousHeight=Screen.height;
    }


    // Check whether we need to resize font or not
    void FixedUpdate()
    {
        if (previousHeight != Screen.height)
        {
            previousHeight = Screen.height;
            ResizeUI();
        }
    }

    //This method calculate new font size which is based on the height
    private void ResizeUI(){
        int textSize = Screen.height / screenSizeDivider;

        foreach (Text buttonText in buttonText){
            buttonText.fontSize = textSize;
        }
    }
}
