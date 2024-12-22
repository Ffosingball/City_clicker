using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeBuildingsManager : MonoBehaviour
{
    public PassiveIncomeManager passiveIncomeManager;
    public float costMultiplier = 1.5f, craftUpgradeCost=40000, farmUpgradeCost=100000;
    public Text craftUpgradeCostText, farmUpgradeCostText, craftLevelText, farmLevelText;
    public Button craftUpgradeButton, farmUpgradeButton;
    private int craftUpgrades=0, farmUpgrades=0;


    private void Start()
    {
        craftUpgradeCostText.text="\n"+craftUpgradeCost;
        farmUpgradeCostText.text="\n"+farmUpgradeCost;

        craftLevelText.text="Lv. 0";
        farmLevelText.text="Lv. 0";
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
    }


    public void upgradeCraft()
    {
        Balance.updateBalance(craftUpgradeCost);
        craftUpgradeCost*=costMultiplier;
        craftUpgradeCostText.text="\n"+Balance.outputCostCorrectly(craftUpgradeCost);

        if(craftUpgrades%2==1 && passiveIncomeManager.periodInSecondsCraft>passiveIncomeManager.minPeriodLimitCraft)
            passiveIncomeManager.periodInSecondsCraft*=(1-passiveIncomeManager.craftTimeDecrease);
        else
            passiveIncomeManager.amountToGetCraft*=passiveIncomeManager.craftAmountMultiplier;
        
        craftUpgrades++;
        craftLevelText.text = "Lv. "+craftUpgrades;
    }


    public void upgradeFarm()
    {
        Balance.updateBalance(farmUpgradeCost);
        farmUpgradeCost*=costMultiplier;
        farmUpgradeCostText.text="\n"+Balance.outputCostCorrectly(farmUpgradeCost);

        if(farmUpgrades%2==1 && passiveIncomeManager.periodInSecondsFarm>passiveIncomeManager.minPeriodLimitFarm)
            passiveIncomeManager.periodInSecondsFarm*=(1-passiveIncomeManager.farmTimeDecrease);
        else
            passiveIncomeManager.amountToGetFarm*=passiveIncomeManager.farmAmountMultiplier;
        
        farmUpgrades++;
        farmLevelText.text = "Lv. "+farmUpgrades;
    }
}
