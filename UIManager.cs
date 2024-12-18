using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject[] sidePanels;
    public GameObject mainPanel, hidedPanel;
    private GameObject currentPanel;
    private static Text balanceText;
    public Text balanceTextNonStatic;


    private void Start()
    {
        currentPanel = sidePanels[0];
        balanceText=balanceTextNonStatic;
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
}
