using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BuildingsManager : MonoBehaviour
{
    private List<Structure> allStructuresList, allBuildings;
    private Dictionary<string, List<GameObject>> allRoadsList;
    private int numOfHouses=0, numOfBigHouses=0, numOfCraft=0;
    public float costMultiplier=1.8f, adderIncrease=1, multiplierIncrease=0.2f, probabilityOfTheCornerRoad=0.4f;
    public float houseCost=20, bigHouseCost=40, craftCost=80, roadLength=5;
    public Text houseText, bigHouseText, craftText, numOfHousesText, numOfBigHousesText, numOfCraftHousesText;
    public Button houseButton, bigHouseButton, craftButton;
    public GameObject[] houses;
    public GameObject[] bigHouses;
    public GameObject[] roadTypes;
    public GameObject[] craftBuildings;

    private void Start()
    {
        allStructuresList = new List<Structure>();
        allBuildings = new List<Structure>();
        allRoadsList = new Dictionary<string, List<GameObject>>();

        houseText.text="\n"+houseCost;
        bigHouseText.text="\n"+bigHouseCost;
        craftText.text="\n"+craftCost;
        numOfHousesText.text="";
        numOfBigHousesText.text="";
        numOfCraftHousesText.text="";

        houseButton.interactable = false;
        bigHouseButton.interactable = false;
        craftButton.interactable = false;
    }

    private void FixedUpdate()
    {
        if(Balance.getBalance()>=houseCost)
            houseButton.interactable = true;
        else
            houseButton.interactable = false;

        if(Balance.getBalance()>=bigHouseCost)
            bigHouseButton.interactable = true;
        else
            bigHouseButton.interactable = false;

        if(Balance.getBalance()>=craftCost)
            craftButton.interactable = true;
        else
            craftButton.interactable = false;
    }


    public void buyHouse()
    {
        Balance.updateBalance(houseCost);
        Balance.updateAdder(adderIncrease);
        houseCost=houseCost*costMultiplier;
        houseText.text="\n"+Balance.outputCostCorrectly(houseCost);
        numOfHouses++;
        numOfHousesText.text="X"+numOfHouses;
        createStructure(houses);
    }


    public void buyBigHouse()
    {
        Balance.updateBalance(bigHouseCost);
        Balance.updateMultiplier(multiplierIncrease);
        bigHouseCost=bigHouseCost*costMultiplier;
        bigHouseText.text="\n"+Balance.outputCostCorrectly(bigHouseCost);
        numOfBigHouses++;
        numOfBigHousesText.text="X"+numOfBigHouses;
        createStructure(bigHouses);
    }


    private void createStructure(GameObject[] structures)
    {
        int y = UnityEngine.Random.Range(-500,501);
        int x = UnityEngine.Random.Range(-1000,1001);
        int houseNum = UnityEngine.Random.Range(0,structures.Length);

        GameObject newHouse = Instantiate(structures[houseNum], new Vector3(x,y,1000-(y+500)), Quaternion.Euler(0f,0f,0f));
        Structure newStruct = new Structure(newHouse,x,y,"House");
        allStructuresList.Add(newStruct);
        allBuildings.Add(newStruct);

        createRoadStructure(allBuildings.Count-1);
    }

    private void createRoadStructure(int index1)
    {
        if(allBuildings.Count>1)
        {
            if(allRoadsList.Count==0)
            {
                int index2 = UnityEngine.Random.Range(0,allBuildings.Count);

                while(index1==index2)
                {
                    index2 = UnityEngine.Random.Range(0,allBuildings.Count);
                }

                createRoad(allBuildings[index1].getX(), allBuildings[index1].getY(), allBuildings[index2].getX(), allBuildings[index2].getY(),index1,index2);
            }
            else
            {
                int index2 = UnityEngine.Random.Range(0,allBuildings.Count);

                int repeat=0;
                while((index1==index2 || allRoadsList.ContainsKey(index1+""+index2+"") || allRoadsList.ContainsKey(index2+""+index1+"")) && repeat<10)
                {
                    index2 = UnityEngine.Random.Range(0,allBuildings.Count);
                    repeat++;
                }

                //Debug.Log("i1 "+index1+"; i2 "+index2+"; total "+allBuildings.Count);

                if(!allRoadsList.ContainsKey(index1+""+index2+"") && !allRoadsList.ContainsKey(index2+""+index1+""))
                    createRoad(allBuildings[index1].getX(), allBuildings[index1].getY(), allBuildings[index2].getX(), allBuildings[index2].getY(),index1,index2);
            }
        }
    }


    private void createRoad(int x1, int y1, int x2, int y2, int i1, int i2)
    {
        List<GameObject> tempPartsOfRoad = new List<GameObject>();
        //Debug.Log("Coord1: "+x1+", "+y1+"; Coord2: "+x2+", "+y2+"; ids: "+i1+", "+i2);

        if(UnityEngine.Random.Range(0f,1f)<probabilityOfTheCornerRoad)
        {
            if(UnityEngine.Random.Range(0,2)==0)
            {
                if(y1>y2)
                {
                    GameObject newPart = Instantiate(roadTypes[0], new Vector3(x1,y1-(Math.Abs(y1-y2)/2),1001-(y2+500)), Quaternion.Euler(0f,0f,0f));
                    newPart.transform.localScale=new Vector3(roadLength,Math.Abs(y1-y2),1);
                    tempPartsOfRoad.Add(newPart);
                    allStructuresList.Add(new Structure(newPart,x1,y1-(Math.Abs(y1-y2)/2),"RoadPart"));
                }
                else
                {
                    GameObject newPart = Instantiate(roadTypes[0], new Vector3(x1,y1+(Math.Abs(y1-y2)/2),1001-(y1+500)), Quaternion.Euler(0f,0f,0f));
                    newPart.transform.localScale=new Vector3(roadLength,Math.Abs(y1-y2),1);
                    tempPartsOfRoad.Add(newPart);
                    allStructuresList.Add(new Structure(newPart,x1,y1+(Math.Abs(y1-y2)/2),"RoadPart"));
                }

                if(x1>x2)
                {
                    GameObject newPart = Instantiate(roadTypes[0], new Vector3(x1-(Math.Abs(x1-x2)/2),y2,1001-(y2+500)), Quaternion.Euler(0f,0f,0f));
                    newPart.transform.localScale=new Vector3(Math.Abs(x1-x2),roadLength,1);
                    tempPartsOfRoad.Add(newPart);
                    allStructuresList.Add(new Structure(newPart,x1-(Math.Abs(x1-x2)/2),y2,"RoadPart"));
                }
                else
                {
                    GameObject newPart = Instantiate(roadTypes[0], new Vector3(x1+(Math.Abs(x1-x2)/2),y2,1001-(y2+500)), Quaternion.Euler(0f,0f,0f));
                    newPart.transform.localScale=new Vector3(Math.Abs(x1-x2),roadLength,1);
                    tempPartsOfRoad.Add(newPart);
                    allStructuresList.Add(new Structure(newPart,x1+(Math.Abs(x1-x2)/2),y2,"RoadPart"));
                }
            }
            else
            {
                if(x1>x2)
                {
                    GameObject newPart = Instantiate(roadTypes[0], new Vector3(x1-(Math.Abs(x1-x2)/2),y1,1001-(y1+500)), Quaternion.Euler(0f,0f,0f));
                    newPart.transform.localScale=new Vector3(Math.Abs(x1-x2),roadLength,1);
                    tempPartsOfRoad.Add(newPart);
                    allStructuresList.Add(new Structure(newPart,x1-(Math.Abs(x1-x2)/2),y1,"RoadPart"));
                }
                else
                {
                    GameObject newPart = Instantiate(roadTypes[0], new Vector3(x1+(Math.Abs(x1-x2)/2),y1,1001-(y1+500)), Quaternion.Euler(0f,0f,0f));
                    newPart.transform.localScale=new Vector3(Math.Abs(x1-x2),roadLength,1);
                    tempPartsOfRoad.Add(newPart);
                    allStructuresList.Add(new Structure(newPart,x1+(Math.Abs(x1-x2)/2),y1,"RoadPart"));
                }

                if(y1>y2)
                {
                    GameObject newPart = Instantiate(roadTypes[0], new Vector3(x2,y1-(Math.Abs(y1-y2)/2),1001-(y2+500)), Quaternion.Euler(0f,0f,0f));
                    newPart.transform.localScale=new Vector3(roadLength,Math.Abs(y1-y2),1);
                    tempPartsOfRoad.Add(newPart);
                    allStructuresList.Add(new Structure(newPart,x2,y1-(Math.Abs(y1-y2)/2),"RoadPart"));
                }
                else
                {
                    GameObject newPart = Instantiate(roadTypes[0], new Vector3(x2,y1+(Math.Abs(y1-y2)/2),1001-(y1+500)), Quaternion.Euler(0f,0f,0f));
                    newPart.transform.localScale=new Vector3(roadLength,Math.Abs(y1-y2),1);
                    tempPartsOfRoad.Add(newPart);
                    allStructuresList.Add(new Structure(newPart,x2,y1+(Math.Abs(y1-y2)/2),"RoadPart"));
                }
            }
        }
        else
        {
            Debug.Log("Unusual road!");
        }

        allRoadsList.Add(i1+""+i2+"",tempPartsOfRoad);
        //Debug.Log(i1+""+i2+"");
    }
}
