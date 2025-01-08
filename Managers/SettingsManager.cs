using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SettingsManager : MonoBehaviour
{
    public GameObject settingsPanel;
    public Button saveSettings;
    public Toggle toggleIncomeSound, toggleMainButton;
    public Slider sliderMusic, sliderSound;
    public Text textSliderMusic, textSliderSound;
    public SavesManager savesManager;
    public SoundManager soundManager;

    private bool settingsChanged=false, isInitialized = false;
    private SettingsInfoKeeper infoBeforeChange;


    private void Start()
    {
        savesManager.LoadSettings();

        resetSoundSettingsUI();
    }


    private void Update() 
    {
        if(settingsChanged)
            saveSettings.interactable = true;
        else
            saveSettings.interactable = false;
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


    public void openSettingsScreen()
    {
        infoBeforeChange = SettingsInfo.getAllInfo();
        resetSoundSettingsUI();
        settingsChanged=false;
        settingsPanel.SetActive(true);
        soundManager.PlayClickSound();
    }

    public void closeSettingsScreen()
    {
        soundManager.PlayClickSound();

        if(settingsChanged)
        {
            SettingsInfo.setNewSettings(infoBeforeChange);
            resetSoundSettingsUI();
        }

        settingsPanel.SetActive(false);
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
}
