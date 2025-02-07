using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject[] sidePanels;
    public GameObject mainPanel, hidedPanel, rewritePanel, warningPanel, deleteWarningPanel, exitWarning;
    private GameObject currentPanel;
    private static Text balanceText;
    public Text balanceTextNonStatic;
    public BuildingsManager buildingsManager;
    public UpgradeBuildingsManager upgradeBuildingsmanager;
    public PassiveIncomeManager passiveIncomeManager;
    public SoundManager soundManager;
    public Button[] loadsBut, saveBut;
    public Button mainSaveBut, yesSaveBut, noSaveBut, mainLoadBut, yesLoadBut, noLoadBut, deleteSaveBut, deleteLoadBut;
    public SavesManager savesManager;
    public Text[] saveNloadText;
    [HideInInspector]
    public Action<Text,Text>[] textToShow;
    public Sprite choosedButton, normalButton;
    private int saveChoosed=-1;


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

        ResetButtonColorSave();
        ResetButtonColorLoad();
    }


    private void Update()
    {
        if (currentPanel == sidePanels[5])
        {
            for(int i=0; i<loadsBut.Length; i++)
            {
                if(savesManager.checkSaves(i))
                {
                    loadsBut[i].interactable = true;
                    saveNloadText[i+5].text=savesManager.getSaveName(i);
                }
                else
                {
                    loadsBut[i].interactable = false;
                    saveNloadText[i+5].text="No save";
                }
            }

            if(saveChoosed==-1)
            {
                mainLoadBut.interactable = false;
                deleteLoadBut.interactable = false;
            }
            else
            {
                mainLoadBut.interactable = true;
                deleteLoadBut.interactable = true;
            }
        }

        if (currentPanel == sidePanels[4])
        {
            for(int i=0; i<saveBut.Length; i++)
            {
                if(savesManager.checkSaves(i))
                    saveNloadText[i].text=savesManager.getSaveName(i);
                else
                    saveNloadText[i].text="No save";
            }

            if(saveChoosed==-1)
            {
                mainSaveBut.interactable = false;
                deleteSaveBut.interactable = false;
            }
            else
            {
                mainSaveBut.interactable = true;
                deleteSaveBut.interactable = true;
            }
        }
    }


    public void backButton()
    {
        currentPanel.SetActive(false);
        sidePanels[0].SetActive(true);
        currentPanel=sidePanels[0];
        soundManager.PlayClickSound();
    }

    public void buildingButton()
    {
        currentPanel.SetActive(false);
        sidePanels[1].SetActive(true);
        currentPanel=sidePanels[1];
        soundManager.PlayClickSound();
    }

    public void upgradeButton()
    {
        currentPanel.SetActive(false);
        sidePanels[2].SetActive(true);
        currentPanel=sidePanels[2];
        soundManager.PlayClickSound();
    }

    public void eraButton()
    {
        currentPanel.SetActive(false);
        sidePanels[3].SetActive(true);
        currentPanel=sidePanels[3];
        soundManager.PlayClickSound();
    }

    public void saveButton()
    {
        currentPanel.SetActive(false);
        sidePanels[4].SetActive(true);
        currentPanel=sidePanels[4];
        saveChoosed=-1;
        soundManager.PlayClickSound();
    }

    public void loadButton()
    {
        currentPanel.SetActive(false);
        sidePanels[5].SetActive(true);
        currentPanel=sidePanels[5];
        saveChoosed=-1;
        soundManager.PlayClickSound();
    }

    public void hideUIButton()
    {
        currentPanel.SetActive(false);
        mainPanel.SetActive(false);
        hidedPanel.SetActive(true);
        soundManager.PlayClickSound();
    }

    public void showUIButton()
    {
        hidedPanel.SetActive(false);
        currentPanel.SetActive(true);
        mainPanel.SetActive(true);
        soundManager.PlayClickSound();
    }

    public void save1Button()
    {
        saveChoosed=0;
        ResetButtonColorSave();
        ModifyOutline(saveBut[0]);
        soundManager.PlayChoiceSound();
    }

    public void save2Button()
    {
        saveChoosed=1;
        ResetButtonColorSave();
        ModifyOutline(saveBut[1]);
        soundManager.PlayChoiceSound();
    }

    public void save3Button()
    {
        saveChoosed=2;
        ResetButtonColorSave();
        ModifyOutline(saveBut[2]);
        soundManager.PlayChoiceSound();
    }

    public void save4Button()
    {
        saveChoosed=3;
        ResetButtonColorSave();
        ModifyOutline(saveBut[3]);
        soundManager.PlayChoiceSound();
    }

    public void save5Button()
    {
        saveChoosed=4;
        ResetButtonColorSave();
        ModifyOutline(saveBut[4]);
        soundManager.PlayChoiceSound();
    }

    public void load1Button()
    {
        saveChoosed=0;
        ResetButtonColorLoad();
        ModifyOutline(loadsBut[0]);
        soundManager.PlayChoiceSound();
    }

    public void load2Button()
    {
        saveChoosed=1;
        ResetButtonColorLoad();
        ModifyOutline(loadsBut[1]);
        soundManager.PlayChoiceSound();
    }

    public void load3Button()
    {
        saveChoosed=2;
        ResetButtonColorLoad();
        ModifyOutline(loadsBut[2]);
        soundManager.PlayChoiceSound();
    }

    public void load4Button()
    {
        saveChoosed=3;
        ResetButtonColorLoad();
        ModifyOutline(loadsBut[3]);
        soundManager.PlayChoiceSound();
    }

    public void load5Button()
    {
        saveChoosed=4;
        ResetButtonColorLoad();
        ModifyOutline(loadsBut[4]);
        soundManager.PlayChoiceSound();
    }


    public void backButtonSaveNLoad()
    {
        currentPanel.SetActive(false);
        sidePanels[0].SetActive(true);
        currentPanel=sidePanels[0];
        ResetButtonColorLoad();
        ResetButtonColorSave();
        soundManager.PlayClickSound();
    }


    //switch of outline of the button
    public void ResetButtonColorLoad()
    {
        foreach(Button button in loadsBut)
        {
            button.image.sprite = normalButton;
        }
    }


    public void ResetButtonColorSave()
    {
        foreach(Button button in saveBut)
        {
            button.image.sprite = normalButton;
        }
    }

    private void ModifyOutline(Button button)
    {
        button.image.sprite = choosedButton;
    }




    public void chooseThisSaveToLoad()
    {
        warningPanel.SetActive(true);
        soundManager.PlayClickSound();
    }

    public void yesLoad()
    {
        soundManager.PlayClickSound();
        savesManager.LoadFrom(saveChoosed);
        saveChoosed=-1;
        warningPanel.SetActive(false);
        ResetButtonColorLoad();
    }

    public void noLoad()
    {
        warningPanel.SetActive(false);
        soundManager.PlayClickSound();
    }

    public void chooseThisSaveSave()
    {
        soundManager.PlayClickSound();
        if(savesManager.checkSaves(saveChoosed))
            rewritePanel.SetActive(true);
        else
        {
            savesManager.SaveTo(saveChoosed);
            saveChoosed=-1;
        }
    }

    public void yesSave()
    {
        soundManager.PlayClickSound();
        savesManager.SaveTo(saveChoosed);
        saveChoosed=-1;
        rewritePanel.SetActive(false);
        ResetButtonColorSave();
    }

    public void noSave()
    {
        rewritePanel.SetActive(false);
        soundManager.PlayClickSound();
    }

    public void deleteTheSave()
    {
        deleteWarningPanel.SetActive(true);
        soundManager.PlayClickSound();
    }

    public void yesDeelete()
    {
        soundManager.PlayClickSound();
        savesManager.deleteSave(saveChoosed);
        saveChoosed=-1;
        deleteWarningPanel.SetActive(false);
        ResetButtonColorSave();
        ResetButtonColorLoad();
    }

    public void noDelete()
    {
        deleteWarningPanel.SetActive(false);
        soundManager.PlayClickSound();
    }
    
    public void exitTheGame()
    {
        exitWarning.SetActive(true);
        soundManager.PlayClickSound();
    }

    public void yesExit()
    {
        soundManager.PlayClickSound();
        SceneManager.LoadScene("main_menu");
    }

    public void noExit()
    {
        exitWarning.SetActive(false);
        soundManager.PlayClickSound();
    }




    public static void updateText(float n)
    {
        balanceText.text = Balance.outputCostCorrectly(n);
    }



    public void craftHouseText(Text text1, Text text2)
    {
        text1.text = "Every craft house will give income "+Balance.outputCostCorrectly(passiveIncomeManager.getIncomeCraft())+" every "+Math.Round(passiveIncomeManager.periodInSecondsCraft,1)+" seconds.";
        text2.text = "Craft houses are buildings where artisans make their job!";

        text1.fontSize = SettingsInfo.smallTextSize;
        text2.fontSize = SettingsInfo.smallTextSize;
    }

    public void farmHouseText(Text text1, Text text2)
    {
        text1.text = "Every farm will give income "+Balance.outputCostCorrectly(passiveIncomeManager.getIncomeFarm())+" every "+Math.Round(passiveIncomeManager.periodInSecondsFarm,1)+" seconds.";
        text2.text = "Farms are buildings where most of the habitats are working!";

        text1.fontSize = SettingsInfo.smallTextSize;
        text2.fontSize = SettingsInfo.smallTextSize;
    }

    public void houseText(Text text1, Text text2)
    {
        string finalText="Every house multiply base income by "+((buildingsManager.multiplier-1)*100)+"%.";

        if(upgradeBuildingsmanager.incomeIncreased2)
            finalText = finalText+" Additionally base income for every craft is increased per "+Balance.outputCostCorrectly(upgradeBuildingsmanager.increaseIncomePerHouseBy)+" by every house.";
        
        if(upgradeBuildingsmanager.costDecreased1)
            finalText = finalText+" Additionally cost for this type of buildings has been decreased by "+Balance.outputCostCorrectly(upgradeBuildingsmanager.decreaseCostBy)+" times.";
        
        text1.text=finalText;
        
        text2.text = "Houses for single families for welthier habitats!";

        text1.fontSize = SettingsInfo.smallTextSize;
        text2.fontSize = SettingsInfo.smallTextSize;
    }

    public void bigHouseText(Text text1, Text text2)
    {
        string finalText="Every big house increase base income by "+buildingsManager.adderIncrease+".";

        if(upgradeBuildingsmanager.incomeIncreased1)
            finalText = finalText+" Additionally base income for every farm is increased per "+Balance.outputCostCorrectly(upgradeBuildingsmanager.increaseIncomePerBigHouseBy)+" by every big house.";
        
        if(upgradeBuildingsmanager.costDecreased1)
            finalText = finalText+" Additionally cost for this type of buildings has been decreased by "+upgradeBuildingsmanager.decreaseCostBy+" times.";
        
        text1.text=finalText;
        
        text2.text = "The simplest buildings where live several families!";

        text1.fontSize = SettingsInfo.smallTextSize;
        text2.fontSize = SettingsInfo.smallTextSize;
    }

    public void craftUpgradeText(Text text1, Text text2)
    {
        text1.text = "This upgrade will alternately increase base income for every craft house by "+((passiveIncomeManager.craftAmountMultiplier-1)*100)+"% and decrease downtime between incomes by "+((passiveIncomeManager.craftTimeDecrease)*100)+"%.";
        text2.text = "Maximizing efficiency of the artisans!";

        text1.fontSize = SettingsInfo.smallTextSize;
        text2.fontSize = SettingsInfo.smallTextSize;
    }

    public void farmUpgradeText(Text text1, Text text2)
    {
        text1.text = "This upgrade will alternately increase base income for every farm by "+((passiveIncomeManager.farmAmountMultiplier-1)*100)+"% and decrease downtime between incomes by "+((passiveIncomeManager.farmTimeDecrease)*100)+"%.";
        text2.text = "Maximizing efficiency of the crops!";

        text1.fontSize = SettingsInfo.smallTextSize;
        text2.fontSize = SettingsInfo.smallTextSize;
    }

    public void decreaseCost1Text(Text text1, Text text2)
    {
        text1.text = "This upgrade will decrease cost of big houses by "+upgradeBuildingsmanager.decreaseCostBy+" times.";
        text2.text = "Fighting with inflation!";

        text1.fontSize = SettingsInfo.smallTextSize;
        text2.fontSize = SettingsInfo.smallTextSize;
    }

    public void decreaseCost2Text(Text text1, Text text2)
    {
        text1.text = "This upgrade will decrease cost of houses by "+upgradeBuildingsmanager.decreaseCostBy+" times.";
        text2.text = "Fighting with inflation!";

        text1.fontSize = SettingsInfo.smallTextSize;
        text2.fontSize = SettingsInfo.smallTextSize;
    }

    public void increaseIncome1Text(Text text1, Text text2)
    {
        text1.text = "Base income for every farm is increased per "+Balance.outputCostCorrectly(upgradeBuildingsmanager.increaseIncomePerBigHouseBy)+" by every big house.";
        text2.text = "Fighting with unemployment!";

        text1.fontSize = SettingsInfo.smallTextSize;
        text2.fontSize = SettingsInfo.smallTextSize;
    }

    public void increaseIncome2Text(Text text1, Text text2)
    {
        text1.text = "Base income for every craft is increased per "+Balance.outputCostCorrectly(upgradeBuildingsmanager.increaseIncomePerHouseBy)+" by every house.";
        text2.text = "Fighting with unemployment!";

        text1.fontSize = SettingsInfo.smallTextSize;
        text2.fontSize = SettingsInfo.smallTextSize;
    }
}
