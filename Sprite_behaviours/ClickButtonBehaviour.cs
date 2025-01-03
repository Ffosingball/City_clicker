using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ClickButtonBehaviour : MonoBehaviour
{
    public Text coinsNum;
    public GameObject movingText, mainButton;
    public int xOffset=0, yOffset=20;
    public float textSpeedMovement=50, textDistanceMove=100, interval=1f;
    public SavesManager savesManager;
    private int clicks=0;
    private float timePassed=0;


    void Start()
    {
        TempObjects.tempObjectsList = new List<GameObject>();

        Balance.updateBalance(Balance.getBalance());
        Balance.setAdder(0);
        Balance.setAmountToMultiply(0);
    }


    void Update()
    {
        if(TempObjects.loadSave)
        {
            savesManager.LoadFrom(TempObjects.saveNum);
            TempObjects.loadSave=false;
        }

        timePassed+= Time.deltaTime;
       //Debug.Log(Time.deltaTime);

        if(timePassed>=interval && clicks>0)
        {
            Debug.Log("Clicks done: "+clicks);
            clicks=0;
            timePassed=0;
        }
        else if(timePassed>=interval)
            timePassed=0;
        
        TempObjects.tempObjectsList.RemoveAll(item => item == null);
    }


    public void increaseBalance()
    {
        float increasedBy = Balance.updateBalance();

        RectTransform rectTransform = mainButton.GetComponent<RectTransform>();
        Vector3 worldCenter = rectTransform.TransformPoint(rectTransform.rect.center);
        worldCenter.z=0.09f;
        worldCenter.y+=yOffset;
        worldCenter.x+=xOffset;

        GameObject movingTextCur = Instantiate(movingText, worldCenter, Quaternion.Euler(0f,0f,0f));
        //Find child gameObject of the created gameObject by its name
        GameObject childCanva = movingTextCur.transform.Find("MovingText").gameObject;
        //And again
        GameObject childText = childCanva.transform.Find("howMuchEarned").gameObject;
        Text textNeeded = childText.GetComponent<Text>();
        if(textNeeded==null)
            Debug.Log("Null");
        else
        {
            textNeeded.text="+"+Balance.outputCostCorrectly((float)Math.Round(increasedBy));
            StartCoroutine(moveText(movingTextCur));
        }
    }


    private IEnumerator moveText(GameObject movingTextCur)
    {
        double distanceMoved=0;
        while(distanceMoved<textDistanceMove)
        {
            movingTextCur.transform.Translate(new Vector3(0,1,0)*textSpeedMovement*Time.deltaTime);
            yield return null; 
            distanceMoved+=1*textSpeedMovement*Time.deltaTime;
        }
        Destroy(movingTextCur);
    }


    public void countClicks()
    {
        clicks++;
    }
}