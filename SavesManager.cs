using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SavesManager : MonoBehaviour
{
    public BuildingsManager buildingsManager;
    public UpgradeBuildingsManager upgradeBuildingsManager;
    public PassiveIncomeManager passiveIncomeManager;
    private string[] saveNames;


    private void Start()
    {
        saveNames = SaveSystem.LoadNames();
        if(saveNames==null)
        {
            saveNames = new string[5];
            for(int i=0; i<saveNames.Length; i++)
            {
                saveNames[i] = "";
            }
        }
    }


    public bool checkSaves(int saveNumber)
    {
        if(saveNames[saveNumber]=="")
            return false;
        else
            return true;
    }


    public string getSaveName(int saveNumber)
    {
        return saveNames[saveNumber];
    }


    public void SaveTo(int saveNumber)
    {
        // Get the current date and time
        DateTime currentDateTime = DateTime.Now;
        saveNames[saveNumber] = currentDateTime + "";

        GameData data = new GameData(Balance.getBalance(), Balance.getAdder(), Balance.getAmountToMultiply());

        List<RoadStructure> tempRoadList = new List<RoadStructure>();
        //if()
        foreach(KeyValuePair<string, List<RoadStructure>> roadStructures in buildingsManager.allRoadsList)
        {
            foreach(RoadStructure road in roadStructures.Value)
            {
                tempRoadList.Add(road);
            }
        }

        data.setBuildingsLists(buildingsManager.allBuildings, tempRoadList, buildingsManager.allPassiveIncomesBuilds);
    
        Debug.Log("Length: "+data.getUsualBuildingsList().Count);

        data.setBuildingsInfo(buildingsManager.houseCost,buildingsManager.bigHouseCost,buildingsManager.farmCost,buildingsManager.craftCost,upgradeBuildingsManager.farmUpgrades,upgradeBuildingsManager.craftUpgrades);
        data.setUpgradeInfo(upgradeBuildingsManager.incomeIncreased1,upgradeBuildingsManager.incomeIncreased2,upgradeBuildingsManager.costDecreased1,upgradeBuildingsManager.costDecreased2);
        data.setPassiveIncomeInfo(passiveIncomeManager.currentAdderFarm,passiveIncomeManager.farmMultipleAmount,passiveIncomeManager.periodInSecondsFarm,passiveIncomeManager.currentAdderCraft,passiveIncomeManager.craftMultipleAmount,passiveIncomeManager.periodInSecondsCraft);
    
        SaveSystem.SaveGame(saveNumber, data);
        SaveSystem.SaveNames(saveNames);
    }


    public void LoadFrom(int saveNumber)
    {
        GameData data = SaveSystem.LoadGame(saveNumber);

        Debug.Log("Length: "+data.getUsualBuildingsList().Count);

        Debug.Log("Loaded");
    }
}