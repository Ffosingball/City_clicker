using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScaler : MonoBehaviour
{

    public Text[] buttonText;
    public Text coinsText, coinsNum;
    public int screenSizeDivider=50; 
    public float coinsTextSize=1.8f;
    private int previousHeight;
    //public static int curTextSize;

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

        coinsText.fontSize = (int)(textSize*coinsTextSize);
        coinsNum.fontSize = (int)(textSize*coinsTextSize);
        //curTextSize=(int)(textSize*coinsTextSize);
    }
}
