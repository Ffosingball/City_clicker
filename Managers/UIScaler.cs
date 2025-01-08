using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScaler : MonoBehaviour
{

    public Text[] buttonText, bigText, smallText;
    public Text coinsNum;
 

    void Start()
    {
        ResizeUI();
    }


    //This method calculate new font size which is based on the height
    public void ResizeUI()
    {
        foreach (Text buttonText1 in buttonText){
            //Debug.Log("Done");
            buttonText1.fontSize = SettingsInfo.middleTextSize;
        }

        foreach (Text bigText1 in bigText){
            bigText1.fontSize = SettingsInfo.bigTextSize;
        }

        foreach (Text smallText1 in smallText){
            smallText1.fontSize = SettingsInfo.smallTextSize;
        }

        if(coinsNum!=null)
            coinsNum.fontSize = SettingsInfo.bigTextSize;
    }
}
