using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PassiveIncomeManager : MonoBehaviour 
{
    public float periodInSecondsCraft, amountToGetCraft, periodInSecondsFarm, amountToGetFarm, craftAmountMultiplier=1.15f, farmAmountMultiplier=1.15f, craftTimeDecrease, farmTimeDecrease, minPeriodLimitCraft, minPeriodLimitFarm;
    public GameObject movingTextSmall;
    public float distanceToMove, textSpeedMovement;
    public bool increaseCostByBigHouses=false, increaseCostByHouses=false;
    public UpgradeBuildingsManager upgradeBuildingsManager;
    private float currentAdderFarm=0, currentAdderCraft=0, farmMultipleAmount=0,  craftMultipleAmount=0;


    public void increaseIncomePerHouse(int amount)
    {
        //Debug.Log("In the right place, "+increaseCostByHouses);
        if(increaseCostByHouses)
            currentAdderCraft+=upgradeBuildingsManager.increaseIncomePerHouseBy*amount;
    }


    public void increaseIncomePerBigHouse(int amount)
    {
        //Debug.Log("In the right place, "+increaseCostByBigHouses);
        if(increaseCostByBigHouses)
            currentAdderFarm+=upgradeBuildingsManager.increaseIncomePerBigHouseBy*amount;
    }


    public void increaseIncomeFarm()
    {
        farmMultipleAmount++;
    }


    public void increaseIncomeCraft()
    {
         craftMultipleAmount++;
    }


    public float getIncomeFarm()
    {
        return (amountToGetFarm+currentAdderFarm)*(float)Math.Pow(farmAmountMultiplier, farmMultipleAmount);
    }


    public float getIncomeCraft()
    {
        return (amountToGetCraft+currentAdderCraft)*(float)Math.Pow(craftAmountMultiplier, craftMultipleAmount);
    }
}
