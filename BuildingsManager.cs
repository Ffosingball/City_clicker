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
    public float costMultiplier=1.8f, adderIncrease=1, multiplierIncrease=0.2f, probabilityOfTheCornerRoad=0.4f, probabilityOfCreatingAddRoad=0.1f;
    public int leastAmountOfBuildingsToCreateAddRoad=10;
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
        createStructure(houses, "House");
    }


    public void buyBigHouse()
    {
        Balance.updateBalance(bigHouseCost);
        Balance.updateMultiplier(multiplierIncrease);
        bigHouseCost=bigHouseCost*costMultiplier;
        bigHouseText.text="\n"+Balance.outputCostCorrectly(bigHouseCost);
        numOfBigHouses++;
        numOfBigHousesText.text="X"+numOfBigHouses;
        createStructure(bigHouses, "BigHouse");
    }


    private void createStructure(GameObject[] structures, string type)
    {
        int y = UnityEngine.Random.Range(-500,501);
        int x = UnityEngine.Random.Range(-1000,1001);
        int houseNum = UnityEngine.Random.Range(0,structures.Length);

        GameObject newHouse = Instantiate(structures[houseNum], new Vector3(x,y,y+500), Quaternion.Euler(0f,0f,0f));
        Structure newStruct = new Structure(newHouse,x,y,type);
        allStructuresList.Add(newStruct);
        allBuildings.Add(newStruct);

        createRoadStructure(allBuildings.Count-1);

        if(allBuildings.Count>=leastAmountOfBuildingsToCreateAddRoad)
        {
            if(UnityEngine.Random.Range(0f,1f)<=probabilityOfCreatingAddRoad)
            {
                createRoadStructure(UnityEngine.Random.Range(0,allBuildings.Count),UnityEngine.Random.Range(0,allBuildings.Count));
            }
        }
    }

    private void createRoadStructure(int index1)
    {
        if(allBuildings.Count>1)
        {
            int index2=findTheClosest(index1);

            if(!allRoadsList.ContainsKey(index1+""+index2+"") && !allRoadsList.ContainsKey(index2+""+index1+""))
                createRoad(allBuildings[index1].getX(), allBuildings[index1].getY(), allBuildings[index2].getX(), allBuildings[index2].getY(),index1,index2);
        }
    }


    private void createRoadStructure(int index1, int index2)
    {
        if(!allRoadsList.ContainsKey(index1+""+index2+"") && !allRoadsList.ContainsKey(index2+""+index1+""))
        {
            Debug.Log("Additional Road!");
            createRoad(allBuildings[index1].getX(), allBuildings[index1].getY(), allBuildings[index2].getX(), allBuildings[index2].getY(),index1,index2);
        }
    }


    private int findTheClosest(int indexTarget)
    {
        double minDistance=100000;
        int closestIndex=-1;
        for(int i=0; i<allBuildings.Count-1; i++)
        {
            if(Math.Sqrt(Math.Pow(allBuildings[i].getX()-allBuildings[indexTarget].getX(),2)+Math.Pow(allBuildings[i].getY()-allBuildings[indexTarget].getY(),2))<minDistance)
            {
                closestIndex = i;
                minDistance=Math.Sqrt(Math.Pow(allBuildings[i].getX()-allBuildings[indexTarget].getX(),2)+Math.Pow(allBuildings[i].getY()-allBuildings[indexTarget].getY(),2));
            }
        }

        return closestIndex;
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
                    tempPartsOfRoad.Add(createVerticalRoad(y1,y1,y2,x1,-1));
                else
                    tempPartsOfRoad.Add(createVerticalRoad(y2,y1,y2,x1,1));

                if(x1>x2)
                    tempPartsOfRoad.Add(createHorizontalRoad(y2,x1,x2,-1));
                else
                    tempPartsOfRoad.Add(createHorizontalRoad(y2,x1,x2,1));
            }
            else
            {
                if(x1>x2)
                    tempPartsOfRoad.Add(createHorizontalRoad(y1,x1,x2,-1));
                else
                    tempPartsOfRoad.Add(createHorizontalRoad(y1,x1,x2,1));

                if(y1>y2)
                    tempPartsOfRoad.Add(createVerticalRoad(y1,y1,y2,x2,-1));
                else
                    tempPartsOfRoad.Add(createVerticalRoad(y2,y1,y2,x2,1));
            }
        }
        else
        {
            Debug.Log("Unusual road!");
        }

        allRoadsList.Add(i1+""+i2+"",tempPartsOfRoad);
        //Debug.Log(i1+""+i2+"");
    }


    public GameObject createVerticalRoad(int h, int y1, int y2, int x, int operand)
    {   
        int y;
        if(operand==-1)
            y=y1-(Math.Abs(y1-y2)/2);
        else
            y=y1+(Math.Abs(y1-y2)/2);

        GameObject newPart = Instantiate(roadTypes[0], new Vector3(x,y,h+520), Quaternion.Euler(0f,0f,0f));
        newPart.transform.localScale=new Vector3(roadLength,Math.Abs(y1-y2),1);
        allStructuresList.Add(new Structure(newPart,x,y,"RoadPart"));
        return newPart;
    }


    public GameObject createHorizontalRoad(int height, int x1, int x2, int operand)
    {
        int x;
        if(operand==-1)
            x=x1-(Math.Abs(x1-x2)/2);
        else
            x=x1+(Math.Abs(x1-x2)/2);

        GameObject newPart = Instantiate(roadTypes[0], new Vector3(x,height,height+520), Quaternion.Euler(0f,0f,0f));
        newPart.transform.localScale=new Vector3(Math.Abs(x1-x2),roadLength,1);
        allStructuresList.Add(new Structure(newPart,x,height,"RoadPart"));
        return newPart;
    }
}
