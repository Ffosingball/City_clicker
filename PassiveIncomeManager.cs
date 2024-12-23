using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveIncomeManager : MonoBehaviour 
{
    public float periodInSecondsCraft, amountToGetCraft, periodInSecondsFarm, amountToGetFarm, craftAmountMultiplier, farmAmountMultiplier, craftTimeDecrease, farmTimeDecrease, minPeriodLimitCraft, minPeriodLimitFarm;
    public GameObject movingTextSmall;
    public float distanceToMove, textSpeedMovement;
    public bool increaseCostByBigHouses=false, increaseCostByHouses=false;
    public UpgradeBuildingsManager upgradeBuildingsManager;
    private float currentMultipliarFarm=1, currentAdderFarm=0, currentMultipliarCraft=1, currentAdderCraft=0;


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
        currentMultipliarFarm+=farmAmountMultiplier;
    }


    public void increaseIncomeCraft()
    {
        currentMultipliarCraft+=craftAmountMultiplier;
    }


    public float getIncomeFarm()
    {
        return (amountToGetFarm+currentAdderFarm)*currentMultipliarFarm;
    }


    public float getIncomeCraft()
    {
        return (amountToGetCraft+currentAdderCraft)*currentMultipliarCraft;
    }
}
