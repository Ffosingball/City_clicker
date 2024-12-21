using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftHouseBehaviour : MonoBehaviour
{
    private float timeLeft;
    public PassiveIncomeManager passiveIncomeManager;

    void Start()
    {
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

            Balance.increaseBalance(passiveIncomeManager.amountToGetCraft);
        }
    }
}
