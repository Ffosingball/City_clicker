using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public static class SettingsInfo
{
    public static bool playMusicForMainButton, playForIncome;
    public static float musicVolume, soundEffectsVolume;
    public static int bigTextSize, middleTextSize, smallTextSize;

    public static SettingsInfoKeeper getAllInfo()
    {
        Debug.Log("Get Info");
        SettingsInfoKeeper info = new SettingsInfoKeeper();
        info.playMusicForMainButton = playMusicForMainButton;
        info.playForIncome = playForIncome;
        info.musicVolume = musicVolume;
        info.soundEffectsVolume = soundEffectsVolume;
        info.bigTextSize = bigTextSize;
        info.middleTextSize = middleTextSize;
        info.smallTextSize = smallTextSize;

        return info;
    }

    public static void setNewSettings(SettingsInfoKeeper info)
    {
        Debug.Log("New Info");
        playMusicForMainButton = info.playMusicForMainButton;
        playForIncome = info.playForIncome;
        musicVolume = info.musicVolume;
        soundEffectsVolume = info.soundEffectsVolume;
        bigTextSize = info.bigTextSize;
        middleTextSize = info.middleTextSize;
        smallTextSize = info.smallTextSize;
    }

    public static void basicSettings()
    {
        playMusicForMainButton = false;
        playForIncome = false;
        musicVolume = 1f;
        soundEffectsVolume = 1f;
        bigTextSize = 22;
        middleTextSize = 14;
        smallTextSize = 25;
    }
}
