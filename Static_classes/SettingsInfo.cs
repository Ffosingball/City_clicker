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
        SettingsInfoKeeper info = new SettingsInfoKeeper();
        info.playMusicForMainButton = playMusicForMainButton;
        info.playForIncome = playForIncome;
        info.musicVolume = musicVolume;
        info.soundEffectsVolume = soundEffectsVolume;

        return info;
    }
}
