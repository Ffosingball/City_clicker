using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public static class SettingsInfo
{
    public static bool playMusicForMainButton, playForIncome;
    public static float musicVolume, soundEffectsVolume;

    public static SettingsInfoKeeper getAllInfo()
    {
        Debug.Log("Get Info");
        SettingsInfoKeeper info = new SettingsInfoKeeper();
        info.playMusicForMainButton = playMusicForMainButton;
        info.playForIncome = playForIncome;
        info.musicVolume = musicVolume;
        info.soundEffectsVolume = soundEffectsVolume;

        return info;
    }

    public static void setNewSettings(SettingsInfoKeeper info)
    {
        Debug.Log("New Info");
        playMusicForMainButton = info.playMusicForMainButton;
        playForIncome = info.playForIncome;
        musicVolume = info.musicVolume;
        soundEffectsVolume = info.soundEffectsVolume;
    }
}
