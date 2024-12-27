using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    public GameObject[] sidePanels;
    public GameObject mainPanel, hidedPanel;
    private GameObject currentPanel;
    private static Text balanceText;
    public Text balanceTextNonStatic;
    public BuildingsManager buildingsManager;
    public UpgradeBuildingsManager upgradeBuildingsmanager;
    public PassiveIncomeManager passiveIncomeManager;
    [HideInInspector]
    public Action<Text,Text>[] textToShow;


    private void Awake()
    {
        currentPanel = sidePanels[0];
        balanceText=balanceTextNonStatic;

        textToShow = new Action<Text,Text>[10];
        textToShow[0] = craftHouseText;
        textToShow[1] = farmHouseText;
        textToShow[2] = houseText;
        textToShow[3] = bigHouseText;
        textToShow[4] = craftUpgradeText;
        textToShow[5] = farmUpgradeText;
        textToShow[6] = decreaseCost1Text;
        textToShow[7] = decreaseCost2Text;
        textToShow[8] = increaseIncome1Text;
        textToShow[9] = increaseIncome2Text;
    }


    public void backButton()
    {
        currentPanel.SetActive(false);
        sidePanels[0].SetActive(true);
        currentPanel=sidePanels[0];
    }

    public void buildingButton()
    {
        currentPanel.SetActive(false);
        sidePanels[1].SetActive(true);
        currentPanel=sidePanels[1];
    }

    public void upgradeButton()
    {
        currentPanel.SetActive(false);
        sidePanels[2].SetActive(true);
        currentPanel=sidePanels[2];
    }

    public void eraButton()
    {
        currentPanel.SetActive(false);
        sidePanels[3].SetActive(true);
        currentPanel=sidePanels[3];
    }

    public void hideUIButton()
    {
        currentPanel.SetActive(false);
        mainPanel.SetActive(false);
        hidedPanel.SetActive(true);
    }

    public void showUIButton()
    {
        hidedPanel.SetActive(false);
        currentPanel.SetActive(true);
        mainPanel.SetActive(true);
    }

    public static void updateText(float n)
    {
        balanceText.text = Balance.outputCostCorrectly(n);
    }



    public void craftHouseText(Text text1, Text text2)
    {
        text1.text = "Every craft house will give income "+passiveIncomeManager.getIncomeCraft()+" every "+passiveIncomeManager.periodInSecondsCraft+" seconds.";
        text2.text = "Craft houses are buildings where artisans make their job!";
    }

    public void farmHouseText(Text text1, Text text2)
    {
        text1.text = "Every farm will give income "+passiveIncomeManager.getIncomeFarm()+" every "+passiveIncomeManager.periodInSecondsFarm+" seconds.";
        text2.text = "Farms are buildings where most of the habitats are working!";
    }

    public void houseText(Text text1, Text text2)
    {
        string finalText="Every house multiply base income by "+((buildingsManager.multiplier-1)*100)+"%.";

        if(upgradeBuildingsmanager.incomeIncreased2)
            finalText = finalText+" Additionally base income for every craft is increased per "+upgradeBuildingsmanager.increaseIncomePerHouseBy+" by every house.";
        
        if(upgradeBuildingsmanager.costDecreased1)
            finalText = finalText+" Additionally cost for this type of buildings has been decreased by "+upgradeBuildingsmanager.decreaseCostBy+" times.";
        
        text1.text=finalText;
        
        text2.text = "Houses for single families for welthier habitats!";
    }

    public void bigHouseText(Text text1, Text text2)
    {
        string finalText="Every big house increase base income by "+buildingsManager.adderIncrease+".";

        if(upgradeBuildingsmanager.incomeIncreased1)
            finalText = finalText+" Additionally base income for every farm is increased per "+upgradeBuildingsmanager.increaseIncomePerBigHouseBy+" by every big house.";
        
        if(upgradeBuildingsmanager.costDecreased1)
            finalText = finalText+" Additionally cost for this type of buildings has been decreased by "+upgradeBuildingsmanager.decreaseCostBy+" times.";
        
        text1.text=finalText;
        
        text2.text = "The simplest buildings where live several families!";
    }

    public void craftUpgradeText(Text text1, Text text2)
    {
        text1.text = "This upgrade will alternately increase base income for every craft house by "+((passiveIncomeManager.craftAmountMultiplier-1)*100)+"% and decrease downtime between incomes by "+((passiveIncomeManager.craftTimeDecrease)*100)+"%.";
        text2.text = "Maximizing efficiency of the artisans!";
    }

    public void farmUpgradeText(Text text1, Text text2)
    {
        text1.text = "This upgrade will alternately increase base income for every farm by "+((passiveIncomeManager.farmAmountMultiplier-1)*100)+"% and decrease downtime between incomes by "+((passiveIncomeManager.farmTimeDecrease)*100)+"%.";
        text2.text = "Maximizing efficiency of the crops!";
    }

    public void decreaseCost1Text(Text text1, Text text2)
    {
        text1.text = "This upgrade will decrease cost of big houses by "+upgradeBuildingsmanager.decreaseCostBy+" times.";
        text2.text = "Fighting with inflation!";
    }

    public void decreaseCost2Text(Text text1, Text text2)
    {
        text1.text = "This upgrade will decrease cost of houses by "+upgradeBuildingsmanager.decreaseCostBy+" times.";
        text2.text = "Fighting with inflation!";
    }

    public void increaseIncome1Text(Text text1, Text text2)
    {
        text1.text = "Base income for every farm is increased per "+upgradeBuildingsmanager.increaseIncomePerBigHouseBy+" by every big house.";
        text2.text = "Fighting with unemployment!";
    }

    public void increaseIncome2Text(Text text1, Text text2)
    {
        text1.text = "Base income for every craft is increased per "+upgradeBuildingsmanager.increaseIncomePerHouseBy+" by every house.";
        text2.text = "Fighting with unemployment!";
    }
}
