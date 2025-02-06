using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject[] otherPanels;
    public Toggle toggleIncomeSound, toggleMainButton;
    public Slider sliderMusic, sliderSound;
    public Text textSliderMusic, textSliderSound, bigTextSize, middleTextSize, smallTextSize;
    public SavesManager savesManager;
    public SoundManager soundManager;
    public Button[] chooseSettingsBut, saveSettings;
    public UIScaler uiScaler;
    public Sprite choosedButton, normalButton;

    private bool settingsChanged=false, isInitialized = false;
    private SettingsInfoKeeper infoBeforeChange;
    private int currentPanelChoosed=0;


    private void Start()
    {
        savesManager.LoadSettings();

        resetSoundSettingsUI();
        resetUISettingsUI();
    }


    private void Update() 
    {
        if(settingsChanged)
            saveSettings[currentPanelChoosed].interactable = true;
        else
            saveSettings[currentPanelChoosed].interactable = false;
    }

    
    public void resetSoundSettingsUI()
    {
        isInitialized = false;

        //Debug.Log("MainButton "+SettingsInfo.playMusicForMainButton);
        if(SettingsInfo.playMusicForMainButton)
            toggleMainButton.isOn = true;
        else
            toggleMainButton.isOn = false;
        
        //Debug.Log("Income "+SettingsInfo.playForIncome);
        if(SettingsInfo.playForIncome)
            toggleIncomeSound.isOn = true;
        else
            toggleIncomeSound.isOn = false;

        sliderMusic.value = SettingsInfo.musicVolume;
        textSliderMusic.text = (Math.Round(SettingsInfo.musicVolume*100))+"%";
        sliderSound.value = SettingsInfo.soundEffectsVolume;
        textSliderSound.text = (Math.Round(SettingsInfo.soundEffectsVolume*100))+"%";
        
        isInitialized = true;
    }

    public void resetUISettingsUI()
    {
        bigTextSize.text = SettingsInfo.bigTextSize.ToString();
        middleTextSize.text = SettingsInfo.middleTextSize.ToString();
        smallTextSize.text = SettingsInfo.smallTextSize.ToString();
    }


    public void openSettingsScreen()
    {
        infoBeforeChange = SettingsInfo.getAllInfo();
        resetSoundSettingsUI();
        settingsChanged=false;
        settingsPanel.SetActive(true);
        soundManager.PlayClickSound();
        otherPanels[currentPanelChoosed].SetActive(false);
        currentPanelChoosed=0;
        otherPanels[currentPanelChoosed].SetActive(true);
        ResetButtonColor();
        ModifyOutline(chooseSettingsBut[currentPanelChoosed]);
    }


    public void openSoundSettings()
    {
        soundManager.PlayClickSound();

        if(settingsChanged)
        {
            SettingsInfo.setNewSettings(infoBeforeChange);
            resetSoundSettingsUI();
        }

        otherPanels[currentPanelChoosed].SetActive(false);
        currentPanelChoosed=0;
        otherPanels[currentPanelChoosed].SetActive(true);
        ResetButtonColor();
        ModifyOutline(chooseSettingsBut[currentPanelChoosed]);
    }

    public void openUISettings()
    {
        soundManager.PlayClickSound();

        if(settingsChanged)
        {
            SettingsInfo.setNewSettings(infoBeforeChange);
            resetUISettingsUI();
        }

        otherPanels[currentPanelChoosed].SetActive(false);
        currentPanelChoosed=1;
        otherPanels[currentPanelChoosed].SetActive(true);
        ResetButtonColor();
        ModifyOutline(chooseSettingsBut[currentPanelChoosed]);


    }


    public void closeSettingsScreen()
    {
        soundManager.PlayClickSound();

        if(settingsChanged)
        {
            SettingsInfo.setNewSettings(infoBeforeChange);
        }

        settingsPanel.SetActive(false);
        uiScaler.ResizeUI();
        soundManager.updateMusciVolume(SettingsInfo.musicVolume);
    }


    public void onCheckMainButtonSound()
    {
        if (!isInitialized)
        {
            // Ignore the first call at initialization
            return;
        }

        if(SettingsInfo.playMusicForMainButton)
        {
            //toggleMainButton.isOn = false;
            SettingsInfo.playMusicForMainButton=false;
        }
        else
        {
            //toggleMainButton.isOn = true;
            SettingsInfo.playMusicForMainButton=true;
        }

        settingsChanged=true;
        soundManager.PlayClickSound();
        //Debug.Log("Checked");
    }

    public void onCheckIncomeSound()
    {
        if (!isInitialized)
        {
            // Ignore the first call at initialization
            return;
        }

        if(SettingsInfo.playForIncome)
        {
            //toggleIncomeSound.isOn = false;
            SettingsInfo.playForIncome=false;
        }
        else
        {
            //toggleIncomeSound.isOn = true;
            SettingsInfo.playForIncome=true;
        }

        settingsChanged=true;
        soundManager.PlayClickSound();
        //Debug.Log("Checked");
    }

    public void onMusicVolumeChange()
    {
        if (!isInitialized)
        {
            // Ignore the first call at initialization
            return;
        }

        SettingsInfo.musicVolume = sliderMusic.value;
        textSliderMusic.text = (Math.Round(SettingsInfo.musicVolume*100))+"%";
        soundManager.updateMusciVolume(SettingsInfo.musicVolume);

        settingsChanged=true;
        soundManager.PlayChangeValueSound();
        //Debug.Log("Changed");
    }

    public void onSoundVolumeChange()
    {
        if (!isInitialized)
        {
            // Ignore the first call at initialization
            return;
        }

        SettingsInfo.soundEffectsVolume = sliderSound.value;
        textSliderSound.text = (Math.Round(SettingsInfo.soundEffectsVolume*100))+"%";
        soundManager.updateSoundVolume(SettingsInfo.soundEffectsVolume);

        settingsChanged=true;
        soundManager.PlayChangeValueSound();
        //Debug.Log("Changed");
    }

    public void saveSettingsNow()
    {
        soundManager.PlayClickSound();
        savesManager.SaveSettings();
        settingsChanged=false;
        infoBeforeChange = SettingsInfo.getAllInfo();
    }



    public void ResetButtonColor()
    {
        foreach(Button button in chooseSettingsBut)
        {
            button.image.sprite = normalButton;
        }
    }


    //Switch on the outline of the button
    private void ModifyOutline(Button button)
    {
        button.image.sprite = choosedButton;
    }



    public void increaseBigTextSize()
    {
        SettingsInfo.bigTextSize++;
        bigTextSize.text = SettingsInfo.bigTextSize.ToString();
        uiScaler.ResizeUI();

        settingsChanged=true;
        soundManager.PlayChangeValueSound();
        //Debug.Log("Changed");
    }

    public void decreaseBigTextSize()
    {
        SettingsInfo.bigTextSize--;
        bigTextSize.text = SettingsInfo.bigTextSize.ToString();
        uiScaler.ResizeUI();

        settingsChanged=true;
        soundManager.PlayChangeValueSound();
        //Debug.Log("Changed");
    }

    public void increaseMiddleTextSize()
    {
        SettingsInfo.middleTextSize++;
        middleTextSize.text = SettingsInfo.middleTextSize.ToString();
        uiScaler.ResizeUI();

        settingsChanged=true;
        soundManager.PlayChangeValueSound();
        //Debug.Log("Changed");
    }

    public void decreaseMiddleTextSize()
    {
        SettingsInfo.middleTextSize--;
        middleTextSize.text = SettingsInfo.middleTextSize.ToString();
        uiScaler.ResizeUI();

        settingsChanged=true;
        soundManager.PlayChangeValueSound();
        //Debug.Log("Changed");
    }

    public void increaseSmallTextSize()
    {
        SettingsInfo.smallTextSize++;
        smallTextSize.text = SettingsInfo.smallTextSize.ToString();
        uiScaler.ResizeUI();

        settingsChanged=true;
        soundManager.PlayChangeValueSound();
        //Debug.Log("Changed");
    }

    public void decreaseSmallTextSize()
    {
        SettingsInfo.smallTextSize--;
        smallTextSize.text = SettingsInfo.smallTextSize.ToString();
        uiScaler.ResizeUI();

        settingsChanged=true;
        soundManager.PlayChangeValueSound();
        //Debug.Log("Changed");
    }
}
