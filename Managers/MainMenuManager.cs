using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject savePanel;
    public Button[] saveBut;
    public Text[] saveNloadText;
    public Button mainLoadBut;
    public SavesManager savesManager;
    private int saveChoosed;


    private void Start()
    {
        ResetButtonColor();
    }


    private void Update()
    {
        for(int i=0; i<saveBut.Length; i++)
        {
            if(savesManager.checkSaves(i))
            {
                saveBut[i].interactable = true;
                saveNloadText[i].text=savesManager.getSaveName(i);
            }
            else
            {
                saveBut[i].interactable = false;
                saveNloadText[i].text="No save";
            }
        }

        if(saveChoosed==-1)
        {
            mainLoadBut.interactable = false;
        }
        else
        {
            mainLoadBut.interactable = true;
        }
    }


    public void startGame()
    {
        TempObjects.loadSave = false;
        SceneManager.LoadScene("main_screen");
    }

    public void loadGame()
    {
        TempObjects.saveNum = saveChoosed;
        TempObjects.loadSave = true;
        SceneManager.LoadScene("main_screen");
    }

    public void openSaveScreen()
    {
        saveChoosed=-1;
        ResetButtonColor();
        savePanel.SetActive(true);
    }

    public void closeSaveScreen()
    {
        saveChoosed=-1;
        savePanel.SetActive(false);
    }

    public void exitGame()
    {
        Debug.Log("Игра закрыта");
        Application.Quit();
    }



    public void load1Button()
    {
        saveChoosed=0;
        ResetButtonColor();
        ModifyOutline(saveBut[0]);
    }

    public void load2Button()
    {
        saveChoosed=1;
        ResetButtonColor();
        ModifyOutline(saveBut[1]);
    }

    public void load3Button()
    {
        saveChoosed=2;
        ResetButtonColor();
        ModifyOutline(saveBut[2]);
    }

    public void load4Button()
    {
        saveChoosed=3;
        ResetButtonColor();
        ModifyOutline(saveBut[3]);
    }

    public void load5Button()
    {
        saveChoosed=4;
        ResetButtonColor();
        ModifyOutline(saveBut[4]);
    }


    public void ResetButtonColor()
    {
        foreach(Button button in saveBut)
        {
            button.GetComponent<Outline>().enabled=false;
        }
    }


    //Switch on the outline of the button
    private void ModifyOutline(Button button)
    {
        var outline = button.GetComponent<Outline>().enabled=true;
    }
}
