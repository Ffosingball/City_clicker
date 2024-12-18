using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingsManager : MonoBehaviour
{
    private List<Structure> allStructuresList;
    public float costMultiplier=1.8f, adderIncrease=1, multiplierIncrease=0.2f;
    public int houseCost=20, roadCost=40, craftCost=80;
    public Text houseText, roadText, craftText;
    public Button houseButton, roadButton, craftButton;

    private void Start()
    {
        allStructuresList = new List<Structure>();

        houseText.text="\n"+houseCost;
        roadText.text="\n"+roadCost;
        craftText.text="\n"+craftCost;

        houseButton.interactable = false;
        roadButton.interactable = false;
        craftButton.interactable = false;
    }

    private void FixedUpdate()
    {
        if(Balance.getBalance()>=houseCost)
            houseButton.interactable = true;
        else
            houseButton.interactable = false;

        if(Balance.getBalance()>=roadCost)
            roadButton.interactable = true;
        else
            roadButton.interactable = false;

        if(Balance.getBalance()>=craftCost)
            craftButton.interactable = true;
        else
            craftButton.interactable = false;
    }


    public void buyHouse()
    {
        Balance.updateBalance(houseCost);
        Balance.updateAdder(adderIncrease);
        houseCost=(int)(houseCost*costMultiplier);
        houseText.text="\n"+houseCost;
        createHouseStructure();
    }


    public void buyRoad()
    {
        Balance.updateBalance(roadCost);
        Balance.updateMultiplier(multiplierIncrease);
        roadCost=(int)(roadCost*costMultiplier);
        roadText.text="\n"+roadCost;
        createRoadStructure();
    }


    private void createHouseStructure(){}

    private void createRoadStructure(){}
}
