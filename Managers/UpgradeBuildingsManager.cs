using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBuildingsManager : MonoBehaviour
{
    public PassiveIncomeManager passiveIncomeManager;
    public BuildingsManager buildingsManager;
    public float costMultiplier = 1.5f, craftUpgradeCost=40000, farmUpgradeCost=100000, upgradeIncomeFromBuild1Cost=100000, upgradeIncomeFromBuild2Cost=1000000, decreaseCost1=200000, decreaseCost2=500000, decreaseCostBy=5, increaseIncomePerHouseBy=100, increaseIncomePerBigHouseBy=10;
    public Text craftUpgradeCostText, farmUpgradeCostText, craftLevelText, farmLevelText, upgradeIncomeText1, upgradeIncomeText2, decreaseCostText1, decreaseCostText2;
    public Button craftUpgradeButton, farmUpgradeButton, upgradeIncomeButton1, upgradeIncomeButton2, decreaseCostButton1, decreaseCostButton2;
    [HideInInspector]
    public int craftUpgrades=0, farmUpgrades=0;
    [HideInInspector]
    public bool incomeIncreased1=false, incomeIncreased2=false, costDecreased1=false, costDecreased2=false;
    public SoundManager soundManager;


    private void Start()
    {
        resetUI();
    }


    private void FixedUpdate()
    {
        if(Balance.getBalance()>=craftUpgradeCost)
            craftUpgradeButton.interactable = true;
        else
            craftUpgradeButton.interactable = false;

        if(Balance.getBalance()>=farmUpgradeCost)
            farmUpgradeButton.interactable = true;
        else
            farmUpgradeButton.interactable = false;

        if(Balance.getBalance()>=upgradeIncomeFromBuild1Cost && !incomeIncreased1)
            upgradeIncomeButton1.interactable = true;
        else
            upgradeIncomeButton1.interactable = false;

        if(Balance.getBalance()>=upgradeIncomeFromBuild2Cost && !incomeIncreased2)
            upgradeIncomeButton2.interactable = true;
        else
            upgradeIncomeButton2.interactable = false;

        if(Balance.getBalance()>=decreaseCost1 && !costDecreased1)
            decreaseCostButton1.interactable = true;
        else
            decreaseCostButton1.interactable = false;

        if(Balance.getBalance()>=decreaseCost2 && !costDecreased2)
            decreaseCostButton2.interactable = true;
        else
            decreaseCostButton2.interactable = false;
    }


    public void upgradeCraft()
    {
        soundManager.PlayUpgradeSound();
        Balance.updateBalance(craftUpgradeCost);
        craftUpgradeCost*=costMultiplier;
        craftUpgradeCostText.text="\n"+Balance.outputCostCorrectly(craftUpgradeCost);

        if(craftUpgrades%2==1 && passiveIncomeManager.periodInSecondsCraft>passiveIncomeManager.minPeriodLimitCraft)
            passiveIncomeManager.periodInSecondsCraft*=(1-passiveIncomeManager.craftTimeDecrease);
        else
            passiveIncomeManager.increaseIncomeCraft();
        
        craftUpgrades++;
        craftLevelText.text = "Lv. "+craftUpgrades;
    }


    public void upgradeFarm()
    {
        soundManager.PlayUpgradeSound();
        Balance.updateBalance(farmUpgradeCost);
        farmUpgradeCost*=costMultiplier;
        farmUpgradeCostText.text="\n"+Balance.outputCostCorrectly(farmUpgradeCost);

        if(farmUpgrades%2==1 && passiveIncomeManager.periodInSecondsFarm>passiveIncomeManager.minPeriodLimitFarm)
            passiveIncomeManager.periodInSecondsFarm*=(1-passiveIncomeManager.farmTimeDecrease);
        else
            passiveIncomeManager.increaseIncomeFarm();
        
        farmUpgrades++;
        farmLevelText.text = "Lv. "+farmUpgrades;
    }


    public void increseIncomeFromBuildings1()
    {
        soundManager.PlayUpgradeSound();
        Balance.updateBalance(upgradeIncomeFromBuild1Cost);
        passiveIncomeManager.increaseCostByBigHouses = true;
        passiveIncomeManager.increaseIncomePerBigHouse(buildingsManager.numOfBigHouses);
        incomeIncreased1=true;
        upgradeIncomeText1.text = "\n Done!";
    }


    public void increseIncomeFromBuildings2()
    {
        soundManager.PlayUpgradeSound();
        Balance.updateBalance(upgradeIncomeFromBuild2Cost);
        passiveIncomeManager.increaseCostByHouses = true;
        passiveIncomeManager.increaseIncomePerHouse(buildingsManager.numOfHouses);
        incomeIncreased2=true;
        upgradeIncomeText2.text = "\n Done!";
    }


    public void decreaseCostBy1()
    {
        soundManager.PlayUpgradeSound();
        Balance.updateBalance(decreaseCost1);
        buildingsManager.decreseCostBigHouse(decreaseCostBy);
        costDecreased1=true;
        decreaseCostText1.text = "\n Done!";
    }


    public void decreaseCostBy2()
    {
        soundManager.PlayUpgradeSound();
        Balance.updateBalance(decreaseCost2);
        buildingsManager.decreseCostHouse(decreaseCostBy);
        costDecreased2=true;
        decreaseCostText2.text = "\n Done!";
    }


    public void resetUI()
    {
        craftUpgradeCostText.text="\n"+Balance.outputCostCorrectly(craftUpgradeCost);
        farmUpgradeCostText.text="\n"+Balance.outputCostCorrectly(farmUpgradeCost);

        if(incomeIncreased1)
            upgradeIncomeText1.text = "\n Done!";
        else
            upgradeIncomeText1.text = "\n"+Balance.outputCostCorrectly(upgradeIncomeFromBuild1Cost);
        
        if(incomeIncreased2)
            upgradeIncomeText2.text = "\n Done!";
        else
            upgradeIncomeText2.text = "\n"+Balance.outputCostCorrectly(upgradeIncomeFromBuild2Cost);
        
        if(costDecreased1)
            decreaseCostText1.text = "\n Done!";
        else
            decreaseCostText1.text = "\n"+Balance.outputCostCorrectly(decreaseCost1);
        
        if(costDecreased2)
            decreaseCostText2.text = "\n Done!";
        else
            decreaseCostText2.text = "\n"+Balance.outputCostCorrectly(decreaseCost2);

        craftLevelText.text="Lv. "+craftUpgrades;
        farmLevelText.text="Lv. "+farmUpgrades;
    }


    public void resetUpgrades(GameData data)
    {
        farmUpgrades = data.levelFarm;
        craftUpgrades = data.levelCraft;
        farmUpgradeCost = data.farmUpgradeCost;
        craftUpgradeCost = data.craftUpgradeCost;
        incomeIncreased1 = data.increasedIncome1;
        incomeIncreased2 = data.increasedIncome2;
        costDecreased1 = data.decreasedCost1;
        costDecreased2 = data.decreasedCost2;

        resetUI();
    }
}
