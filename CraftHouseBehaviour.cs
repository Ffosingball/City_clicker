using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CraftHouseBehaviour : MonoBehaviour
{
    [HideInInspector]
    public float timeLeft;
    private Vector3 coords;
    public PassiveIncomeManager passiveIncomeManager;

    void Start()
    {
        Transform transform= GetComponent<Transform>();
        coords = transform.position;
        StartCoroutine(givePassiveIncome());
    }


    private IEnumerator givePassiveIncome()
    {
        while(true)
        {
            timeLeft=passiveIncomeManager.periodInSecondsCraft;

            while(timeLeft>0)
            {
                timeLeft-=0.2f;
                yield return new WaitForSeconds(0.2f);
            }

            Balance.increaseBalance(passiveIncomeManager.getIncomeCraft());
            createText();
        }
    }


    private void createText()
    {
        coords.z=2;
        GameObject movingTextCur = Instantiate(passiveIncomeManager.movingTextSmall, coords, Quaternion.Euler(0f,0f,0f));
        //Find child gameObject of the created gameObject by its name
        GameObject childCanva = movingTextCur.transform.Find("MovingText").gameObject;
        //And again
        GameObject childText = childCanva.transform.Find("howMuchEarned").gameObject;
        Text textNeeded = childText.GetComponent<Text>();
        textNeeded.text="+"+Balance.outputCostCorrectly((float)Math.Round(passiveIncomeManager.getIncomeCraft()));
        StartCoroutine(moveText(movingTextCur));
    }


    private IEnumerator moveText(GameObject movingTextCur)
    {
        double distanceMoved=0;
        while(distanceMoved<passiveIncomeManager.distanceToMove)
        {
            movingTextCur.transform.Translate(new Vector3(0,1,0)*passiveIncomeManager.textSpeedMovement*Time.deltaTime);
            yield return new WaitForSeconds(0.02f); 
            distanceMoved+=1*passiveIncomeManager.textSpeedMovement*Time.deltaTime;
        }
        Destroy(movingTextCur);
    }
}
