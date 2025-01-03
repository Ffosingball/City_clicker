using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScaler : MonoBehaviour
{

    public Text[] buttonText, bigText, smallText;
    public Text coinsNum;
    public int screenSizeDivider=50; 
    public float coinsTextSize=1.8f, smallTextSize=0.7f;
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

        foreach (Text buttonText1 in buttonText){
            buttonText1.fontSize = textSize;
        }

        foreach (Text bigText1 in bigText){
            bigText1.fontSize = (int)(textSize*coinsTextSize);
        }

        foreach (Text smallText1 in smallText){
            smallText1.fontSize = (int)(textSize*smallTextSize);
        }

        if(coinsNum!=null)
            coinsNum.fontSize = (int)(textSize*coinsTextSize);
        //curTextSize=(int)(textSize*coinsTextSize);
    }
}
