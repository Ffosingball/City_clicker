using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavesManager : MonoBehaviour
{
    public bool checkSaves(int saveNumber)
    {
        return false;
    }


    public string getSaveName(int saveNumber)
    {
        return "SaveName";
    }


    public void SaveTo(int saveNumber)
    {
        Debug.Log("Saved");
    }


    public void LoadFrom(int saveNumber)
    {
        Debug.Log("Loaded");
    }
}
