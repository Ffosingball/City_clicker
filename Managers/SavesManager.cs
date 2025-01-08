using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SavesManager : MonoBehaviour
{
    public BuildingsManager buildingsManager;
    public UpgradeBuildingsManager upgradeBuildingsManager;
    public PassiveIncomeManager passiveIncomeManager;
    public MapGenerator mapGenerator;
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


    public void deleteSave(int saveNumber)
    {
        saveNames[saveNumber]="";
        SaveSystem.DeleteSave(saveNumber);
        SaveSystem.SaveNames(saveNames);
    }


    public void SaveTo(int saveNumber)
    {
        // Get the current date and time
        DateTime currentDateTime = DateTime.Now;
        saveNames[saveNumber] = currentDateTime + "";

        GameData data = new GameData(Balance.getBalance(), Balance.getAdder(), Balance.getAmountToMultiply());

        List<RoadStructure> tempRoadList = new List<RoadStructure>();
        foreach(KeyValuePair<string, List<RoadStructure>> roadStructures in buildingsManager.allRoadsList)
        {
            foreach(RoadStructure road in roadStructures.Value)
            {
                tempRoadList.Add(road);
            }
        }

        data.setBuildingsLists(buildingsManager.allBuildings, tempRoadList, buildingsManager.allPassiveIncomesBuilds);
        data.setBuildingsInfo(buildingsManager.houseCost,buildingsManager.bigHouseCost,buildingsManager.farmCost,buildingsManager.craftCost,upgradeBuildingsManager.farmUpgrades,upgradeBuildingsManager.craftUpgrades,buildingsManager.numOfBigHouses,buildingsManager.numOfHouses,buildingsManager.numOfFarms,buildingsManager.numOfCraft);
        data.setUpgradeInfo(upgradeBuildingsManager.incomeIncreased1,upgradeBuildingsManager.incomeIncreased2,upgradeBuildingsManager.costDecreased1,upgradeBuildingsManager.costDecreased2,upgradeBuildingsManager.farmUpgradeCost,upgradeBuildingsManager.craftUpgradeCost);
        data.setPassiveIncomeInfo(passiveIncomeManager.currentAdderFarm,passiveIncomeManager.farmMultipleAmount,passiveIncomeManager.periodInSecondsFarm,passiveIncomeManager.currentAdderCraft,passiveIncomeManager.craftMultipleAmount,passiveIncomeManager.periodInSecondsCraft);
        
        List<Structure> tempNatureObjects = new List<Structure>();
        foreach(KeyValuePair<GameObject, Structure> natureStructures in mapGenerator.allNatureStructures)
        {
            tempNatureObjects.Add(natureStructures.Value);
        }
        
        data.setNatureObjectsInfo(tempNatureObjects);
    
        SaveSystem.SaveGame(saveNumber, data);
        SaveSystem.SaveNames(saveNames);
    }


    public void LoadFrom(int saveNumber)
    {
        GameData data = SaveSystem.LoadGame(saveNumber);
        //Debug.Log("Length: "+data.getUsualBuildingsList().Count);

        foreach(GameObject tempObject in TempObjects.tempObjectsList)
        {
            if(tempObject!=null)
                Destroy(tempObject);
        }
        TempObjects.tempObjectsList.Clear();

        passiveIncomeManager.resetPassiveIncome(data);
        upgradeBuildingsManager.resetUpgrades(data);
        buildingsManager.resetBuildings(data);
        mapGenerator.resetNature(data);
        resetBalance(data);

        //Debug.Log("Loaded");
    }


    public void resetBalance(GameData data)
    {
        Balance.updateBalance(Balance.getBalance());
        Balance.increaseBalance(data.balance);
        Balance.setAdder(data.adder);
        Balance.setAmountToMultiply(data.multiplierExponent);
    }


    public void LoadSettings()
    {
        SettingsInfoKeeper info = SaveSystem.LoadSettings();
        if (info == null)
        {
            SettingsInfo.playMusicForMainButton = false;
            SettingsInfo.playForIncome = false;
            SettingsInfo.musicVolume = 1f;
            SettingsInfo.soundEffectsVolume = 1f;
        }
        else
        {
            SettingsInfo.setNewSettings(info);
        }
    }


    public void SaveSettings()
    {
        SaveSystem.SaveSettings();
    }
}
