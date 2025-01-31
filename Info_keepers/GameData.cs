using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class GameData
{
    public List<Structure> usualBuildingsList, natureObjects;
    public List<RoadStructure> roadList;
    public List<PassiveIncomeStructure> specialBildingsList;
    public float balance, adder, multiplierExponent, houseCost, bigHouseCost, farmCost, craftCost;
    public int levelFarm, levelCraft, numOfBigHouses, numOfHouses, numOfFarms, numOfCrafts;
    public float passIncAdderFarm, passIncMultiplierFarm, passIncTimeFarm, passIncAdderCraft, passIncMultiplierCraft, passIncTimeCraft, farmUpgradeCost, craftUpgradeCost;
    public bool increasedIncome1, increasedIncome2, decreasedCost1, decreasedCost2;

    public GameData(float balance, float adder, float multiplierExponent)
    {
        this.balance = balance;
        this.adder = adder;
        this.multiplierExponent = multiplierExponent;
    }


    public void setBuildingsLists(List<Structure> usualBuildingsList, List<RoadStructure> roadList, List<PassiveIncomeStructure> specialBildingsList)
    {
        this.roadList = roadList;
        this.specialBildingsList = specialBildingsList;
        this.usualBuildingsList = usualBuildingsList;
    }


    public void setBuildingsInfo(float houseCost, float bigHouseCost, float farmCost, float craftCost, int levelFarm, int levelCraft, int numOfBigHouses, int numOfHouses, int numOfFarms, int numOfCrafts)
    {
        this.houseCost=houseCost;
        this.bigHouseCost=bigHouseCost;
        this.farmCost=farmCost;
        this.craftCost=craftCost;
        this.levelFarm=levelFarm;
        this.levelCraft=levelCraft;
        this.numOfBigHouses=numOfBigHouses;
        this.numOfFarms=numOfFarms;
        this.numOfCrafts=numOfCrafts;
        this.numOfHouses = numOfHouses;
    }


    public void setPassiveIncomeInfo(float passIncAdderFarm, float passIncMultiplierFarm, float passIncTimeFarm, float passIncAdderCraft, float passIncMultiplierCraft, float passIncTimeCraft)
    {
        this.passIncAdderFarm=passIncAdderFarm;
        this.passIncMultiplierFarm=passIncMultiplierFarm;
        this.passIncTimeFarm=passIncTimeFarm;
        this.passIncAdderCraft=passIncAdderCraft;
        this.passIncMultiplierCraft=passIncMultiplierCraft;
        this.passIncTimeCraft=passIncTimeCraft;
    }


    public void setUpgradeInfo(bool increasedIncome1, bool increasedIncome2, bool decreasedCost1, bool decreasedCost2, float farmUpgradeCost, float craftUpgradeCost)
    {
        this.increasedIncome1=increasedIncome1;
        this.increasedIncome2=increasedIncome2;
        this.decreasedCost1=decreasedCost1;
        this.decreasedCost2=decreasedCost2;
        this.farmUpgradeCost = farmUpgradeCost;
        this.craftUpgradeCost = craftUpgradeCost;
    }


    public void setNatureObjectsInfo(List<Structure> natureObjects)
    {
        this.natureObjects=natureObjects;
    }
}
